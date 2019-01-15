using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    [Serializable]
    public class SpecialJson
    {
        public List<Tank> infoList;
    }

    [Serializable]
    public class Tank: Item, ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize()
        {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), typeString);
            itemQuality = (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQualityString);
        }

        public void OnBeforeSerialize()
        {
            
        }
	}
}