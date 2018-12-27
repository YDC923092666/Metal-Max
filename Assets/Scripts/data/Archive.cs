using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    [Serializable]
    public class Archive
    {
        private int id;
        private int teamPersonCount;
        private string sceneName;   //最后所在的场景
        private double[] position;   //最后保存的位置
        private DateTime archiveDateTime; //存档时间
        private List<PersonStatus> personStatusList;
        private List<TankStatus> tankStatusList;
        private List<Boss> bossList; //消灭的BOSS列表

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

        public List<PersonStatus> PersonStatusList
        {
            get
            {
                return personStatusList;
            }

            set
            {
                personStatusList = value;
            }
        }

        public List<TankStatus> TankStatusList
        {
            get
            {
                return tankStatusList;
            }

            set
            {
                tankStatusList = value;
            }
        }

        public List<Boss> BossList
        {
            get
            {
                return bossList;
            }

            set
            {
                bossList = value;
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

        public double[] Position
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

        public DateTime ArchiveDateTime
        {
            get
            {
                return archiveDateTime;
            }

            set
            {
                archiveDateTime = value;
            }
        }
    }

    public class PersonStatus
    {
        private string personName; // 角色名
        private int personLevel; // 等级
        private int personHp; //生命值
        private int personDamage; //攻击力
        private int personDefense; //防御力

        private Dictionary<string, int> personEquipmentDict; //装备位置和对应的装备对象

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

        public Dictionary<string, int> PersonEquipmentDict
        {
            get
            {
                return personEquipmentDict;
            }

            set
            {
                personEquipmentDict = value;
            }
        }

        public string PersonName
        {
            get
            {
                return personName;
            }

            set
            {
                personName = value;
            }
        }
    }

    public class TankStatus
    {
        private int tankHp; //生命值
        private int tankDamage; //攻击力
        private int tankDefense; //防御力

        private Dictionary<string, int> tankEquipmentDict; //坦克装备位置和对应的装备对象.int值是id值
        private Dictionary<string, int> tankBulletCountDict; //炮弹的类型和数量

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

        public Dictionary<string, int> TankEquipmentDict
        {
            get
            {
                return tankEquipmentDict;
            }

            set
            {
                tankEquipmentDict = value;
            }
        }

        public Dictionary<string, int> TankBulletCountDict
        {
            get
            {
                return tankBulletCountDict;
            }

            set
            {
                tankBulletCountDict = value;
            }
        }
    }
}
