using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class GameManager : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //1.显示健康游戏提醒。使用Dotween。
            GUIManager.LoadPanel("Prefab", "JiankongyouxiPanel");
            Delay(3.0f, () => GUIManager.UnLoadPanel("JiankongyouxiPanel"));

            //2.显示BG，播放音效，同时检查版本更新（右下角显示“正在检查更新……”）。
        }

        protected override void LaunchInProductionMode()
        {
            //1.显示健康游戏提醒。使用Dotween。

            //2.显示BG，播放音效，同时检查版本更新（右下角显示“正在检查更新……”）。
            throw new System.NotImplementedException();
        }

        protected override void LaunchInTestMode()
        {
            //1.显示健康游戏提醒。使用Dotween。

            //2.显示BG，播放音效，同时检查版本更新（右下角显示“正在检查更新……”）。
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            
        }

        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
    }
}