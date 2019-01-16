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
                UIManager.Instance.OnTalkButtonClick();
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
            if (GameManager.Instance.isEquipTank)
            {
                var tankButtonGo = rightPanel.transform.Find("Equipment/ButtonGroup/TankButton").gameObject;
                tankButtonGo.SetActive(true);
                tankButton = tankButtonGo.GetComponent<Button>();
                print(SaveManager.currentArchive.currentTankID);
                var tankStatus = SaveManager.GetTankStatusById(SaveManager.currentArchive.currentTankID);
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
        /// 只显示特定的某个面板，其他MainMenu同级面板全部隐藏
        /// </summary>
        /// <param name="panelName"></param>
        public void ShowOnlyOnePanel(string panelName)
        {
            foreach (Transform panel in rightPanel)
            {
                if (panel.name == panelName)
                {
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    panel.gameObject.SetActive(false);
                }
            }
        }
    }
}