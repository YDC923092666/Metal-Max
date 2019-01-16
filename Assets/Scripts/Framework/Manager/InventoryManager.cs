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
        public List<Item> itemList;

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
            ////读取人物装备
            //string personJson = SaveManager.GetJsonStringFromFile(Const.personEquipmentInfoFilePath);
            //var personJsonObject = JsonUtility.FromJson<PersonEquipmentJson>(personJson);
            //foreach (var item in personJsonObject.infoList)
            //{
            //    itemList.Add(item);
            //}

            //读取坦克装备
            string tankEquipJson = SaveManager.GetJsonStringFromFile(Const.tankEquipmentInfoFilePath);
            var tankEquipJsonObject = JsonUtility.FromJson<TankEquipmentJson>(tankEquipJson);
            foreach (var item in tankEquipJsonObject.infoList)
            {
                itemList.Add(item);
            }

            //读取坦克
            string tankJson = SaveManager.GetJsonStringFromFile(Const.tankFilePath);
            var tankJsonObject = JsonUtility.FromJson<TankJson>(tankJson);
            foreach (var item in tankJsonObject.infoList)
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