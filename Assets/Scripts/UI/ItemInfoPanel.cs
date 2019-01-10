using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class ItemInfoPanel : BasePanel
    {
        private Text contentText;
        private Button useButton;
        private Button moreButton;

        protected override void Start()
        {
            base.Start();
            contentText = transform.Find("Content").GetComponent<Text>();

            //给“装备/使用”按钮添加点击事件
            useButton = transform.Find("ButtonGroup/UseButton").GetComponent<Button>();
            useButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnUseButtonClick();
            });

            //给“更多”按钮添加点击事件
            moreButton = transform.Find("ButtonGroup/MoreButton").GetComponent<Button>();
            moreButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnMoreButtonClick();
            });
        }

        public override void OnEnter(string content)
        {
            if (contentText==null)
            {
                contentText = transform.Find("Content").GetComponent<Text>();
            }
            contentText.text = content;
        }

        public override void OnPause()
        {
            
        }

        public override void OnResume()
        {
            
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}