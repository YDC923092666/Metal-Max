using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace MetalMax
{
	public class GameManager : MonoSingleton<GameManager>
    {
        public bool isInBattleState = false;
        public void EnterBattleState()
        {
            isInBattleState = true;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }
}
