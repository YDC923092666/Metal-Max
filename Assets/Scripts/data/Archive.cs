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
        public int teamPersonCount;
        public string sceneName;   //最后所在的场景
        public double[] position;   //最后保存的位置
        public DateTime archiveDateTime; //存档时间
        public List<PersonStatus> personStatusList;
        public List<TankStatus> tankStatusList;
        public List<Boss> bossList; //消灭的BOSS列表
    }

    public class PersonStatus
    {
        public string personName; // 角色名
        public int personLevel; // 等级
        public int personHp; //生命值
        public int personDamage; //攻击力
        public int personDefense; //防御力

        public Dictionary<PersonEquipmentType, int> personEquipmentDict; //装备位置和对应的装备对象
    }

    public class TankStatus
    {
        public int tankHp; //生命值
        public int tankDamage; //攻击力
        public int tankDefense; //防御力

        public Dictionary<TankEquipmentType, int> tankEquipmentDict; //坦克装备位置和对应的装备对象.int值是id值
        public Dictionary<TankBulletType, int> tankBulletCountDict; //炮弹的类型和数量
    }
}
