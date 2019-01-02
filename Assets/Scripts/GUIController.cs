using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class GUIController : MonoSingleton<GUIController> 
	{
        private Vector2 rayDir;
        private float distance = 1f; //射线检测距离
        private LayerMask mask = 1 << 8; //射线只检测Item层
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
        public void OnTalkButtonClick()
        {
            Vector2 headDir = Char01Move.direction;
            Vector2 char01Position = GameObject.FindGameObjectWithTag(Tags.charactor).transform.position;
            RaycastHit2D hit = Physics2D.Raycast(char01Position, headDir, distance, mask);
            print(headDir.ToString());
            Debug.DrawRay(char01Position, headDir, Color.green);
            if (hit.collider != null)
            {
                print("对话");
                print(hit.collider.name);
            }
        }

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
