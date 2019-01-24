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
            newGo.AddComponent<GameManager>();
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[0]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[1]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[2]);
            GameManager.battleMonsters.Add(GameManager.Instance.monsterList[3]);

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
