using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace MetalMax
{
    public enum SpecialSkill
    {
        Null,
        Flee, //逃跑
        Destroy, //破坏坦克的主炮
        Critical
    }

    [Serializable]
    class BossJson
    {
        public List<Boss> infoList;
    }

    [Serializable]
    public class Boss
	{
        public int id;
        public string name;
        public int hp;
        public int damage;
        public int defense;
        public int criticalRate;  //暴击率
        public List<BossSkill> skillList;
        public string skillString;
    }

    [Serializable]
    public class BossSkill : ISerializationCallbackReceiver
    {
        [NonSerialized]
        public SpecialSkill skill;
        public string skillString;

        public void OnAfterDeserialize()
        {
            SpecialSkill type = (SpecialSkill)System.Enum.Parse(typeof(SpecialSkill), skillString);
            skill = type;
        }

        public void OnBeforeSerialize()
        {

        }
    }
}