using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetalMax
{
    public class World1_Home_GameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Prefab/Managers"));
            newGo.name = "Managers";
            DontDestroyOnLoad(newGo);

            //构建角色对象
            PersonStatus player1 = new PersonStatus()
            {
                personName = "丶橙子皮皮",
                personLv = 1,
                personCurrentHp = 20,
                personMaxHp = 20,
                personDamage = 10,
                personDefense = 10,
                personSpeed = 1,
                personExp = 1,
                personCurrentLvNeedExp = 20
            };
            //保存角色姓名，初始化玩家等级为1
            Archive archive = new Archive()
            {
                id = 1,
                sceneName = SceneManager.GetActiveScene().name,
                position = new double[] { 0, 0, 0 },
                archiveDateTime = DateTime.Now,
                personStatus = player1
            };
            SaveManager.SaveCurrentArchive(archive);

            World1_Home_GameController gm = GetComponent<World1_Home_GameController>();
            gm.Init();
        }

        protected override void LaunchInProductionMode()
        {
            World1_Home_GameController gm = GetComponent<World1_Home_GameController>();
            gm.Init();
        }

        protected override void LaunchInTestMode()
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/HP3");
            GameObject.Find("Test").GetComponent<SpriteRenderer>().sprite = sprite;
            
        }
    }
}
