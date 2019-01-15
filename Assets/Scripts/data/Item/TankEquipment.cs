using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


#pragma warning disable 0649
namespace MetalMax
{
    /// <summary>
    /// 坦克装备类型
    /// </summary>
    public enum TankEquipmentType
    {
        Main, //主炮
        Second, //副炮
        Engine, //发动机 - 决定最大装甲片值
        Chassis, //底盘 - 决定防御力
    }

    public enum TankBulletType
    {
        Nomal, //普通炮弹
        Iron, //铁弹
        Smoke   //烟雾弹
    }

    [Serializable]
    class TankEquipmentJson
    {
        public List<TankEquipment> infoList;
    }

    [Serializable]
    public class TankEquipment: Item, ISerializationCallbackReceiver
	{
        [NonSerialized]
        public TankEquipmentType tankEquipmentType; //装备类型
        public string equiptype;    
        public int sp;
        public int damage;
        public int defense;

        public override string GetToolTipText()
        {
            string text = base.GetToolTipText();

            string equipTypeText = "";
            switch (tankEquipmentType)
            {
                case TankEquipmentType.Main:
                    equipTypeText = "主炮";
                    break;
                case TankEquipmentType.Second:
                    equipTypeText = "副炮";
                    break;
                case TankEquipmentType.Engine:
                    equipTypeText = "引擎";
                    break;
                case TankEquipmentType.Chassis:
                    equipTypeText = "底盘";
                    break;
            }
            string newText = string.Format("{0}\n\n<color=blue><size=15>装备类型：{1}\n最大装甲片数：{2}\n攻击力：{3}\n防御力：{4}</size></color>", text, equipTypeText, sp, damage, defense);

            return newText;
        }

        public void OnAfterDeserialize()
        {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), typeString);
            itemQuality = (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQualityString);
            tankEquipmentType = (TankEquipmentType)System.Enum.Parse(typeof(TankEquipmentType), equiptype);
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}