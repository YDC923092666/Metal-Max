using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#pragma warning disable 0649
namespace MetalMax
{
    public enum UIPanelType
    {
        TalkPanel,
        KnapsackPanel,
        MainMenuPanel,
        ItemInfoPanel,
        CharacterPanel
    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    [Serializable]
    public class UIPanelInfo: ISerializationCallbackReceiver
    {
        [NonSerialized]
        public UIPanelType panelType;
        public string panelTypeString;
        public string path;

        // 反序列化   从文本信息 到对象
        public void OnAfterDeserialize()
        {
            UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
            panelType = type;
        }

        public void OnBeforeSerialize()
        {

        }
    }
}