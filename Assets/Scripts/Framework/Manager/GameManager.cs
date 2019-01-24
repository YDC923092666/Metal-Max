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

        public List<Monster> monsterList;  //从excel解析出来的所有怪物
        public static List<Monster> battleMonsters = new List<Monster>();   //战斗场景要生成的怪物
        public int monstersCount;

        protected override void Awake()
        {
            base.Awake();
            GetAllMonsters();
        }

        private void Start()
        {
            
        }

        public void EnterBattleState(int minMonsterId, int maxMonsterId)
        {
            isInBattleState = true;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
            monstersCount = UnityEngine.Random.Range(1, 7);
            battleMonsters = new List<Monster>();
            for (int i = 0; i < monstersCount; i++)
            {
                var index = UnityEngine.Random.Range(0, monsterList.Count);
                battleMonsters.Add(monsterList[index]);
            }
        }

        void GetAllMonsters()
        {
            var result = SaveManager.ReadExcelStream(Const.monsterFilePath);
            int columns = result.Tables[0].Columns.Count;
            int rows = result.Tables[0].Rows.Count;

            monsterList = new List<Monster>();

            for (int i = 1; i < rows; i++)
            {
                Monster monster = new Monster();
                for (int j = 1; j < columns; j++)
                {
                    var nValue = result.Tables[0].Rows[i][j];
                    if (j == 1)
                    {
                        monster.name = nValue.ToString();
                    }
                    else if (j == 2)
                    {
                        monster.id = Convert.ToInt32(nValue);
                    }
                    else if (j == 3)
                    {
                        monster.hp = Convert.ToInt32(nValue);
                    }
                    else if (j == 4)
                    {
                        monster.attackCount = Convert.ToInt32(nValue);
                    }
                    else if (j == 5)
                    {
                        monster.damage = Convert.ToInt32(nValue);
                    }
                    else if (j == 6)
                    {
                        monster.defense = Convert.ToInt32(nValue);
                    }
                    else if (j == 7)
                    {
                        monster.speed = Convert.ToInt32(nValue);
                    }
                    else if (j == 8)
                    {
                        monster.shootingRate = Convert.ToInt32(nValue);
                    }
                    else if (j == 9)
                    {
                        monster.escapeRate = Convert.ToInt32(nValue);
                    }
                    else if (j == 10)
                    {
                        monster.sprite = nValue.ToString();
                    }
                }
                monsterList.Add(monster);
            }
            print(monsterList.Count);
            //MonsterJson monsterJson = new MonsterJson
            //{
            //    infoList = monsterList
            //};
            //string text = JsonUtility.ToJson(monsterJson);
        }
    }
}
