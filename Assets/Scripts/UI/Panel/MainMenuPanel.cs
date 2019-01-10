using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
	public class MainMenuPanel : BasePanel
	{
        private CanvasGroup canvasGroup;
        private Button talkButton;

        void Start()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            talkButton = GameObject.Find("TalkButton").GetComponent<Button>();
            talkButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnTalkButtonClick();
            });
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnPause()
        {
            canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板 不再和鼠标交互
        }

        public override void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}