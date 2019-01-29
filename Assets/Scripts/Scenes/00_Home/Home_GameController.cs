using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace MetalMax
{
    public class Home_GameController : BaseGameController
    {
        private GameObject canvas;

        protected override void OnBeforeDestroy()
        {

        }

        public override void Init()
        {
            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Prefab/Managers"));
            newGo.name = "Managers";
            DontDestroyOnLoad(newGo);

            //设置分辨率
            UIManager.SetResolution(1920, 1080, 1);

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
                        GameObject chooseArchivePanel = canvas.transform.Find("ChooseArchivePanel").gameObject;
                        chooseArchivePanel.SetActive(true);
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

        protected override void InitCharactor()
        {

        }

        /// <summary>
        /// 当点击"开始游戏"后，跳转到游戏场景
        /// </summary>
        public void OnStartGameButtonClick()
        {
            string nameText = GameObject.FindObjectOfType<InputField>().text;

            //初始化预制体
            GameObject charGo = Instantiate(Resources.Load<GameObject>("Prefab/Char"));
            charGo.name = "Char";
            DontDestroyOnLoad(charGo);

            //设置角色的初始属性
            var charGoScript = charGo.GetComponent<Charactor>();
            charGoScript.nameString = nameText;
            charGoScript.id = GameManager.Instance.charactor.id;
            charGoScript.hp = GameManager.Instance.charactor.initHp;
            charGoScript.attackCount = GameManager.Instance.charactor.attackCount;
            charGoScript.damage = GameManager.Instance.charactor.initDamage;
            charGoScript.defense = GameManager.Instance.charactor.initDefense;
            charGoScript.speed = GameManager.Instance.charactor.initSpeed;
            charGoScript.shootingRate = GameManager.Instance.charactor.initShootingRate;
            charGoScript.escapeRate = GameManager.Instance.charactor.initEscapeRate;
            charGoScript.nameString = GameManager.Instance.charactor.nameString;
            charGoScript.sprite = GameManager.Instance.charactor.sprite;
            charGoScript.battleSprite = GameManager.Instance.charactor.battleSprite;

            charGoScript.initHp = GameManager.Instance.charactor.initHp;
            charGoScript.initDamage = GameManager.Instance.charactor.initDamage;
            charGoScript.initDefense = GameManager.Instance.charactor.initDefense;
            charGoScript.initSpeed = GameManager.Instance.charactor.initSpeed;
            charGoScript.initShootingRate = GameManager.Instance.charactor.initShootingRate;
            charGoScript.initEscapeRate = GameManager.Instance.charactor.initEscapeRate;

            GameManager.nextSceneName = "01_World1_Home";
            //加载loading页面
            SceneManager.LoadScene("Loading");

            /// <summary>
            /// 刚进入游戏时，第一次初始化
            /// 初始化easytouch,ui,managers等
            /// </summary>
            void GameInit()
            {
                //初始化UICanvas，包含了easytouch和button
                GameObject UICanvasGo = Instantiate(Resources.Load<GameObject>("Prefab/UI/UICanvas"));
                UICanvasGo.name = "UICanvas";
                //设置渲染当前UI的相机
                UICanvasGo.GetComponent<Canvas>().worldCamera = Camera.main;
                DontDestroyOnLoad(UICanvasGo);

                //给Managers添加UIManager脚本
                GameObject managersGo = GameObject.FindGameObjectWithTag(Tags.managers);
                managersGo.AddComponent<UIManager>();

                //置为true表示已经初始化过游戏
                GameManager.isInitGame = true;
            }
        }
    }
}
