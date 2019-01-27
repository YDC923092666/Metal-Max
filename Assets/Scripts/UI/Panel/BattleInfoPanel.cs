using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
	public class BattleInfoPanel : MonoBehaviour
	{
        public float duration = 1;  //dotween的动画时间
        private Text statusText;    //状态文字
        private Text battleInfoText;    //战斗信息文字

        private void Start()
        {
            statusText = transform.Find("StatusText").GetComponent<Text>();
            battleInfoText = transform.Find("BattleInfoText").GetComponent<Text>();
        }

        //private void OnEnable()
        //{
        //    if (battleInfoText == null)
        //    {
        //        battleInfoText = transform.Find("BattleInfoText").GetComponent<Text>();
        //    }
        //    battleInfoText.text = null;
        //}

        public void ChangeStatusText(string content)
        {
            statusText.text = content;
        }

        public void ChangeBattleInfoText(string content)
        {
            battleInfoText.text = null;
            battleInfoText.DOText(content, duration);
        }

        //public void UpdateStatusPanelUI()
        //{
        //    remainingPlayer
        //}
    }
}