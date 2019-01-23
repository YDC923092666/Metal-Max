using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public class MonsterBattle : MonoBehaviour
	{
        public int HP = 100;
        public bool isWaitPlayer = true;
        public float duration = 0.5f;
        public Monster monster;

        private SpriteRenderer mRenderer;

        void Start()
        {
            mRenderer = GetComponent<SpriteRenderer>();
            //mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }

        void OnDamage(int mValue)
        {
            HP -= mValue;
        }

        //敌人AI算法
        public void StartAI()
        {
            if (!isWaitPlayer)
            {
                //人物闪烁，表示攻击
                mRenderer.DOFade(0, duration).ChangeEndValue(255, true).Restart();
            }
        }
    }
}