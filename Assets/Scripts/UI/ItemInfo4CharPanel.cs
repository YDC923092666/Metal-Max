using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class ItemInfo4CharPanel : BasePanel
    {
        private Text contentText;
        private Button takeOffButton;
        private Button moreButton;

        protected override void Start()
        {
            base.Start();
            contentText = transform.Find("Content").GetComponent<Text>();

            //给“卸下”按钮添加点击事件
            takeOffButton = transform.Find("ButtonGroup/TakeOffButton").GetComponent<Button>();
            takeOffButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnTakeOffButtonClick();
            });

            //给“丢弃”按钮添加点击事件
            moreButton = transform.Find("ButtonGroup/DropButton").GetComponent<Button>();
            moreButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnDropButtonClick();
            });
        }

        public override void OnEnter(string content)
        {
            if (contentText == null)
            {
                contentText = transform.Find("Content").GetComponent<Text>();
            }
            contentText.text = content;
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
        }

        public override void OnPause()
        {

        }

        public override void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            isShow = true;
        }

        public override void OnExit()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            isShow = false;
        }
    }
}