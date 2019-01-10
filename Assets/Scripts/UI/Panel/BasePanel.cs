using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public abstract class BasePanel : MonoBehaviour
    {
        public bool isShow = false;
        protected CanvasGroup canvasGroup;

        protected virtual void Start()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 界面显示出来
        /// </summary>
        public abstract void OnEnter(string content);

        /// <summary>
        /// 界面暂停，禁用交互
        /// </summary>
        public abstract void OnPause();

        /// <summary>
        /// 界面恢复
        /// </summary>
        public abstract void OnResume();

        /// <summary>
        /// 界面被关闭
        /// </summary>
        public abstract void OnExit();
    }
}
