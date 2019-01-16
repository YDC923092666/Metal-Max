using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 物品槽
    /// </summary>
    public class Slot : MonoBehaviour, IPointerClickHandler
    {
        public GameObject itemPrefab;
        /// <summary>
        /// 把item放在自身下面
        /// 如果自身下面已经有item了，amount++
        /// 如果没有 根据itemPrefab去实例化一个item，放在下面
        /// </summary>
        /// <param name="item"></param>
        public void StoreItem(Item item)
        {
            if (transform.childCount == 0)
            {
                GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
                itemGameObject.transform.SetParent(this.transform);
                itemGameObject.transform.localScale = Vector3.one;
                itemGameObject.transform.localPosition = Vector3.zero;
                itemGameObject.GetComponent<ItemUI>().SetItem(item);
            }
            else
            {
                transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
            }
        }

        /// <summary>
        /// 得到当前物品槽存储的物品类型
        /// </summary>
        /// <returns></returns>
        public ItemType GetItemType()
        {
            return transform.GetChild(0).GetComponent<ItemUI>().Item.itemType;
        }

        /// <summary>
        /// 得到物品的id
        /// </summary>
        /// <returns></returns>
        public int GetItemId()
        {
            return transform.GetChild(0).GetComponent<ItemUI>().Item.id;
        }

        public bool IsFilled()
        {
            ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
            return itemUI.Amount >= itemUI.Item.capacity;//当前的数量大于等于容量
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                UIManager.Instance.selectedSlot = GetComponent<Slot>();
                ItemType type = item.itemType;
                string toolTipText = item.GetToolTipText();
                UIManager.Instance.ShowItemInfoPanel(toolTipText, type); //根据不同的物品类型，显示不同的按钮Text（装备or使用）
            }
            else
            {
                UIManager.Instance.HideItemInfoPanel();
                UIManager.Instance.selectedSlot = null;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}