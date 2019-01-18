using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class NPCBase : MonoBehaviour 
	{
        public int id;
        protected List<string> talkList;
        protected SpriteRenderer rd;
        protected NPCInfo info;
        private MainMenuPanel menuPanel;

        protected virtual void Start()
        {
            InitNPC();
        }

        public void InitNPC()
        {
            rd = GetComponent<SpriteRenderer>();
            //读取NPC的json数据
            info = SaveManager.GetNPCjectFromListById(id);
            rd.sprite = Resources.Load<Sprite>(info.sprite);
        }

        public virtual void Talk()
        {
            var containTextList = info.talkList;
            if (menuPanel == null)
            {
                menuPanel = GameObject.Find("Canvas/MainMenuPanel").GetComponent<MainMenuPanel>();
            }
            menuPanel.ShowInfoPanel(containTextList);
        }
    }
}
