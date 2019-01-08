using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#pragma warning disable 0649
namespace MetalMax
{
    /// <summary>
    /// 人类装备类型
    /// </summary>
    public enum PersonEquipmentType
    {
        Head,
        Breastplate,
        Hands,
        Pant,
        Foot,
        Gun
    }

    [Serializable]
    class PersonEquipmentJson
    {
        public List<PersonEquipment> infoList;
    }

    [Serializable]
    public class PersonEquipment:ISerializationCallbackReceiver
	{
        public int id;
        public string name; //装备名字
        [NonSerialized]
        public PersonEquipmentType personEquipmentType;    //装备类型
        public string personEquipmentTypeString;    
        public int hp;
        public int damage;
        public int defense;

        public void OnAfterDeserialize()
        {
            PersonEquipmentType type = (PersonEquipmentType)System.Enum.Parse(typeof(PersonEquipmentType), personEquipmentTypeString);
            personEquipmentType = type;
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}