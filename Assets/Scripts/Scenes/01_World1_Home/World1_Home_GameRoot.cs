using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            throw new NotImplementedException();
        }
    }
}
