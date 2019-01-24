using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class PlayerBattle : BattleStat
	{
        public int HP = 100;

        //等待玩家操作
        public bool isWaitPlayer = true;
        public bool ifUIshow = true;

        //动画组件
        private Animator mAnim;

        // Use this for initialization
        void Start()
        {
            mAnim = GetComponent<Animator>();
            mAnim.SetBool("idle", true);
        }

        //伤害
        void OnDamage(int mValue)
        {
            HP -= mValue;
        }

        public override bool IsDead()
        {
            if (HP == 0)
            {
                return true;
            }
            return false;
        }
    }
}