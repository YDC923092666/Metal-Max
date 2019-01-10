using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class BasePanel : MonoBehaviour
    {

        /// <summary>
        /// 界面显示出来
        /// </summary>
        public virtual void OnEnter(string content)
        {

        }

        /// <summary>
        /// 界面暂停，禁用交互
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面恢复
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面被关闭
        /// </summary>
        public virtual void OnExit()
        {

        }
    }
}
