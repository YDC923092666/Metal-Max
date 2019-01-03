using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class World1_Home_GameController : BaseGameController
    {
        public override void Init()
        {
            base.Init();
        }

        protected override void InitCharactor()
        {
            print("进入新地图");
        }

        protected override void OnBeforeDestroy()
        {
            
        }
    }
}
