using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class GUIController : MonoSingleton<GUIController> 
	{
        private Vector2 rayDir;
        private float distance = 0.5f; //射线检测距离
        private GameObject buttonPanel;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            buttonPanel = GameObject.Find("ButtonPanel");
        }

        /// <summary>
        /// 当对话按钮被点击时
        /// </summary>
        //public void OnTalkButtonClick()
        //{
        //    Vector2 headDir = Char01Move.direction;
        //    Vector2 char01Position = GameObject.FindGameObjectWithTag(Tags.charactor).transform.position;
        //    RaycastHit2D hit = Physics2D.Raycast(char01Position, headDir, distance);
        //    if (hit.collider != null && hit.collider.tag == Tags.NPC)
        //    {
        //        print(hit.collider.name);
        //    }
        //}

        public void ShowButtonPanel()
        {
            buttonPanel.SetActive(true);
        }

        public void HideButtonPanel()
        {
            buttonPanel.SetActive(false);
        }
    }
}
