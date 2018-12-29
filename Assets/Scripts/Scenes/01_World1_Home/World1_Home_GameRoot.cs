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
            throw new NotImplementedException();
        }

        protected override void LaunchInProductionMode()
        {
            World1_Home_GameController gm = GameObject.Find("Controllers").GetComponent<World1_Home_GameController>();
            gm.Init();
        }

        protected override void LaunchInTestMode()
        {
            throw new NotImplementedException();
        }
    }
}
