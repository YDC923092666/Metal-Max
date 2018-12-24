using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class GameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //测试json文件的读取
            //TODO

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