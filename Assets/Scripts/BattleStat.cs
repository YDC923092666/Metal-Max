using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public abstract class BattleStat : MonoBehaviour 
	{
        public float duration = 0.5f;

        protected SpriteRenderer mRenderer;

        public virtual void Start()
        {
            mRenderer = GetComponent<SpriteRenderer>();
            //mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }

        public abstract void ReceiveDamage(int mValue);

        public abstract bool IsDead();

        public virtual void Attack(GameObject target)
        {
            //人物闪烁，表示攻击
            if (mRenderer == null)
            {
                mRenderer = GetComponent<SpriteRenderer>();
            }
            mRenderer.DOFade(0, duration).OnComplete(() =>
            {
                mRenderer.DOFade(255, duration);
            });
        }
    }
}
