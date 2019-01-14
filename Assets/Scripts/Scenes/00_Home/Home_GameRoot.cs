using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace MetalMax
{
    public class Home_GameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //测试json文件的读取
            //TODO
            //string filePath = "Resources/Data/PersonEquipmentInfo.json";
            //TankEquipment[] tankEquipmentArray = SaveManager.GetObjectFromJsonString<TankEquipment>(filePath);

            //print(tankEquipmentArray[2].Name);

            //构建角色列表
            //List<PersonStatus> personStatusList = new List<PersonStatus>();
            //PersonStatus player1 = new PersonStatus()
            //{
            //    personLevel = 1,
            //    personHp = 10,
            //    personDamage = 10,
            //    personDefense = 10,
            //    personEquipmentDict = new Dictionary<PersonEquipmentType, int>()
            //    {
            //        {PersonEquipmentType.Head, 1},
            //        {PersonEquipmentType.Breastplate, 2 },
            //        {PersonEquipmentType.Hands, 3 },
            //        {PersonEquipmentType.Pant, 4 },
            //        {PersonEquipmentType.Foot, 5 },
            //        {PersonEquipmentType.Gun, 6 }
            //    }
            //};
            //personStatusList.Add(player1);

            ////构建tank列表
            //List<TankStatus> tankStatusList = new List<TankStatus>();
            //TankStatus tank1 = new TankStatus()
            //{
            //    tankDamage = 100,
            //    tankDefense = 100,
            //    tankHp = 200,
            //    tankBulletCountDict = new Dictionary<TankBulletType, int>()
            //    {
            //        {TankBulletType.Nomal, 1}
            //    },
            //    tankEquipmentDict = new Dictionary<TankEquipmentType, int>()
            //    {
            //        {TankEquipmentType.Main,1 },
            //        {TankEquipmentType.Second,2 },
            //        {TankEquipmentType.Engine,3 },
            //        {TankEquipmentType.Chassis,4 },
            //    }
            //};
            //tankStatusList.Add(tank1);

            ////构建Boss列表
            //List<Boss> bossList = new List<Boss>();
            //Boss boss = new Boss()
            //{
            //    id = 1,
            //    name = "洞穴守卫者",
            //    hp = 200,
            //    damage = 20,
            //    defense = 20,
            //    criticalRate = 2,
            //    skillList = new List<BossSkill> { SpecialSkill.Null }
            //};
            //bossList.Add(boss);

            //List<Archive> archiveList = new List<Archive>();
            //Archive archive = new Archive()
            //{
            //    id = 1,
            //    teamPersonCount = 1,
            //    sceneName = "01",
            //    position = new double[] { 0, 0, 0 },
            //    archiveDateTime = DateTime.Now,
            //    personStatusList = personStatusList,
            //    tankStatusList = tankStatusList,
            //    bossList = bossList
            //};
            //archiveList.Add(archive);

            //SaveManager.SaveArchiveList(archiveList);
        }

        protected override void LaunchInTestMode()
        {
            GetComponent<Test>().TestArray();
        }

        protected override void LaunchInProductionMode()
        {
            Home_GameController gm = GetComponent<Home_GameController>();
            gm.Init();
        }
    }
}