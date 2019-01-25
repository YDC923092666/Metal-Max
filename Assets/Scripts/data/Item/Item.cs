using UnityEngine;
using System;
using System.Collections;

namespace MetalMax
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,
        PersonEquipment,
        Special,
    }

    /// <summary>
    /// 品质
    /// </summary>
    public enum ItemQuality
    {
        Common,
        Uncommon,
        Rare
    }

    /// <summary>
    /// 物品基类
    /// </summary>
    [Serializable]
    public class Item
    {
        public int id; 
        public string name; 
        [NonSerialized]
        public ItemType itemType;
        public string typeString;
        [NonSerialized]
        public ItemQuality itemQuality;
        public string itemQualityString;
        public string description;
        public int capacity;
        public int buyPrice;
        public int sellPrice;
        public string sprite;

        /// <summary> 
        /// 得到提示面板应该显示什么样的内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetToolTipText()
        {
            string color = "";
            switch (itemQuality)
            {
                case ItemQuality.Common:
                    color = "white";
                    break;
                case ItemQuality.Uncommon:
                    color = "lime";
                    break;
                case ItemQuality.Rare:
                    color = "orange";
                    break;
            }
            string text = string.Format("<color={4}>{0}</color>\n<size=10><color=green>购买价格：{1} 出售价格：{2}</color></size>\n<color=yellow><size=15>{3}</size></color>", name, buyPrice, sellPrice, description, color);
            return text;
        }

        public virtual void Use()
        {

        }
    }
}