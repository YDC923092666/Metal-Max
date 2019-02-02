using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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

        protected GameObject charGo;
        protected GameObject UICanvasGo;

        #region
        private void OnDestroy()
        {
            OnBeforeDestroy();
        }
        
        public virtual void Init()
        {
            InitUI();
            InitCharactor();
            InitCam();
            //初始化完毕后，后续再跳转不需要再初始化
            GameManager.isNewGame = false;

            //GameManager.charactorInScene = charGo;
            //GameManager.UICanvasInScene = UICanvasGo;
        }

        protected virtual void OnBeforeDestroy()
        {
            UICanvasGo = UIManager.Instance.CanvasTransform.gameObject;
            UICanvasGo.SetActive(false);
        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        protected virtual void InitCharactor()
        {
            if(charGo == null)
            {
                //如果是读档，则读取存档里的角色
                if (GameManager.isReadArchive)
                {
                    //TODO
                }
                else if(GameManager.isNewGame) //如果是新游戏
                {
                    //初始化预制体
                    charGo = Instantiate(Resources.Load<GameObject>("Prefab/Char"));
                    charGo.name = "Char";
                    DontDestroyOnLoad(charGo);

                    //设置角色的初始属性
                    var charGoScript = charGo.GetComponent<Charactor>();
                    charGoScript.nameString = GameManager.charactor.nameString;
                    charGoScript.id = GameManager.charactor.id;
                    charGoScript.hp = GameManager.charactor.initHp;
                    charGoScript.attackCount = GameManager.charactor.attackCount;
                    charGoScript.damage = GameManager.charactor.initDamage;
                    charGoScript.defense = GameManager.charactor.initDefense;
                    charGoScript.speed = GameManager.charactor.initSpeed;
                    charGoScript.shootingRate = GameManager.charactor.initShootingRate;
                    charGoScript.escapeRate = GameManager.charactor.initEscapeRate;
                    charGoScript.nameString = GameManager.charactor.nameString;
                    charGoScript.sprite = GameManager.charactor.sprite;
                    charGoScript.battleSprite = GameManager.charactor.battleSprite;

                    charGoScript.initHp = GameManager.charactor.initHp;
                    charGoScript.initDamage = GameManager.charactor.initDamage;
                    charGoScript.initDefense = GameManager.charactor.initDefense;
                    charGoScript.initSpeed = GameManager.charactor.initSpeed;
                    charGoScript.initShootingRate = GameManager.charactor.initShootingRate;
                    charGoScript.initEscapeRate = GameManager.charactor.initEscapeRate;

                    //设置角色的位置
                    charGo.transform.position = GameObject.Find("Map/Points/Default").transform.position;
                }
                else //如果是场景跳转
                {
                    charGo = GameObject.FindGameObjectWithTag(Tags.charactor);
                    //charGo.name = "Char";
                    var sceneName = PlayerPrefs.GetString("sceneName");
                    if (SceneManager.GetActiveScene().name == sceneName)
                    {
                        var spawnPoint = PlayerPrefs.GetString("spawnPoint");
                        if (spawnPoint == null)
                        {
                            spawnPoint = "Default";
                        }
                        charGo.transform.position = GameObject.Find("Map/Points/" + spawnPoint).transform.position;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化UI
        /// </summary>
        protected virtual void InitUI()
        {
            if(UICanvasGo == null)
            {
                if (GameManager.isReadArchive)
                {
                    //TODO
                }
                else if (GameManager.isNewGame)
                {
                    //初始化UICanvas，包含了easytouch和button
                    UICanvasGo = Instantiate(Resources.Load<GameObject>("Prefab/UI/UICanvas"));
                    DontDestroyOnLoad(UICanvasGo);

                    GameObject managersGo = GameObject.FindGameObjectWithTag(Tags.managers);
                    managersGo.AddComponent<UIManager>();
                }
                else
                {
                    UICanvasGo = UIManager.Instance.CanvasTransform.gameObject;
                    UICanvasGo.SetActive(true);
                }

                UICanvasGo.name = "UICanvas";
                //设置渲染当前UI的相机
                UICanvasGo.GetComponent<Canvas>().worldCamera = Camera.main;
            }
        }

        protected virtual void InitCam()
        {
            var vCam = GameObject.FindGameObjectWithTag(Tags.vCam).GetComponent<CinemachineVirtualCamera>();

            vCam.m_Follow = charGo.transform;
            vCam.m_Lens.OrthographicSize = 5f;
        }
        #endregion
    }
}