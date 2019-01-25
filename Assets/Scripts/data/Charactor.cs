using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

namespace MetalMax
{
	public class Charactor : BaseAttr
	{
        public int initHp;
        public int initDamage;
        public int initDefense;
        public int initSpeed;
        public int initShootingRate; //命中率
        public int initEscapeRate;  //躲避率
        public string battleSprite;

        public int lv = 1;
        public int maxHp;   //最大生命值
        public int exp = 1; //当前经验
        public int maxExp = 20; //升级所需经验

        private void Start()
        {
            maxHp = initHp;
        }
    }
}
