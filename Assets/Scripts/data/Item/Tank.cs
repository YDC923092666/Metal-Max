using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    [Serializable]
    public class TankJson
    {
        public List<Tank> infoList;
    }

    [Serializable]
    public class Tank : Item, ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize()
        {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), typeString);
            itemQuality = (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQualityString);
        }

        public void OnBeforeSerialize()
        {

        }

        public override string GetToolTipText()
        {
            string color = "";
            switch (itemQuality)
            {
                case ItemQuality.Common:
                    color = "white";
                    break;
                case ItemQuality.Uncommon:
                    color = "lime";
                    break;
                case ItemQuality.Rare:
                    color = "orange";
                    break;
            }
            string text = string.Format("<color={2}>{0}</color>\n<color=yellow><size=15>{1}</size></color>", name, description, color);
            return text;
        }
    }
}
