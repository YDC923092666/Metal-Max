#if UNITY_ANDROID && !UNITY_EDITOR
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
        
        public bool canClickUIButton = true; //是否可以点击UI按钮。在某些情况下（如正在对话），该按钮不能点击
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
        
        public Slot selectedSlot; //当前选中的格子

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
        public GameObject PushPanel(UIPanelType panelType, string content=null)
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
            return panel.gameObject;
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
        /// 显示tipspanel和指定的内容
        /// </summary>
        /// <param name="content">要显示的文本内容</param>
        public void ShowTipsPanel(string content)
        {
            var tipsPanel = PushPanel(UIPanelType.TipsPanel);
            var tipsPanelText = tipsPanel.GetComponentInChildren<Text>();
            tipsPanelText.text = content;
        }

        public void ShowItemInfoPanel(string content, ItemType type)
        {
            PushPanel(UIPanelType.ItemInfo4knapsackPanel, content);
            var buttonGo = panelDict[UIPanelType.ItemInfo4knapsackPanel].gameObject;
            string text = null;
            string buttonName = null;
            switch (type)
            {
                case ItemType.Consumable:
                    text = "使用";
                    buttonName = "UseButton";
                    break;
                case ItemType.PersonEquipment:
                    text = "装备";
                    buttonName = "TakeOnButton";
                    break;
                case ItemType.Tank:
                    text = "装备";
                    buttonName = "TakeOnButton";
                    buttonGo.transform.Find("ButtonGroup/SellButton").gameObject.SetActive(false);
                    buttonGo.transform.Find("ButtonGroup/DropButton").gameObject.SetActive(false);
                    break;
                case ItemType.TankEquipment:
                    text = "装备";
                    buttonName = "TakeOnButton";
                    break;
                case ItemType.Special:
                    text = "使用";
                    buttonName = "UseButton";
                    break;
            }
            buttonGo.transform.Find("ButtonGroup").GetChild(0).GetComponentInChildren<Text>().text = text;
            buttonGo.transform.Find("ButtonGroup").GetChild(0).name = buttonName;
        }

        /// <summary>
        /// 显示角色面板专用ItemInfoPanel
        /// </summary>
        /// <param name="content"></param>
        public void ShowItemInfo4CharPanel(string content)
        {
            PushPanel(UIPanelType.ItemInfo4CharPanel, content);
            string text = "卸下";

            panelDict[UIPanelType.ItemInfo4CharPanel].gameObject.transform.Find("ButtonGroup/TakeOffButton/Text").GetComponent<Text>().text = text;
        }

        /// <summary>
        /// 显示角色面板专用ItemInfoPanel
        /// </summary>
        /// <param name="content"></param>
        public void ShowItemInfo4CharPanel(string content, Item item)
        {
            PushPanel(UIPanelType.ItemInfo4CharPanel, content);
            string text = "卸下";
            var panelGo = panelDict[UIPanelType.ItemInfo4CharPanel].gameObject;
            if (item.itemType == ItemType.Tank)
            {
                panelGo.transform.Find("ButtonGroup/SellButton").gameObject.SetActive(false);
                panelGo.transform.Find("ButtonGroup/DropButton").gameObject.SetActive(false);
            }
            panelGo.transform.Find("ButtonGroup/TakeOffButton/Text").GetComponent<Text>().text = text;
        }

        public void HideItemInfoPanel()
        {
            BasePanel panel;
            panelDict.TryGetValue(UIPanelType.ItemInfo4knapsackPanel, out panel);
            if (panelStack.Contains(panel))
            {
                PopPanel();
            }
        }

        /// <summary>
        /// 显示TalkPanel和指定的内容
        /// </summary>
        /// <param name = "content" > 要显示的文本内容 </ param >
        //public void ShowTalkPanel(List<NPCTalk> containTextList)
        //{
        //    if (containText == null)
        //    {
        //        containText = infoPanel.GetComponentInChildren<Text>();
        //    }
        //    containText.text = null;
        //    ChangeContainText(containTextList);
        //}

        /// <summary>
        /// 递归调用本身，读取所有的对话内容
        /// </summary>
        /// <param name = "containTextList" > 包含对话的list </ param >
        //private void ChangeContainText(List<NPCTalk> containTextList)
        //{
        //    递归结束的条件。如果索引大于等于列表的长度，则表示对话内容已经读取完毕。
        //    隐藏对话面板
        //    if (index >= containTextList.Count)
        //    {
        //        ShowOnlyOnePanel("Status");
        //        index = 0;
        //        UIManager.Instance.canClickUIButton = true;
        //        return;
        //    }
        //    使用dotween插件，每一次动画完成后递归调用本函数，直至所有对话内容读取完毕
        //    containText.DOText(containTextList[index].talk, talkInterval).OnComplete(() =>
        //    {
        //        index++;
        //        containText.text = null;
        //        ChangeContainText(containTextList);
        //    });
        //}

        #region ButtonClickEvent
        /// <summary>
        /// 当右下角按钮被点击时，显示buttonPanel，当再点击时，隐藏buttonPanel;
        /// </summary>
        public void OnUIButtonClick()
        {
            print("UIBUTTONCLICK");
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
        /// 打开背包面板
        /// </summary>
        public void OnKnapsackButtonClick()
        {
            PushPanel(UIPanelType.KnapsackPanel);
        }

        /// <summary>
        /// 点击装备/使用按钮
        /// </summary>
        /// <param name="go"></param>
        public void OnClick(GameObject go)
        {
            if(go != null)
            {
                var selectedItem = selectedSlot.GetComponentInChildren<ItemUI>().Item;
                switch (go.name)
                {
                    case "TakeOnButton":
                        //如果装备类型是人物装备，则给人物穿上
                        if(selectedItem.itemType == ItemType.PersonEquipment)
                        {
                            //弹出当前最高层（ItemInfoPanel），显示角色面板层
                            PopPanel();
                            PushPanel(UIPanelType.CharacterPanel);
                            PopPanel();

                            CharacterPanel.Instance.PutOn(selectedItem);
                            DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
                        }
                        //如果装备类型是坦克装备，则给坦克装备上
                        else if (selectedItem.itemType == ItemType.TankEquipment) 
                        {
                            //首先判断人物是否已经装备上了坦克
                            if (SaveManager.currentArchive.isEquipTank)
                            {
                                PopPanel();
                                PushPanel(UIPanelType.TankPanel);
                                PopPanel();

                                TankPanel.Instance.PutOn(selectedItem);
                                DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
                            }
                            else
                            {
                                PopPanel();
                                ShowTipsPanel("请先装备一辆坦克");
                            }
                        }
                        //如果装备类型是坦克
                        else
                        {
                            //弹出当前最高层（ItemInfoPanel），显示角色面板层
                            PopPanel();
                            PushPanel(UIPanelType.CharacterPanel);
                            PopPanel();

                            CharacterPanel.Instance.PutOnTank(selectedItem);
                            SaveManager.currentArchive.currentEquipTankID = selectedItem.id;
                            SaveManager.currentArchive.isEquipTank = true;
                            DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
                        }
                        break;
                    case "UseButton":
                        PopPanel();
                        //调用物品的use方法，适用于消耗品和特殊物品
                        selectedItem.Use();
                        break;
                }
            }
        }

        /// <summary>
        /// 点击卸下按钮
        /// </summary>
        public void OnTakeOffButtonClick()
        {
            //弹出当前最高层（ItemInfoPanel）
            PopPanel();
            var item = selectedSlot.GetComponentInChildren<ItemUI>().Item;
            CharacterPanel.Instance.PutOff(item);
            DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
            CharacterPanel.Instance.UpdateUI();
        }
        
        /// <summary>
        /// 点击丢弃按钮
        /// </summary>
        public void OnDropButtonClick()
        {
            DestroyImmediate(selectedSlot.transform.GetChild(0).gameObject);
        }
        #endregion
    }
}