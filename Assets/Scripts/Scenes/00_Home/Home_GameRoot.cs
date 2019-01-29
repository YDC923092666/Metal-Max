using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class Home_GameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            Home_GameController gm = GetComponent<Home_GameController>();
            gm.Init();
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