using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class CommonGameRoot : MainManager 
	{
        protected override void LaunchInDevelopingMode()
        {
            //print("加载");
            ////创建空物体Managers，并挂载各种manager脚本
            //GameObject newGo = Instantiate(Resources.Load<GameObject>("Prefab/Managers"));
            //newGo.AddComponent<UIManager>();
            //newGo.name = "Managers";
            //DontDestroyOnLoad(newGo);

            base.LaunchInDevelopingMode();
        }

        protected override void LaunchInProductionMode()
        {
            CommonGameController gm = GetComponent<CommonGameController>();
            gm.Init();
        }

        protected override void LaunchInTestMode()
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/HP3");
            GameObject.Find("Test").GetComponent<SpriteRenderer>().sprite = sprite;

        }
    }
}
