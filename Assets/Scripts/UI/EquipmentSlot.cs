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

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                UIManager.selectedSlot = GetComponent<Slot>();
                string toolTipText = item.GetToolTipText();
                UIManager.Instance.ShowItemInfoPanel(toolTipText); //装备面板，显示“卸下”的按钮
            }
            else
            {
                UIManager.Instance.HideItemInfoPanel();
                UIManager.selectedSlot = null;
            }
        }
    }
}