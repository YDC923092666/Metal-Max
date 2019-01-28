using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    [Serializable]
    public class MonsterJson
    {
        public List<BaseAttr> infoList;
    }

    [Serializable]
    public class Monster: BaseAttr
	{
        public int exp;
        public int gold;
    }
}
