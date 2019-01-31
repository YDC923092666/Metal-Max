using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class BattleGameRoot : MainManager
    {
        protected override void LaunchInDevelopingMode()
        {
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[1]);
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[2]);
            //GameManager.battleMonsters.Add(GameManager.Instance.monsterList[3]);

            BattleGameController.Instance.StartGame();
        }

        protected override void LaunchInProductionMode()
        {
            BattleGameController.Instance.StartGame();
        }

        protected override void LaunchInTestMode()
        {
        }
    }
}
