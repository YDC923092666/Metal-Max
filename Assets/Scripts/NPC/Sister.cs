using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Sister : NPCBase 
	{
        protected override void Start()
        {
            base.Start();
            print(info.Name);
        }
    }
}
