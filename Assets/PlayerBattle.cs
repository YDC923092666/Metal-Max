using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class PlayerBattle : MonoBehaviour
	{
        public int HP = 100;

        //等待玩家操作
        public bool isWaitPlayer = true;
        public bool ifUIshow = true;

        //动画组件
        private Animator mAnim;

        // Use this for initialization
        void Start()
        {
            mAnim = GetComponent<Animator>();
            mAnim.SetBool("idle", true);
        }

        //伤害
        void OnDamage(int mValue)
        {
            HP -= mValue;
        }

        void OnGUI()
        {
            //如果处于等待状态，则显示操作窗口
            if (isWaitPlayer && ifUIshow)
            {
                GUI.Window(0, new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 200, 200), InitWindow, "请选择技能");
                mAnim.SetBool("skill", false);
                mAnim.SetBool("idle", true);
            }
        }

        void InitWindow(int ID)
        {
            if (GUI.Button(new Rect(0, 20, 200, 30), "飞剑斩"))
            {
                mAnim.SetBool("idle", false);
                mAnim.SetBool("skill", true);
                //交换操作权
                isWaitPlayer = false;
                ifUIshow = false;
            }
            if (GUI.Button(new Rect(0, 50, 200, 30), "降魔伏法"))
            {
                mAnim.SetBool("idle", false);
                mAnim.SetBool("skill", true);
                //交换操作权
                isWaitPlayer = false;
                ifUIshow = false;
            }
        }
    }
}