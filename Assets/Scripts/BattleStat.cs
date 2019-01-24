using UnityEngine;
using DG.Tweening;

namespace MetalMax
{
	public abstract class BattleStat : MonoBehaviour 
	{
        public BaseAttr status;

        public float duration = 0.5f;

        protected SpriteRenderer mRenderer;

        public virtual void Start()
        {
            mRenderer = GetComponent<SpriteRenderer>();
            //mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }

        public virtual void ReceiveDamage(int mValue)
        {
            status.hp -= mValue;
            if (status.hp < 0)
            {
                status.hp = 0;
            }
            mRenderer.DOFade(0, duration).SetLoops(1, LoopType.Yoyo);
        }

        public virtual bool IsDead()
        {
            if (status.hp == 0)
            {
                return true;
            }
            return false;
        }

        public virtual void Attack()
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
