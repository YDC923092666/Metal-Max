using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace MetalMax
{
    public class GameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //测试json文件的读取
            //TODO
            //string filePath = "Resources/Data/PersonEquipmentInfo.json";
            //TankEquipment[] tankEquipmentArray = SaveManager.GetObjectFromJsonString<TankEquipment>(filePath);

            //print(tankEquipmentArray[2].Name);

            //构建角色列表
            List<PersonStatus> personStatusList = new List<PersonStatus>();
            PersonStatus player1 = new PersonStatus()
            {
                PersonLevel = 1,
                PersonHp = 10,
                PersonDamage = 10,
                PersonDefence = 10,
                PersonEquipmentDict = new Dictionary<string, int>()
                {
                    {"Head", 1},
                    {"Breastplate", 2 },
                    {"Hands", 3 },
                    {"Pant", 4 },
                    {"Foot", 5 },
                    {"Gun", 6 }
                }
            };
            personStatusList.Add(player1);

            //构建tank列表
            List<TankStatus> tankStatusList = new List<TankStatus>();
            TankStatus tank1 = new TankStatus()
            {
                TankDamage = 100,
                TankDefense = 100,
                TankHp = 200,
                TankBulletCountDict = new Dictionary<string, int>()
                {
                    {"Nomal", 1}
                },
                TankEquipmentDict = new Dictionary<string, int>()
                {
                    {"Main",1 },
                    {"Second",2 },
                    {"Engine",3 },
                    {"Chassis",4 },
                }
            };
            tankStatusList.Add(tank1);

            //构建Boss列表
            List<Boss> bossList = new List<Boss>();
            Boss boss = new Boss()
            {
                Id = 1,
                Name = "洞穴守卫者",
                Hp = 200,
                Damage = 20,
                Defense = 20,
                CriticalRate = 2,
                Skill = new List<string> { "Null" }
            };
            bossList.Add(boss);

            List<Archive> archiveList = new List<Archive>();
            Archive archive = new Archive()
            {
                Id = 1,
                TeamPersonCount = 1,
                SceneName = "01",
                Position = new double[] { 0, 0, 0 },
                ArchiveDateTime = DateTime.Now,
                PersonStatusList = personStatusList,
                TankStatusList = tankStatusList,
                BossList = bossList
            };
            archiveList.Add(archive);

            SaveManager.SaveArchiveList(archiveList);
        }

        protected override void LaunchInTestMode()
        {
        }

        protected override void LaunchInProductionMode()
        {
            DontDestroyOnLoad(this);
            GameController gm = GameObject.Find("GameController").GetComponent<GameController>();
            print("Game Init...");
            gm.Init();
        }
    }
}