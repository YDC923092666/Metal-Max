using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public class PlayerBattle : BattleStat
	{
        public Charactor status;

        public override bool IsDead()
        {
            throw new System.NotImplementedException();
        }

        public override void ReceiveDamage(int mValue)
        {
            //((HumanState)status).hp -= mValue;
            //if (((HumanState)status).hp < 0)
            //{
            //    ((HumanState)status).hp = 0;
            //}
            //mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }
    }
}