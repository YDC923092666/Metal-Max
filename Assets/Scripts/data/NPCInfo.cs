using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace MetalMax
{
    [Serializable]
    public class NPCInfoJson
    {
        public List<NPCInfo> infoList;
    }

    [Serializable]
    public class NPCInfo
	{
        public int id;
        public string name;
        public string sprite;
        public List<NPCTalk> talkList;
    }

    [Serializable]
    public class NPCTalk
    {
        public string talk;
    }
}
