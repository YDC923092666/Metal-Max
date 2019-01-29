using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private GameObject charGo;

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
        protected virtual void InitCharactor()
        {
            if(charGo == null)
            {
                charGo = GameObject.FindGameObjectWithTag(Tags.charactor);
            }

            //如果是读档，则读取存档里的场景和角色位置
            if (GameManager.isReadArchive)
            {

            }
            else //如果不是读档，是正常的场景跳转
            {
                var sceneName = PlayerPrefs.GetString("sceneName");
                if(SceneManager.GetActiveScene().name == sceneName)
                {
                    var spawnPoint = PlayerPrefs.GetString("spawnPoint");
                    charGo.transform.position = GameObject.Find("Map/Points/" + spawnPoint).transform.position;
                }
            }
        }
        #endregion
    }
}