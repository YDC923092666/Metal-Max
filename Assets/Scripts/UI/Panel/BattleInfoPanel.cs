using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
	public class BattleInfoPanel : MonoBehaviour
	{
        private Text statusText;    //状态文字
        private Text battleInfoText;    //战斗信息文字

        private void Start()
        {
            statusText = transform.Find("StatusText").GetComponent<Text>();
            battleInfoText = transform.Find("BattleInfoText").GetComponent<Text>();
        }

        public void ChangeStatusText(string content)
        {
            statusText.text = content;
        }

        public void ChangeBattleInfoTextText(string content)
        {
            battleInfoText.text = content;
        }
    }
}