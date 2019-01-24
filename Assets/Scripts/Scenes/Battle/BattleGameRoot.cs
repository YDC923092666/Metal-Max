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
            GameObject.Find("Controllers").AddComponent<SaveManager>();
            //构建角色对象
            PersonStatus player1 = new PersonStatus()
            {
                personName = "超级无敌大帅哥",
                personLv = 1,
                personCurrentHp = 20,
                personMaxHp = 20,
                personDamage = 10,
                personDefense = 10,
                personSpeed = 1,
                personExp = 1,
                personCurrentLvNeedExp = 20
            };

            TankStatus tankStatus = new TankStatus()
            {
                tankID = 1,
                tankName = "飞毛腿"
            };
            //保存角色姓名，初始化玩家等级为1
            Archive archive = new Archive()
            {
                id = 1,
                personStatus = player1,
                currentEquipTankID = 1,
                isEquipTank = true,
                isOnTank = true,
                tankStatusList = new List<TankStatus>()
                {
                    tankStatus
                }

            };
            SaveManager.SaveCurrentArchive(archive);

            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = new GameObject();
            newGo.name = "Managers";
            GameManager gameManager = newGo.AddComponent<GameManager>();
            gameManager.enabled = true;
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[0]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[1]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[2]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[3]);

            //创建一个tag为charactor的人物在场景中
            GameObject newGo2 = Instantiate(Resources.Load<GameObject>("Prefab/Char"));
            newGo2.name = "Charactor";
            newGo2.GetComponent<BaseAttr>().id = 1;
            newGo2.GetComponent<BaseAttr>().hp = 20;
            newGo2.GetComponent<BaseAttr>().damage = 1;
            newGo2.GetComponent<BaseAttr>().defense = 1;
            newGo2.GetComponent<BaseAttr>().shootingRate = 1;
            newGo2.GetComponent<BaseAttr>().escapeRate = 1;
            newGo2.GetComponent<BaseAttr>().speed = 1;
            newGo2.GetComponent<BaseAttr>().attackCount = 1;


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
