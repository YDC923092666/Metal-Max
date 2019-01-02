using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class NPCInfo
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }
        public List<string> TalkList { get; set; }
    }
}
