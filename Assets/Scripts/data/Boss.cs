using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public enum SpecialSkill
    {
        Null,
        Flee, //逃跑
        Destroy, //破坏坦克的主炮
    }

	public class Boss
	{
        private int id;
        private string name;
        private int hp;
        private int damage;
        private int defense;
        private int criticalRate;  //暴击率
        private List<string> skill;

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

        public List<string> Skill
        {
            set
            {
                skill = value;
            }
            get
            {
                return skill;
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

        public int CriticalRate
        {
            get
            {
                return criticalRate;
            }

            set
            {
                criticalRate = value;
            }
        }
    }
}