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

    public class TankEquipment:ISerializationCallbackReceiver
	{
        public int id;
        public string name;
        [NonSerialized]
        public TankEquipmentType tankEquipmentType; //装备类型
        public string tankEquipmentTypeString;    
        public int hp;
        public int damage;
        public int defense;

        public void OnAfterDeserialize()
        {
            TankEquipmentType type = (TankEquipmentType)System.Enum.Parse(typeof(TankEquipmentType), tankEquipmentTypeString);
            tankEquipmentType = type;
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}