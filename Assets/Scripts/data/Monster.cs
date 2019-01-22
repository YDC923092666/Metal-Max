using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class MonsterJson
    {
        public List<Monster> infoList;
    }

	public class Monster
	{
        public string name;
        public int id;
        public int hp;
        public int attackCount; //攻击次数
        public int damage;
        public int defense;
        public int speed;
        public int shootingRate; //命中率
        public int escapeRate;  //躲避率
        public string sprite;
	}
}
