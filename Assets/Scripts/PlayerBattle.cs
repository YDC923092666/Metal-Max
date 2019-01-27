using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public class PlayerBattle : BattleStat
	{
        private GameObject battleInfoPanel;

        public override void Start()
        {
            base.Start();
            if (battleInfoPanel == null)
            {
                battleInfoPanel = GameObject.Find("Canvas/BattleInfoPanel");
            }
        }

        public override void ReceiveDamage(int mValue)
        {
            status.hp -= mValue;
            if (status.hp < 0)
            {
                status.hp = 0;
            }
            mRenderer.DOFade(0, duration).OnComplete(() =>
            {
                mRenderer.DOFade(255, duration);
            });
            Camera.main.DOShakePosition(0.2f, new Vector3(1, 1, 0));
            //更新status面板
            var battleInfoPanelScript = battleInfoPanel.GetComponent<BattleInfoPanel>();
            string statusText = string.Format("HP:{0}", status.hp);
            battleInfoPanelScript.ChangeStatusText(statusText);
        }

        private void Update()
        {
            
        }
    }
}