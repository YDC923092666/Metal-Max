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
        }

        public abstract void ReceiveDamage(int mValue);

        public virtual bool IsDead()
        {
            return (status.hp == 0) ? true : false;
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
