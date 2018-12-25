using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Gun
    }

    public class PersonEquipment
	{
        private int id;
        private string name; //装备名字
        private string type;    //装备类型
        private int hp;
        private int damage;
        private int defense;

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

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}