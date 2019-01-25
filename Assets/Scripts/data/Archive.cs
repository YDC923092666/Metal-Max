using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace MetalMax
{
    [Serializable]
    class ArchiveJson
    {
        public List<Archive> infoList;
    }

    public class Archive
    {
        public int id;
        public string sceneName;   //最后所在的场景
        public double[] position;   //最后保存的位置
        public DateTime archiveDateTime; //存档时间
        public PersonStatus personStatus;
    }

    public class PersonStatus
    {
        public string personName; // 角色名
        public int personLv; // 等级
        public int personCurrentHp; //当前生命值
        public int personMaxHp; //最大生命值
        public int personDamage; //攻击力
        public int personDefense; //防御力
        public int personSpeed; //速度，决定在战斗中怪物或者人物的攻击先后顺序
        public int personExp; //当前经验值
        public int personCurrentLvNeedExp; //当前等级所需的经验值

        public Dictionary<PersonEquipmentType, int> personEquipmentDict; //装备位置和对应的装备对象
    }
}
