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
        #endregion

        protected override void Awake()
        {
            base.Awake();
            ParseItemJson();
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
    }
}