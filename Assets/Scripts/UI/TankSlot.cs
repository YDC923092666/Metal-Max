using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MetalMax
{
	public class TankSlot : Slot
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                UIManager.Instance.selectedSlot = GetComponent<Slot>();
                string toolTipText = item.GetToolTipText();
                UIManager.Instance.ShowItemInfo4CharPanel(toolTipText, item); //装备面板，显示“卸下”的按钮
            }
            else
            {
                UIManager.Instance.HideItemInfoPanel();
                UIManager.Instance.selectedSlot = null;
            }
        }
    }
}
