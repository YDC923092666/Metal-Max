using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public class PlayerBattle : BattleStat
	{
        public override void ReceiveDamage(int mValue)
        {
            status.hp -= mValue;
            if (status.hp < 0)
            {
                status.hp = 0;
            }
            mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }
    }
}