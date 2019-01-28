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
        public float initShootingRate; //命中率
        public float initEscapeRate;  //躲避率
        public string battleSprite;

        public int lv = 1;
        public int maxHp;   //最大生命值
        public int exp = 1; //当前经验
        public int maxExp = 20; //升级所需经验

        private void Start()
        {
            maxHp = initHp;
        }

        /// <summary>
        /// 给定一个等级，获取lv对象
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public Lv GetLvItem(int lv)
        {
            var lvTable = GameManager.Instance.lvList;
            Lv lvItem = null;
            foreach (var item in lvTable)
            {
                if (item.lv == lv)
                {
                    lvItem = item;
                    break;
                }
            }
            return lvItem;
        }

        /// <summary>
        /// 检查是否可以升级
        /// </summary>
        public bool Check4Upgrade(int exp)
        {
            //修改当前人物经验值
            this.exp += exp;
            if (this.exp > this.maxExp)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 升级
        /// </summary>
        public void Upgrade()
        {
            //如果当前经验比当前等级升级所需经验高，则代表还需要再升级
            while(this.exp > this.maxExp)
            {
                var thisLv = GetLvItem(this.lv);
                this.exp -= this.maxExp;
                this.lv++;
                this.maxExp = thisLv.exp;
                //每升一级，增加额外的属性
                this.hp += thisLv.addHp;
                this.damage += thisLv.addDamage;
                this.defense += thisLv.addDefense;
                this.speed += thisLv.addSpeed;
                this.shootingRate += thisLv.addShootingRate;
                this.escapeRate += thisLv.addEscapeRate;
            }
            print(this.lv);
            print(this.maxExp);
            print(this.shootingRate);
        }
    }
}
