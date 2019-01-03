using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MetalMax
{
    public enum UIPanelType
    {
        ItemMessage,
        Knapsack,
        MainMenu,
        Shop,
        Skill,
        System,
        Task,
    }

    [Serializable]
    public class UIPanelInfo : ISerializationCallbackReceiver
    {
        [NonSerialized]
        public UIPanelType panelType;
        public string panelTypeString;
        public string path;

        public void OnBeforeSerialize()
        {

        }

        //反序列化
        public void OnAfterDeserialize()
        {
            UIPanelType type = (UIPanelType)Enum.Parse(typeof(UIPanelType), panelTypeString);
            panelType = type;
        }
    }
}