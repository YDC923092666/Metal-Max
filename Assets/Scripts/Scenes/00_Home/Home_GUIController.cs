using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MetalMax
{
	public class Home_GUIController : MonoBehaviour 
	{
        /// <summary>
        /// 当点击"开始游戏"后，跳转到游戏场景
        /// </summary>
        public void OnStartGameButtonClick()
        {
            string text = GameObject.FindObjectOfType<InputField>().text;

            //构建角色对象
            PersonStatus player1 = new PersonStatus()
            {
                personName = text,
                personLv = 1,
                personCurrentHp = 20,
                personMaxHp = 20,
                personDamage = 10,
                personDefense = 10,
                personSpeed = 1,
                personExp = 1,
                personCurrentLvNeedExp = 20
            };
            //保存角色姓名，初始化玩家等级为1
            Archive archive = new Archive()
            {
                id = 1,
                sceneName = SceneManager.GetActiveScene().name,
                position = new double[] { 0, 0, 0 },
                archiveDateTime = DateTime.Now,
                personStatus = player1
            };
            SaveManager.SaveCurrentArchive(archive);

            //进入游戏场景
            SceneManager.LoadScene("01_World1_Home");
        }
    }
}
