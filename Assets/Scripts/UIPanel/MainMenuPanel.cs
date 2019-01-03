using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MetalMax
{
	public class MainMenuPanel : BasePanel
	{
        private Vector2 rayDir;
        private float distance = 1f; //射线检测距离
        private LayerMask mask = 1 << 8; //射线只检测Item层
        private GameObject talkPanel; //对话框面板
        private Text nameText;  //名字文本框
        private Text containText;   //对话内容文本框
        private int index = 0; //对话的索引
        private int talkInterval = 3;   //对话间隔
        private CanvasGroup canvasGroup;
        private Button talkButton;

        void Start()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            talkButton = FindObjectOfType<Button>();
            talkButton.onClick.AddListener(() =>
            {
                OnTalkButtonClick();
            });
        }

        public override void OnEnter()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnPause()
        {
            canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板 不再和鼠标交互
        }

        public override void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 当对话按钮被点击时
        /// </summary>
        public void OnTalkButtonClick()
        {
            //射线检测身前的物体
            Vector2 headDir = Char01Move.direction;
            Vector2 char01Position = GameObject.FindGameObjectWithTag(Tags.charactor).transform.position;
            RaycastHit2D hit = Physics2D.Raycast(char01Position, headDir, distance, mask);
            if (hit.collider != null && hit.collider.tag == Tags.NPC)
            {
                //ShowTalkPanel();
                //TODO
                hit.collider.GetComponent<NPCBase>().Talk();
            }
            else
            {
                //显示tipPanel，提示前面没有人。
                //TODO
            }
        }
    }
}