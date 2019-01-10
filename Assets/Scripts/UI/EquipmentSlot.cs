using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace MetalMax
{
    public class EquipmentSlot : Slot
    {
        public PersonEquipmentType equipType;

        //判断item是否适合放在这个位置
        public bool IsRightItem(Item item)
        {
            if (item is PersonEquipment && (((PersonEquipment)item).personEquipmentType == equipType))
            {
                return true;
            }
            return false;
        }
    }
}