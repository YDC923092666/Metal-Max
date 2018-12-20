using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax {
	public class DelayUtil : MonoBehaviour
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
    }
}
