using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class TankEquipment
	{
        private int id;
        private string name;
        private TankEquipmentType type;    //装备类型
        private int hp;
        private int damage;
        private int defense;

        public string Type
        {
            set
            {
                type = (TankEquipmentType)System.Enum.Parse(typeof(TankEquipmentType), value);
            }
        }

        public int Hp
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        public int Defense
        {
            get
            {
                return defense;
            }

            set
            {
                defense = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}