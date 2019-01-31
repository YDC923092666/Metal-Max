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

        private GameObject tipPanel;
        private GameObject statusPanel;

        private Button attackButton;
        private Button escapeButton;

        private void Start()
        {
            upperText = transform.Find("UpperPanel").GetComponentInChildren<Text>();
            nameText = transform.Find("RightPanel/StatusPanel/NameText").GetComponent<Text>();
            hpText = transform.Find("RightPanel/StatusPanel/HPText").GetComponent<Text>();

            leftPanel = transform.Find("LeftPanel").gameObject;
            rightPanel = transform.Find("RightPanel").gameObject;

            tipPanel = transform.Find("RightPanel/TipPanel").gameObject;
            statusPanel = transform.Find("RightPanel/StatusPanel").gameObject;

            attackButton = leftPanel.transform.Find("AttackButton").GetComponent<Button>();
            escapeButton = leftPanel.transform.Find("EscapeButton").GetComponent<Button>();

            //给攻击按钮绑定点击事件
            attackButton.onClick.AddListener(OnAttackButtonClick);

            //给逃跑按钮绑定点击事件
            escapeButton.onClick.AddListener(OnEscapeButtonClick);
        }

        /// <summary>
        /// 初始化面板显示
        /// </summary>
        public void InitPanel()
        {
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

        /// <summary>
        /// 更新右侧人物属性界面
        /// </summary>
        public void UpdateStatusPanelUI()
        {
            BaseAttr status = GameObject.FindGameObjectWithTag(Tags.battleCharactor).GetComponent<BattleStat>().status;
            nameText.text = status.nameString;
            upperText.text = status.nameString;
            hpText.text = status.hp.ToString();
        }

        private void OnAttackButtonClick()
        {
            ShowOnlyOnePanel(rightPanel, "TipPanel");
            BattleGameController.Instance.isWaitForPlayerToChooseTarget = true;
        }

        private void OnEscapeButtonClick()
        {
            BattleGameController.Instance.BattleEscape();
        }
    }
}
