using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class ItemInfoPanel : BasePanel
    {

        private Text contentText;
        private CanvasGroup canvasGroup;

        void Start()
        {
            contentText = transform.Find("Content").GetComponent<Text>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void OnEnter(string content)
        {
            if (contentText==null)
            {
                contentText = transform.Find("Content").GetComponent<Text>();
            }
            if(canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            contentText.text = content;
            canvasGroup.alpha = 1;
        }

        public void SetLocalPotion(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetWorldPotion(Vector3 position)
        {
            transform.position = position;
        }

    }
}