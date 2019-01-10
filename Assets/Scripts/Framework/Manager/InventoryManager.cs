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

        #region ItemInfoPanel
        private ItemInfoPanel itemInfoPanel;

        private bool isItemInfoPanelShow = false;

        private Vector2 ItemInfoPanelPosionOffset = new Vector2(1, 0);
        #endregion

        private Canvas canvas;

        protected override void Awake()
        {
            base.Awake();
            ParseItemJson();
        }

        void Start()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        void Update()
        {
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

        public void ShowItemInfoPanel(string content, ItemType type)
        {
            isItemInfoPanelShow = true;
            UIManager.Instance.PushPanel(UIPanelType.ItemInfoPanel, content);
            string text = null;
            switch (type)
            {
                case ItemType.Consumable:
                    text = "使用";
                    break;
                case ItemType.PersonEquipment:
                    text = "装备";
                    break;
                case ItemType.TankEquipment:
                    text = "装备";
                    break;
                case ItemType.Special:
                    text = "使用";
                    break;
            }
            UIManager.panelDict[UIPanelType.ItemInfoPanel].gameObject.transform.Find("ButtonGroup/UseButton/Text").GetComponent<Text>().text = text;
        }
    }
}