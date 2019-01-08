using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Chest : MonoBehaviour 
	{
        public int containedItemId; //箱子包含的物品的ID

		public int OpenChest()
        {
            var clip = Resources.Load<Sprite>("Sprites/chest_open");
            GetComponent<SpriteRenderer>().sprite = clip;
            return containedItemId;
        }
	}
}
