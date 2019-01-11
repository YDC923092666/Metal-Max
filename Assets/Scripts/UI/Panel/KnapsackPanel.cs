using UnityEngine;
using System.Collections;

namespace MetalMax
{
    public class KnapsackPanel : BasePanel
    {
        #region 单例模式
        private static KnapsackPanel _instance;
        public static KnapsackPanel Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

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
        /// 这个方法用来找到一个空的物品槽
        /// </summary>
        /// <returns></returns>
        protected Slot FindEmptySlot()
        {
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount == 0)
                {
                    return slot;
                }
            }
            return null;
        }

        protected Slot FindSameIdSlot(Item item)
        {
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount >= 1 && slot.GetItemId() == item.id && slot.IsFilled() == false)
                {
                    return slot;

                }
            }
            return null;
        }

        public bool StoreItem(int id)
        {
            Item item = InventoryManager.Instance.GetItemById(id);
            return StoreItem(item);
        }

        public bool StoreItem(Item item)
        {
            if (item == null)
            {
                Debug.LogWarning("要存储的物品的id不存在");
                return false;
            }
            if (item.capacity == 1)
            {
                Slot slot = FindEmptySlot();
                if (slot == null)
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
                else
                {
                    slot.StoreItem(item);//把物品存储到这个空的物品槽里面
                }
            }
            else
            {
                Slot slot = FindSameIdSlot(item);
                if (slot != null)
                {
                    slot.StoreItem(item);
                }
                else
                {
                    Slot emptySlot = FindEmptySlot();
                    if (emptySlot != null)
                    {
                        emptySlot.StoreItem(item);
                    }
                    else
                    {
                        Debug.LogWarning("没有空的物品槽");
                        return false;
                    }
                }
            }
            return true;
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
        }

        public override void OnPause()
        {
            
        }

        public override void OnResume()
        {
            
        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isShow = false;
        }
    }
}