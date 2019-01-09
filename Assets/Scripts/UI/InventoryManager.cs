using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

namespace MetalMax
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        /// <summary>
        ///  物品信息的列表（集合）
        /// </summary>
        private List<Item> itemList;

        #region ToolTip
        private ToolTip toolTip;

        private bool isToolTipShow = false;

        private Vector2 toolTipPosionOffset = new Vector2(10, -10);
        #endregion

        private Canvas canvas;

        #region PickedItem
        private bool isPickedItem = false;

        public bool IsPickedItem
        {
            get
            {
                return isPickedItem;
            }
        }

        private ItemUI pickedItem;//鼠标选中的物体

        public ItemUI PickedItem
        {
            get
            {
                return pickedItem;
            }
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            ParseItemJson();
        }

        void Start()
        {
            toolTip = GameObject.FindObjectOfType<ToolTip>();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
            pickedItem.Hide();
        }

        void Update()
        {
            if (isToolTipShow)
            {
                //控制提示面板跟随鼠标
                Vector2 position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
                toolTip.SetLocalPotion(position + toolTipPosionOffset);
            }

            //物品丢弃的处理
            if (isPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)
            {
                isPickedItem = false;
                PickedItem.Hide();
            }
        }

        /// <summary>
        /// 解析物品信息
        /// </summary>
        void ParseItemJson()
        {
            itemList = new List<Item>();
            string ta = SaveManager.GetJsonStringFromFile(Const.personEquipmentInfoFilePath);
            PersonEquipmentJson jsonObject = JsonUtility.FromJson<PersonEquipmentJson>(ta);
            foreach (var item in jsonObject.infoList)
            {
                itemList.Add(item);
            }
        }

        public Item GetItemById(int id)
        {
            foreach (Item item in itemList)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public void ShowToolTip(string content)
        {
            if (this.isPickedItem) return;
            isToolTipShow = true;
            toolTip.Show(content);
        }

        public void HideToolTip()
        {
            isToolTipShow = false;
            toolTip.Hide();
        }

        //捡起物品槽指定数量的物品
        public void PickupItem(Item item, int amount)
        {
            PickedItem.SetItem(item, amount);
            isPickedItem = true;

            PickedItem.Show();
            this.toolTip.Hide();
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }

        /// <summary>
        /// 从手上拿掉一个物品放在物品槽里面
        /// </summary>
        public void RemoveItem(int amount = 1)
        {
            PickedItem.ReduceAmount(amount);
            if (PickedItem.Amount <= 0)
            {
                isPickedItem = false;
                PickedItem.Hide();
            }
        }

        //public void SaveInventory()
        //{
        //    Knapsack.Instance.SaveInventory();
        //    Chest.Instance.SaveInventory();
        //    CharacterPanel.Instance.SaveInventory();
        //    Forge.Instance.SaveInventory();
        //    PlayerPrefs.SetInt("CoinAmount", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount);
        //}

        //public void LoadInventory()
        //{
        //    Knapsack.Instance.LoadInventory();
        //    Chest.Instance.LoadInventory();
        //    CharacterPanel.Instance.LoadInventory();
        //    Forge.Instance.LoadInventory();
        //    if (PlayerPrefs.HasKey("CoinAmount"))
        //    {
        //        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
        //    }
        //}

    }
}