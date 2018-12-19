using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class HomeManager : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //1.显示健康游戏提醒。使用Dotween。

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
    }
}