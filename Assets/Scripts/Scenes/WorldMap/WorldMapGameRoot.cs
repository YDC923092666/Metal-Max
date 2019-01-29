using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class WorldMapGameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Prefab/Managers"));
            newGo.name = "Managers";
            DontDestroyOnLoad(newGo);

            WorldMapGameController gm = GetComponent<WorldMapGameController>();
            gm.Init();
        }

        protected override void LaunchInProductionMode()
        {
            
        }

        protected override void LaunchInTestMode()
        {
            
        }
    }
}
