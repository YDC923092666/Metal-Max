using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class Archive
    {
        private int teamPersonCount;
        private PersonStatus[] personStatus;
        private TankStatus[] tankStatus;
        private Boss[] boss; //消灭的BOSS列表
        private string sceneName;   //最后所在的场景
        private float[] position;   //最后保存的位置

        public int TeamPersonCount
        {
            get
            {
                return teamPersonCount;
            }

            set
            {
                teamPersonCount = value;
            }
        }

        public PersonStatus[] PersonStatus
        {
            get
            {
                return personStatus;
            }

            set
            {
                personStatus = value;
            }
        }

        public TankStatus[] TankStatus
        {
            get
            {
                return tankStatus;
            }

            set
            {
                tankStatus = value;
            }
        }

        public Boss[] Boss
        {
            get
            {
                return boss;
            }

            set
            {
                boss = value;
            }
        }

        public string SceneName
        {
            get
            {
                return sceneName;
            }

            set
            {
                sceneName = value;
            }
        }

        public float[] Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }
    }

    public class PersonStatus
    {
        private int personLevel; // 等级
        private int personHp; //生命值
        private int personDamage; //攻击力
        private int personDefense; //防御力

        public Dictionary<PersonEquipmentType, PersonEquipment> personEquipmentDict; //装备位置和对应的装备对象

        public int PersonLevel
        {
            get
            {
                return personLevel;
            }

            set
            {
                personLevel = value;
            }
        }

        public int PersonHp
        {
            get
            {
                return personHp;
            }

            set
            {
                personHp = value;
            }
        }

        public int PersonDamage
        {
            get
            {
                return personDamage;
            }

            set
            {
                personDamage = value;
            }
        }

        public int PersonDefence
        {
            get
            {
                return personDefense;
            }

            set
            {
                personDefense = value;
            }
        }
    }

    public class TankStatus
    {
        private int tankHp; //生命值
        private int tankDamage; //攻击力
        private int tankDefense; //防御力

        public Dictionary<TankEquipmentType, TankEquipment> tankEquipmentDict; //坦克装备位置和对应的装备对象
        public Dictionary<TankBulletType, int> tankBulletCountDict; //炮弹的类型和数量
        public int TankHp
        {
            get
            {
                return tankHp;
            }

            set
            {
                tankHp = value;
            }
        }

        public int TankDamage
        {
            get
            {
                return tankDamage;
            }

            set
            {
                tankDamage = value;
            }
        }

        public int TankDefense
        {
            get
            {
                return tankDefense;
            }

            set
            {
                tankDefense = value;
            }
        }
    }
}
