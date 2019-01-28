using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class BattleGameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = new GameObject();
            newGo.name = "Managers";
            GameManager gameManager = newGo.AddComponent<GameManager>();
            gameManager.enabled = true;
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[0]);
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[1]);
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[2]);
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[3]);

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


            BattleGameController.Instance.StartGame();
        }

        protected override void LaunchInProductionMode()
        {
        }

        protected override void LaunchInTestMode()
        {
        }
    }
}
