﻿#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif
 
#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace MetalMax
{
	public class UIManager: MonoSingleton<UIManager>
	{
        private Vector2 rayDir;
        private float distance = 1f; //射线检测距离
        private LayerMask mask = 1 << 8; //射线只检测Item层
        private GameObject talkPanel; //对话框面板
        private Text nameText;  //名字文本框
        private Text containText;   //对话内容文本框
        private int index = 0; //对话的索引
        private int talkInterval = 3;   //对话间隔
        private bool canClickUIButton = true; //是否可以点击UI按钮。在某些情况下（如正在对话），该按钮不能点击
        private Stack<BasePanel> panelStack;

        private Transform canvasTransform;
        public Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GameObject.Find("Canvas").transform;
                }
                return canvasTransform;
            }
        }

        private ETCButton button;   //UI按钮

        private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板prefab的路径
        public static Dictionary<UIPanelType, BasePanel> panelDict; //保存所有被实例化的面板的游戏物体身上的BasePanel组件
        
        [HideInInspector]
        public static Slot selectedSlot; //当前选中的物体

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            ParseUIPanelTypeJson();
        }

        private void Start()
        {
            button = CanvasTransform.GetComponentInChildren<ETCButton>();
            button.onDown.AddListener(() =>
            {
                OnUIButtonClick();
            });
        }

        private void Update()
        {
            if (canClickUIButton)
            {
                button.activated = true;
            }
            else
            {
                button.activated = false;
            }

            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
#if IPHONE || ANDROID
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
                if (EventSystem.current.IsPointerOverGameObject())
#endif
                {
                    Debug.Log("当前触摸在UI上");
                }
                else
                {
                    Debug.Log("当前没有触摸在UI上");
                    PopPanel();
                }
            }
        }

        /// <summary>
        /// 入栈，把某个页面显示在界面上
        /// </summary>
        public void PushPanel(UIPanelType panelType, string content=null)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断一下栈里面是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            BasePanel panel = GetPanel(panelType);
            panel.OnEnter(content);
            if (panelStack.Contains(panel) == false)
            {
                panelStack.Push(panel);
            }
        }

        /// <summary>
        /// 出栈
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            //判断一下栈里面是否有页面
            if (panelStack.Count <= 0) return;

            //关闭栈顶页面的显示
            BasePanel topPanel1 = panelStack.Pop();
            topPanel1.OnExit();


            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();
        }

        /// <summary>
        /// 根据面板类型，得到实例化的面板
        /// </summary>
        /// <returns></returns>
        private BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelDict == null)
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();
            }

            BasePanel panel;
            panelDict.TryGetValue(panelType, out panel);

            if (panel == null)
            {
                //如果找不到，那么就找这个面板的prefab的路径，实例化面板
                string path;
                panelPathDict.TryGetValue(panelType, out path);

                GameObject instPanel = Instantiate(Resources.Load(path)) as GameObject;
                instPanel.name = Resources.Load(path).name; //保持prefab本来的名字
                instPanel.transform.SetParent(CanvasTransform, false);
                panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }

        /// <summary>
        /// 解析json文件
        /// </summary>
        public void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();
            UIPanelTypeJson infos = SaveManager.GetObjectFromJsonString<UIPanelTypeJson>(Const.panelTypeFilePath);
            foreach (UIPanelInfo info in infos.infoList)
            {
                panelPathDict.Add(info.panelType, info.path);
            }
        }

        
        public void SetResolution(float width, float height, float matchWidthOrHeight)
        {
            var canvas = GameObject.Find("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }

        /// <summary>
        /// 更新对话面板
        /// </summary>
        /// <param name="nameText">角色名</param>
        /// <param name="containTextList">角色对话列表</param>
        public void UpdateTalkPanel(string nameText, List<NPCTalk> containTextList)
        {
            this.nameText.text = nameText + "：";
            //读取所有的对话内容
            ChangeContainText(containTextList);
        }

        /// <summary>
        /// 递归调用本身，读取所有的对话内容
        /// </summary>
        /// <param name="containTextList">包含对话的list</param>
        private void ChangeContainText(List<NPCTalk> containTextList)
        {
            //递归结束的条件。如果索引大于等于列表的长度，则表示对话内容已经读取完毕。
            //隐藏对话面板
            if (index >= containTextList.Count)
            {
                PopPanel();
                this.nameText.text = null;
                index = 0;
                canClickUIButton = true;
                return;
            }
            //使用dotween插件，每一次动画完成后递归调用本函数，直至所有对话内容读取完毕
            containText.DOText(containTextList[index].talk, talkInterval).OnComplete(() =>
            {
                index++;
                containText.text = null;
                ChangeContainText(containTextList);
            });
        }

        public void ShowItemInfoPanel(string content, ItemType type)
        {
            PushPanel(UIPanelType.ItemInfoPanel, content);
            string text = null;
            switch (type)
            {
                case ItemType.Consumable:
                    text = "使用";
                    break;
                case ItemType.PersonEquipment:
                    text = "装备";
                    break;
                case ItemType.TankEquipment:
                    text = "装备";
                    break;
                case ItemType.Special:
                    text = "使用";
                    break;
            }
            panelDict[UIPanelType.ItemInfoPanel].gameObject.transform.Find("ButtonGroup/UseButton/Text").GetComponent<Text>().text = text;
        }

        public void ShowItemInfoPanel(string content)
        {
            PushPanel(UIPanelType.ItemInfoPanel, content);
            string text = "卸下";
            panelDict[UIPanelType.ItemInfoPanel].gameObject.transform.Find("ButtonGroup/UseButton/Text").GetComponent<Text>().text = text;
        }

        public void HideItemInfoPanel()
        {
            BasePanel panel;
            panelDict.TryGetValue(UIPanelType.ItemInfoPanel, out panel);
            if (panelStack.Contains(panel))
            {
                PopPanel();
            }
        }

        #region ButtonClickEvent
        /// <summary>
        /// 当右下角按钮被点击时，显示buttonPanel，当再点击时，隐藏buttonPanel;
        /// </summary>
        public void OnUIButtonClick()
        {
            BasePanel mainMenuPanel = null;
            if (panelDict == null)
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();
            }
            //判断主面板是否已经初始化过
            var isExist = panelDict.TryGetValue(UIPanelType.MainMenuPanel, out mainMenuPanel);
            if (isExist == false || (isExist && mainMenuPanel.isShow == false))
            {
                PushPanel(UIPanelType.MainMenuPanel);
            }
            else if (isExist && mainMenuPanel.isShow == true)
            {
                PopPanel();
            }
        }

        /// <summary>
        /// 当对话按钮被点击时
        /// </summary>
        public void OnTalkButtonClick()
        {
            //射线检测身前的物体
            Vector2 headDir = Char01Move.direction;
            Vector2 char01Position = GameObject.FindGameObjectWithTag(Tags.charactor).transform.position;
            RaycastHit2D hit = Physics2D.Raycast(char01Position, headDir, distance, mask);
            if (hit.collider != null && hit.collider.tag == Tags.NPC)
            {
                canClickUIButton = false;
                PushPanel(UIPanelType.TalkPanel);
                talkPanel = panelDict[UIPanelType.TalkPanel].gameObject;
                nameText = talkPanel.transform.Find("NameText").GetComponent<Text>();
                containText = talkPanel.transform.Find("ContainText").GetComponent<Text>();
                
                hit.collider.GetComponent<NPCBase>().Talk();
            }
            else
            {
                //显示tipPanel，提示前面没有人。
                //TODO
            }
        }

        /// <summary>
        /// 打开背包面板
        /// </summary>
        public void OnKnapsackButtonClick()
        {
            PushPanel(UIPanelType.KnapsackPanel);
        }

        /// <summary>
        /// 打开装备面板
        /// </summary>
        public void OnEquipmentButtonClick()
        {
            PushPanel(UIPanelType.CharacterPanel);
        }

        public void OnUseButtonClick()
        {
            //弹出当前最高层（ItemInfoPanel），显示角色面板层
            PopPanel();
            PushPanel(UIPanelType.CharacterPanel);

            CharacterPanel.Instance.PutOn(selectedSlot.GetComponentInChildren<ItemUI>().Item);
            DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
        }

        public void OnMoreButtonClick()
        {
        }
        #endregion
    }
}