using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using LitJson;

namespace MetalMax
{
	public class GameManager : MonoBehaviourSimplify 
	{
        private GameObject canvas;

        protected override void OnBeforeDestroy()
        {

        }

        public override void Init()
        {
            //设置分辨率
            GUIManager.Instance.SetResolution(1920, 1080, 1);

            canvas = GameObject.Find("Canvas");
            //显示健康游戏提醒。
            GameObject jiankangyouxiPanel = canvas.transform.Find("JiankongyouxiPanel").gameObject;
            jiankangyouxiPanel.SetActive(true);

            Delay(3.0f, () =>
            {
                Destroy(jiankangyouxiPanel);
                GameObject BGPanel = canvas.transform.Find("BGPanel").gameObject;
                BGPanel.SetActive(true);
                Text text = BGPanel.GetComponentInChildren<Text>();
                text.DOFade(1, 2);
            
                //播放BGM
                AudioSource BGMAudioSource = Camera.main.GetComponent<AudioSource>();
                AudioManager.Instance.PlayBGM("Sound", "Bgm_JieMian", BGMAudioSource);
            });

            Delay(6.0f, () =>
            {
                //检查版本更新（右下角显示“正在检查更新……”）。
                if (Check4Update())
                {
                    //TODO
                    //有更新
                }
                else
                {
                    //无更新
                    //检查是否有存档。无存档则进入游戏角色名字创建页面。有存档则进入选择存档页面
                    if (SaveManager.Check4Archive())
                    {
                        //TODO
                        //有存档
                    }
                    else
                    {
                        //无存档
                        //进入游戏角色名字创建页面
                        GameObject createRolePanel = canvas.transform.Find("CreateRolePanel").gameObject;
                        createRolePanel.SetActive(true);
                    }
                }
            });
        }

        /// <summary>
        /// 检查是否有最新版本需要更新
        /// </summary>
        /// <returns>true则代表需要更新</returns>
        private bool Check4Update()
        {
            //TODO
            return false;
        }

        /// <summary>
        /// 当点击"开始游戏"后，跳转到游戏场景
        /// </summary>
        public void OnStartGameButtonClick()
        {
            //保存角色姓名，初始化玩家等级为1

            //进入游戏场景
            SceneManager.LoadScene("");
        }

    }
}
