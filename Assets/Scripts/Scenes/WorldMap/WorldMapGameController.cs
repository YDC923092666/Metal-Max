using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class WorldMapGameController : BaseGameController
    {
        public override void Init()
        {
            base.Init();
            var canvas = GameObject.Find("Canvas");
            DontDestroyOnLoad(canvas);
        }

        protected override void InitCharactor()
        {
            //初始化预制体
            GameObject charGo = Instantiate(Resources.Load<GameObject>("Prefab/Char"));
            charGo.name = "Char";
            DontDestroyOnLoad(charGo);

            //设置角色的初始属性
            var charGoScript = charGo.GetComponent<Charactor>();
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
        }

        protected override void OnBeforeDestroy()
        {
            
        }
    }
}
