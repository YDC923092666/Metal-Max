using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
    public class MonsterBattle : BattleStat
    {
        public Monster status;

        public override bool IsDead()
        {
            return false;
        }

        public override void ReceiveDamage(int mValue)
        {
            
        }
    }
}