using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
	public class MainMenuPanel : BasePanel
	{
        private Button talkButton;
        private Button knapsackButton;
        private Button equipmentButton;
        private Button charButton;
        private Button tankButton;

        private float distance = 1f; //射线检测距离
        private LayerMask mask = 1 << 8; //射线只检测Item层
        private GameObject talkPanel; //对话框面板
        private Text nameText;  //名字文本框
        private Text containText;   //对话内容文本框
        private int index = 0; //对话的索引
        private int talkInterval = 3;   //对话间隔

        private Transform rightPanel;

        protected override void Start()
        {
            base.Start();
            rightPanel = transform.Find("RightPanel");
            talkButton = transform.Find("LeftPanel/TalkButton").GetComponent<Button>();
            knapsackButton = transform.Find("LeftPanel/KnapsackButton").GetComponent<Button>();
            equipmentButton = transform.Find("LeftPanel/EquipmentButton").GetComponent<Button>();

            talkButton.onClick.AddListener(() =>
            {
                OnTalkButtonClick();
            });
            knapsackButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnKnapsackButtonClick();
            });
            equipmentButton.onClick.AddListener(OnEquipmentButtonClick);
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            if(rightPanel == null) rightPanel = transform.Find("RightPanel");
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
            ShowOnlyOnePanel("Status");
        }

        public override void OnPause()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            isShow = false;
        }

        public override void OnResume()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
            ShowOnlyOnePanel("Status");
        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isShow = false;
            ShowOnlyOnePanel("Status");
        }

        /// <summary>
        /// 打开装备面板
        /// </summary>
        public void OnEquipmentButtonClick()
        {
            ShowOnlyOnePanel("Equipment");
            var charButtonGo = rightPanel.transform.Find("Equipment/ButtonGroup/CharButton").gameObject;
            charButton = charButtonGo.GetComponent<Button>();
            charButton.GetComponentInChildren<Text>().text = SaveManager.currentArchive.personStatus.personName;
            charButton.onClick.AddListener(OnCharButtonClick);
            //如果装备了坦克，则显示查看坦克装备的按钮
            if (SaveManager.currentArchive.isEquipTank)
            {
                var tankButtonGo = rightPanel.transform.Find("Equipment/ButtonGroup/TankButton").gameObject;
                tankButtonGo.SetActive(true);
                tankButton = tankButtonGo.GetComponent<Button>();
                print(SaveManager.currentArchive.currentEquipTankID);
                var tankStatus = SaveManager.GetTankStatusById(SaveManager.currentArchive.currentEquipTankID);
                tankButton.GetComponentInChildren<Text>().text = tankStatus.tankName;
                tankButton.onClick.AddListener(OnTankButtonClick);
            }
            else
            {
                var tankButtonGo = rightPanel.transform.Find("Equipment/ButtonGroup/TankButton").gameObject;
                tankButtonGo.SetActive(false);
            }
            
        }


        public void OnCharButtonClick()
        {
            UIManager.Instance.PushPanel(UIPanelType.CharacterPanel);
        }

        public void OnTankButtonClick()
        {
            UIManager.Instance.PushPanel(UIPanelType.TankPanel);
        }

        /// <summary>
        /// 当对话按钮被点击时
        /// </summary>
        public void OnTalkButtonClick()
        {
            //射线检测身前的物体
            Vector2 headDir = CharactorMove.direction;
            Vector2 char01Position = GameObject.FindGameObjectWithTag(Tags.charactor).transform.position;
            RaycastHit2D hit = Physics2D.Raycast(char01Position, headDir, distance, mask);
            if (hit.collider != null && hit.collider.tag == Tags.NPC)
            {
                UIManager.Instance.canClickUIButton = false;
                hit.collider.GetComponent<NPCBase>().Talk();
            }
            else
            {
                //显示tipPanel，提示前面没有人。
                //TODO
            }
        }

        /// <summary>
        /// 只显示特定的某个面板，其他MainMenu同级面板全部隐藏
        /// </summary>
        /// <param name="panelName"></param>
        public GameObject ShowOnlyOnePanel(string panelName)
        {
            GameObject targetPanel = null;
            foreach (Transform panel in rightPanel)
            {
                if (panel.name == panelName)
                {
                    targetPanel = panel.gameObject;
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    panel.gameObject.SetActive(false);
                }
            }
            return targetPanel;
        }

        /// <summary>
        /// 显示InfoPanel和指定的内容
        /// </summary>
        /// <param name="content">要显示的文本内容</param>
        public void ShowInfoPanel(List<NPCTalk> containTextList)
        {
            var infoPanel = ShowOnlyOnePanel("Info");
            if(containText == null)
            {
                containText = infoPanel.GetComponentInChildren<Text>();
            }
            containText.text = null;
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
                ShowOnlyOnePanel("Status");
                index = 0;
                UIManager.Instance.canClickUIButton = true;
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
    }
}