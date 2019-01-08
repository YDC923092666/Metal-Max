using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public abstract class BaseGameController : MonoBehaviour
    {
        #region Delay
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
        #endregion


        #region
        private void OnDestroy()
        {
            OnBeforeDestroy();
        }

        
        public virtual void Init()
        {
            InitCharactor();
        }

        protected abstract void OnBeforeDestroy();

        /// <summary>
        /// 初始化角色
        /// </summary>
        protected abstract void InitCharactor();
        #endregion
    }
}