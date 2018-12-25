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
            Archive archive = new Archive();
            archive.TeamPersonCount = 1;
            Dictionary<string, PersonEquipment> personEquipmentDict = new Dictionary<string, PersonEquipment>();
            personEquipmentDict.Add("Head",)
            PersonStatus player1 = new PersonStatus()
            {
                PersonLevel = 1,
                PersonHp = 10,
                PersonDamage = 10,
                PersonDefence = 10,
                personEquipmentDict = new Dictionary<string, PersonEquipment>() { }
            }
            SaveManager.SaveObject2JsonFile(tank);
        }

        protected override void LaunchInTestMode()
        {
        }

        protected override void LaunchInProductionMode()
        {
            DontDestroyOnLoad(this);
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            print("Game Init...");
            gm.Init();
        }
    }
}