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

        }

        protected override void LaunchInProductionMode()
        {
        }

        protected override void LaunchInTestMode()
        {
        }
    }
}
