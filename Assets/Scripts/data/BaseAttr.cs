using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class BaseAttr : MonoBehaviour
	{
        public int id;
        public int hp;
        public int attackCount; //攻击次数
        public int damage;
        public int defense;
        public int speed;
        public float shootingRate; //命中率
        public float escapeRate;  //躲避率
        public string nameString;
        public string sprite;
    }
}