using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
    public class MonsterBattle : BattleStat
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
                //更新battleInfo面板
                var battleInfoPanelScript = battleInfoPanel.GetComponent<BattleInfoPanel>();
                var infoText = gameObject.name + "被击败！";
                battleInfoPanelScript.ChangeBattleInfoText(infoText);

                //exp,gold累加
                BattleGameController.Instance.exp += ((Monster)status).exp;
                BattleGameController.Instance.gold += ((Monster)status).gold;
            }
            //mRenderer.DOFade(0, duration).OnComplete(() =>
            //{
            //    mRenderer.DOFade(255, duration);
            //});
            //Camera.main.DOShakePosition(0.2f, new Vector3(1, 1, 0));
        }

        public void OnClick()
        {
            print(gameObject.name);
            if (BattleGameController.Instance.isWaitForPlayerToChooseTarget)
            {
                BattleGameController.Instance.currentActUnitTarget = this.gameObject;
                BattleGameController.Instance.StartCoroutine(BattleGameController.Instance.LaunchAttack());
            }
        }
    }
}