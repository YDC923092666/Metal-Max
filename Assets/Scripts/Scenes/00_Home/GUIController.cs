using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MetalMax
{
	public class GUIController : MonoBehaviour 
	{
        /// <summary>
        /// 当点击"开始游戏"后，跳转到游戏场景
        /// </summary>
        public void OnStartGameButtonClick()
        {
            string text = GameObject.FindObjectOfType<InputField>().text;

            //构建角色列表
            List<PersonStatus> personStatusList = new List<PersonStatus>();
            PersonStatus player1 = new PersonStatus()
            {
                PersonName = text,
                PersonLevel = 1,
                PersonHp = 10,
                PersonDamage = 10,
                PersonDefence = 10
            };
            personStatusList.Add(player1);
            //保存角色姓名，初始化玩家等级为1
            Archive archive = new Archive()
            {
                Id = 1,
                TeamPersonCount = 1,
                SceneName = SceneManager.GetActiveScene().name,
                Position = new double[] { 0, 0, 0 },
                ArchiveDateTime = DateTime.Now,
                PersonStatusList = personStatusList
            };
            SaveManager.SaveCurrentArchive(archive);

            //进入游戏场景
            SceneManager.LoadScene("01_World1_Home");
        }

        public void OnStartGameButtonClickMsgReceived(object data)
        {

        }
    }
}
