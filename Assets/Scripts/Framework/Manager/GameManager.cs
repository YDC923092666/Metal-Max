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

        public Charactor charactor;  //从excel解析出来的1个主角
        public List<Monster> monsterList;  //从excel解析出来的所有怪物
        public static List<Monster> battleMonsters = new List<Monster>();   //战斗场景要生成的怪物
        public int monstersCount;

        protected override void Awake()
        {
            base.Awake();
            GetAllMonsters();
            GetCharactorBaseAttr();
        }

        private void Start()
        {

        }

        public void EnterBattleState(int minMonsterId, int maxMonsterId , int monsterCount = 0)
        {
            isInBattleState = true;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
            if(monsterCount == 0)
            {
                monstersCount = UnityEngine.Random.Range(1, 7);
            }
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
                        monster.nameString = nValue.ToString();
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
            //MonsterJson monsterJson = new MonsterJson
            //{
            //    infoList = monsterList
            //};
            //string text = JsonUtility.ToJson(monsterJson);
        }

        /// <summary>
        /// 获取人物基础属性
        /// </summary>
        void GetCharactorBaseAttr()
        {
            var result = SaveManager.ReadExcelStream(Const.charactorFilePath);
            int columns = result.Tables[0].Columns.Count;
            int rows = result.Tables[0].Rows.Count;

            charactor = new Charactor();
            for (int j = 0; j < columns; j++)
            {
                var nValue = result.Tables[0].Rows[1][j];
                if (j == 0)
                {
                    charactor.id = Convert.ToInt32(nValue);
                }
                else if (j == 1)
                {
                    charactor.initHp = Convert.ToInt32(nValue);
                }
                else if (j == 2)
                {
                    charactor.attackCount = Convert.ToInt32(nValue);
                }
                else if (j == 3)
                {
                    charactor.initDamage = Convert.ToInt32(nValue);
                }
                else if (j == 4)
                {
                    charactor.initDefense = Convert.ToInt32(nValue);
                }
                else if (j == 5)
                {
                    charactor.initSpeed = Convert.ToInt32(nValue);
                }
                else if (j == 6)
                {
                    charactor.initShootingRate = Convert.ToInt32(nValue);
                }
                else if (j == 7)
                {
                    charactor.initEscapeRate = Convert.ToInt32(nValue);
                }
                else if (j == 8)
                {
                    charactor.nameString = Convert.ToString(nValue);
                }
                else if (j == 9)
                {
                    charactor.sprite = Convert.ToString(nValue);
                }
                else if (j == 10)
                {
                    charactor.battleSprite = Convert.ToString(nValue);
                }
            }
        }
    }
}
