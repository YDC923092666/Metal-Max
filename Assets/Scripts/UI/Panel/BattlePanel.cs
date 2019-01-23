using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
	public class BattlePanel : MonoBehaviour 
	{
        private Text upperText; //上方文字
        private Text nameText;
        private Text hpText;

        private GameObject leftPanel;
        private GameObject rightPanel;

        private GameObject tankPanel;
        private GameObject humanPanel;
        private GameObject tipPanel;
        private GameObject statusPanel;
        private GameObject bulletPanel;

        private Button mainButton;
        private Button secondButton;
        private Button bulletButton;
        private Button otherButton1;
        private Button attackButton;
        private Button otherButton2;

        private void Start()
        {
            upperText = transform.Find("UpperPanel").GetComponentInChildren<Text>();
            nameText = transform.Find("RightPanel/StatusPanel/NameText").GetComponent<Text>();
            hpText = transform.Find("RightPanel/StatusPanel/HPText").GetComponent<Text>();

            leftPanel = transform.Find("LeftPanel").gameObject;
            rightPanel = transform.Find("RightPanel").gameObject;

            tankPanel = transform.Find("LeftPanel/TankPanel").gameObject;
            humanPanel = transform.Find("LeftPanel/HumanPanel").gameObject;
            tipPanel = transform.Find("RightPanel/TipPanel").gameObject;
            bulletPanel = transform.Find("RightPanel/BulletPanel").gameObject;
            statusPanel = transform.Find("RightPanel/StatusPanel").gameObject;

            mainButton = tankPanel.transform.Find("MainButton").GetComponent<Button>();
            secondButton = tankPanel.transform.Find("SecondButton").GetComponent<Button>();
            bulletButton = tankPanel.transform.Find("BulletButton").GetComponent<Button>();
            otherButton1 = tankPanel.transform.Find("OtherButton1").GetComponent<Button>();

            attackButton = humanPanel.transform.Find("AttackButton").GetComponent<Button>();
            otherButton2 = humanPanel.transform.Find("OtherButton2").GetComponent<Button>();
            InitPanel();
        }

        /// <summary>
        /// 初始化面板显示
        /// </summary>
        private void InitPanel()
        {
            //显示坦克面板还是人物面板？
            if (SaveManager.currentArchive.isOnTank && SaveManager.currentArchive.isEquipTank)
            {
                ShowOnlyOnePanel(leftPanel, "TankPanel");
            }
            else
            {
                ShowOnlyOnePanel(leftPanel, "HumanPanel");
            }
            ShowOnlyOnePanel(rightPanel, "StatusPanel");
            UpdateStatusPanelUI();
        }

        public GameObject ShowOnlyOnePanel(GameObject parent, string panelName)
        {
            GameObject targetPanel = null;
            foreach (Transform panel in parent.transform)
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

        public void UpdateStatusPanelUI()
        {
            string hpString = null;
            if (SaveManager.currentArchive.isEquipTank && SaveManager.currentArchive.isOnTank)
            {
                var tank = SaveManager.GetTankStatusById(SaveManager.currentArchive.currentEquipTankID);
                hpString = string.Format("HP: {0}\nSP: {1}", SaveManager.currentArchive.personStatus.personCurrentHp, tank.tankSp);
            }
            else
            {
                hpString = string.Format("HP: {0}", SaveManager.currentArchive.personStatus.personCurrentHp);
            }
            nameText.text = SaveManager.currentArchive.personStatus.personName;
            hpText.text = hpString;
        }

        public void BondingTankPanelButtonEvent()
        {
            mainButton.onClick.AddListener(OnMainButtonClick);
        }

        void OnMainButtonClick()
        {

        }
    }
}
