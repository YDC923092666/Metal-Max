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
        public List<Lv> lvList;  //从excel解析出来的所有怪物
        public static List<BaseAttr> battleMonsters = new List<BaseAttr>();   //战斗场景要生成的怪物
        public int monstersCount;

        protected override void Awake()
        {
            base.Awake();
            GetAllMonsters();
            GetCharactorBaseAttr();
            GetLvExpTable();
        }

        public void EnterBattleState(int minMonsterId, int maxMonsterId , int monsterCount = 0)
        {
            isInBattleState = true;
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
            if(monsterCount == 0)
            {
                monstersCount = UnityEngine.Random.Range(1, 7);
            }
            battleMonsters = new List<BaseAttr>();
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
                    else if (j == 11)
                    {
                        monster.exp = Convert.ToInt32(nValue);
                    }
                    else if (j == 12)
                    {
                        monster.gold = Convert.ToInt32(nValue);
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

        /// <summary>
        /// 获取lv信息表，每个等级对应所需的经验值
        /// </summary>
        public void GetLvExpTable()
        {
            var result = SaveManager.ReadExcelStream(Const.lvFilePath);
            int columns = result.Tables[0].Columns.Count;
            int rows = result.Tables[0].Rows.Count;

            //初始化一个列数和对应的字段的对应字典
            //Dictionary<int, string> dict = new Dictionary<int, string>();
            //dict.Add(0, "lv");
            //dict.Add(1, "exp");
            //dict.Add(2, "addHp");
            //dict.Add(3, "addDamage");
            //dict.Add(4, "addDefense");
            //dict.Add(5, "addSpeed");
            //dict.Add(6, "addShootingRate");
            //dict.Add(7, "addEscapeRate");
            lvList = new List<Lv>();

            for (int i = 1; i < rows; i++)
            {
                Lv lv = new Lv();
                for (int j = 0; j < columns; j++)
                {
                    //使用反射给字段赋值
                    //print(dict[j]);
                    //var fieldInfo = lv.GetType().GetField(dict[j]);
                    //print(fieldInfo.Name);
                    //var nValue = result.Tables[0].Rows[i][j];
                    //fieldInfo.SetValue(lv, Convert.ToInt32(nValue));

                    var nValue = result.Tables[0].Rows[i][j];
                    if (j == 0)
                    {
                        lv.lv = Convert.ToInt32(nValue);
                    }
                    else if (j == 1)
                    {
                        lv.exp = Convert.ToInt32(nValue);
                    }
                    else if (j == 2)
                    {
                        lv.addHp = Convert.ToInt32(nValue);
                    }
                    else if (j == 3)
                    {
                        lv.addDamage = Convert.ToInt32(nValue);
                    }
                    else if (j == 4)
                    {
                        lv.addDefense = Convert.ToInt32(nValue);
                    }
                    else if (j == 5)
                    {
                        lv.addSpeed = Convert.ToInt32(nValue);
                    }
                    else if (j == 6)
                    {
                        lv.addShootingRate = Convert.ToSingle(nValue);
                    }
                    else if (j == 7)
                    {
                        lv.addEscapeRate = Convert.ToSingle(nValue);
                    }
                }
                lvList.Add(lv);
            }
        }
    }
}
