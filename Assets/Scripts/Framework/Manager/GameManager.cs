using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using LitJson;

namespace MetalMax
{
	public class GameManager : MonoSingleton<GameManager>
    {
        public bool isEquipTank = false; //是否已经装备了坦克
    }
}
