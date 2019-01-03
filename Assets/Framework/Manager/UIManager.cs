using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
	public class UIManager: MonoSingleton<UIManager>
	{
        private Vector2 rayDir;
        private float distance = 1f; //射线检测距离
        private LayerMask mask = 1 << 8; //射线只检测Item层
        private GameObject buttonPanel; //按钮组面板
        private GameObject talkPanel; //对话框面板
        private Text nameText;  //名字文本框
        private Text containText;   //对话内容文本框
        private int index = 0; //对话的索引
        private int talkInterval = 3;   //对话间隔

        private bool isButtonPanelShow = false; //buttonPanel是否显示

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

        private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板prefab的路径
        private Dictionary<UIPanelType, BasePanel> panelDict; //保存所有被实例化的面板的游戏物体身上的BasePanel组件
        private Stack<BasePanel> panelStack;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            ParseUIPanelTypeJson();
            buttonPanel = GameObject.Find("Canvas/ButtonPanel");
            talkPanel = GameObject.Find("Canvas/TalkPanel");
            nameText = GameObject.Find("Canvas/TalkPanel/NameText").GetComponent<Text>();
            containText = GameObject.Find("Canvas/TalkPanel/ContainText").GetComponent<Text>();
            HideButtonPanel();
            HideTalkPanel();
        }

        /// <summary>
        /// 入栈，把某个页面显示在界面上
        /// </summary>
        public void PushPanel(UIPanelType panelType)
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
            panel.OnEnter();
            panelStack.Push(panel);
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
                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.transform.SetParent(CanvasTransform, false);
                panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }

        private void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();
            TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

            UIPanelInfo jsonObject = JsonUtility.FromJson<UIPanelInfo>(ta.text);
            foreach (UIPanelInfo info in jsonObject.infoList)
            {
                panelPathDict.Add(info.panelType, info.path);
            }
        }

        public static NPCInfo GetNPCjectFromListById(int id)
        {
            List<NPCInfo> objList = SaveManager.GetObjectListFromJsonString<NPCInfo>(NPCInfoFilePath);
            foreach (var item in objList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
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
                HideButtonPanel();
                ShowTalkPanel();
                hit.collider.GetComponent<NPCBase>().Talk();
            }
            else
            {
                //显示tipPanel，提示前面没有人。
                //TODO
            }
        }

        /// <summary>
        /// 当右下角按钮被点击时，显示buttonPanel，当再点击时，隐藏buttonPanel;
        /// </summary>
        public void OnUIButtonClick()
        {
            if (!isButtonPanelShow)
            {
                ShowButtonPanel();
            }
            else
            {
                HideButtonPanel();
            }
        }

        public void ShowButtonPanel()
        {
            buttonPanel.SetActive(true);
            isButtonPanelShow = true;
        }

        public void HideButtonPanel()
        {
            buttonPanel.SetActive(false);
            isButtonPanelShow = false;
        }

        public void ShowTalkPanel()
        {
            talkPanel.SetActive(true);
        }

        public void HideTalkPanel()
        {
            talkPanel.SetActive(false);
        }

        /// <summary>
        /// 更新对话面板
        /// </summary>
        /// <param name="nameText">角色名</param>
        /// <param name="containTextList">角色对话列表</param>
        public void UpdateTalkPanel(string nameText, List<string> containTextList)
        {
            this.nameText.text = nameText + "：";
            //读取所有的对话内容
            ChangeContainText(containTextList);
        }

        /// <summary>
        /// 递归调用本身，读取所有的对话内容
        /// </summary>
        /// <param name="containTextList">包含对话的list</param>
        private void ChangeContainText(List<string> containTextList)
        {
            //递归结束的条件。如果索引大于等于列表的长度，则表示对话内容已经读取完毕。
            //隐藏对话面板
            if (index >= containTextList.Count)
            {
                HideTalkPanel();
                return;
            }
            //使用dotween插件，每一次动画完成后递归调用本函数，直至所有对话内容读取完毕
            Tweener t = containText.DOText(containTextList[index], talkInterval).OnComplete(() =>
            {
                index++;
                containText.text = null;
                ChangeContainText(containTextList);
            });
        }

        public void SetResolution(float width, float height, float matchWidthOrHeight)
        {
            var canvas = GameObject.Find("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }
    }
}