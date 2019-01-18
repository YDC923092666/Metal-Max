using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
    public class TipsPanel : BasePanel
    {
        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            StartCoroutine(ClosePanel());
        }

        public override void OnExit()
        {

            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 1);
            canvasGroup.blocksRaycasts = false;
            isShow = false;
            UIManager.Instance.canClickUIButton = true;
        }

        public override void OnPause()
        {
            
        }

        public override void OnResume()
        {
            
        }

        private IEnumerator ClosePanel()
        {
            yield return new WaitForSeconds(3);
            UIManager.Instance.PopPanel();
        }
    }
}