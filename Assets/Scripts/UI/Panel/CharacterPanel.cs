using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class CharacterPanel : BasePanel
    {
        private static CharacterPanel _instance;
        public static CharacterPanel Instance
        {
            get
            {
                return _instance;
            }
        }

        protected Slot[] slotList;

        protected virtual void Awake()
        {
            _instance = this;
        }

        protected override void Start()
        {
            base.Start();
            slotList = GetComponentsInChildren<Slot>();
        }

        /// <summary>
        /// 装上装备
        /// </summary>
        /// <param name="item"></param>
        public void PutOn(Item item)
        {
            Item exitItem = null;
            foreach (Slot slot in slotList)
            {
                EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
                //找到符合item type的格子
                if (equipmentSlot.IsRightItem(item))
                {
                    //如果格子里有装备了，则交换装备
                    if (equipmentSlot.transform.childCount > 0)
                    {
                        ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                        exitItem = currentItemUI.Item;
                        currentItemUI.SetItem(item, 1);
                    }
                    //如果没有装备，则装备上
                    else
                    {
                        equipmentSlot.StoreItem(item);
                    }
                    break;
                }
            }
            //如果对象不为空，则代表有装备被换下来，换下来的装备要装入背包中
            if (exitItem != null)
            {
                KnapsackPanel.Instance.StoreItem(exitItem);
            }
        }

        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="item"></param>
        public void PutOff(Item item)
        {
            KnapsackPanel.Instance.StoreItem(item);
        }

        public override void OnEnter(string content)
        {
            
        }

        public override void OnPause()
        {
            
        }

        public override void OnResume()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}