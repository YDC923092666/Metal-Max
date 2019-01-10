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

        protected override void Start()
        {
            base.Start();
            talkButton = GameObject.Find("LeftPanel/TalkButton").GetComponent<Button>();
            knapsackButton = GameObject.Find("LeftPanel/KnapsackButton").GetComponent<Button>();
            equipmentButton = GameObject.Find("LeftPanel/EquipmentButton").GetComponent<Button>();

            talkButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnTalkButtonClick();
            });
            knapsackButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnKnapsackButtonClick();
            });
            equipmentButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnEquipmentButtonClick();
            });
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
        }

        public override void OnPause()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            isShow = false;
        }

        public override void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
            isShow = true;
        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isShow = false;
        }
    }
}