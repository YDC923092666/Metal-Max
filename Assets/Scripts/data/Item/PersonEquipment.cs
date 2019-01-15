using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#pragma warning disable 0649
namespace MetalMax
{
    /// <summary>
    /// 人类装备类型
    /// </summary>
    public enum PersonEquipmentType
    {
        Head,
        Breastplate,
        Hands,
        Pant,
        Foot,
        Weapon
    }

    [Serializable]
    class PersonEquipmentJson
    {
        public List<PersonEquipment> infoList;
    }

    [Serializable]
    public class PersonEquipment : Item, ISerializationCallbackReceiver
    {
        [NonSerialized]
        public PersonEquipmentType personEquipmentType;    //装备类型
        public string equiptype;
        public int hp;
        public int damage;
        public int defense;
        public int speed;

        public override string GetToolTipText()
        {
            base.GetToolTipText();
            string text = base.GetToolTipText();

            string equipTypeText = "";
            switch (personEquipmentType)
            {
                case PersonEquipmentType.Breastplate:
                    equipTypeText = "胸甲";
                    break;
                case PersonEquipmentType.Foot:
                    equipTypeText = "脚部";
                    break;
                case PersonEquipmentType.Hands:
                    equipTypeText = "护手";
                    break;
                case PersonEquipmentType.Head:
                    equipTypeText = "头部";
                    break;
                case PersonEquipmentType.Pant:
                    equipTypeText = "腿部";
                    break;
                case PersonEquipmentType.Weapon:
                    equipTypeText = "武器";
                    break;
            }

            string newText = string.Format("{0}\n\n<color=blue><size=15>装备类型：{1}\n最大生命值：{2}\n攻击力：{3}\n防御力：{4}\n速度：{5}</size></color>", text, equipTypeText, hp, damage, defense, speed);

            return newText;
        }

        public void OnAfterDeserialize()
        {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), typeString);
            itemQuality = (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQualityString);
            personEquipmentType = (PersonEquipmentType)Enum.Parse(typeof(PersonEquipmentType), equiptype);
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}