using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class BattleGameController : MonoSingleton<BattleGameController> 
	{
        private Queue<GameObject> battleUnits;           //所有参战对象的列表
        private GameObject[] playerUnits;           //所有参战玩家的列表
        private GameObject[] enemyUnits;            //所有参战敌人的列表
        private List<GameObject> fasterEnemyUnits = new List<GameObject>(); //所有参战速度比主角快的敌人的列表
        private List<GameObject> slowerEnemyUnits = new List<GameObject>(); //所有参战速度比主角慢的敌人的列表
        private GameObject[] remainingEnemyUnits;           //剩余参战对敌人的列表
        private GameObject remainingPlayer;           //剩余参战玩家

        private GameObject currentActUnit;          //当前行动的单位
        private GameObject currentActUnitTarget;            //当前行动的单位的目标

        public bool isWaitForPlayerToChooseSkill = false;            //玩家选择技能UI的开关
        public bool isWaitForPlayerToChooseTarget = false;            //是否等待玩家选择目标，控制射线的开关


        private GameObject battlePanel;
        private List<Transform> parents;

        protected override void Awake()
        {
            base.Awake();
        }
        /// <summary>
        /// 创建初始参战列表，存储参战单位
        /// </summary>
        public void StartGame()
        {
            //禁用结束菜单
            //endImage = GameObject.Find("ResultImage");
            //endImage.SetActive(false);

            parents = new List<Transform>();
            Transform Bg = GameObject.Find("BG").transform;
            foreach (Transform item in Bg)
            {
                parents.Add(item);
            }
            //创建怪物和角色
            SpawnMonsters(GameManager.battleMonsters, parents);
            SpawnCharactor();

            //禁用角色面板
            battlePanel = GameObject.Find("Canvas/BattlePanel");
            battlePanel.SetActive(false);

            //创建参战列表
            battleUnits = new Queue<GameObject>();

            //将怪物单位分类成速度快和速度慢的
            enemyUnits = GameObject.FindGameObjectsWithTag(Tags.monster);
            foreach (GameObject enemyUnit in enemyUnits)
            {
                if(enemyUnit.GetComponent<MonsterBattle>().monster.speed > SaveManager.currentArchive.personStatus.personSpeed)
                {
                    fasterEnemyUnits.Add(enemyUnit);
                }
                else
                {
                    slowerEnemyUnits.Add(enemyUnit);
                }
            }

            //添加速度比主角快的怪物单位至参战队列
            foreach (GameObject item in fasterEnemyUnits)
            {
                battleUnits.Enqueue(item);
            }


            //添加玩家单位至参战列表
            playerUnits = GameObject.FindGameObjectsWithTag(Tags.charactor);
            foreach (GameObject playerUnit in playerUnits)
            {
                battleUnits.Enqueue(playerUnit);
            }

            //添加速度比主角慢的怪物单位至参战队列
            foreach (GameObject item in slowerEnemyUnits)
            {
                battleUnits.Enqueue(item);
            }

            //开始战斗
            //ToBattle();
        }

        /// <summary>
        /// 判断战斗进行的条件是否满足，取出参战列表第一单位，并从列表移除该单位，单位行动
        /// 行动完后重新添加单位至队列，继续ToBattle()
        /// </summary>
        public void ToBattle()
        {
            remainingEnemyUnits = GameObject.FindGameObjectsWithTag(Tags.monster);
            remainingPlayer = GameObject.FindGameObjectWithTag(Tags.charactor);

            //检查存活敌人单位
            if (remainingEnemyUnits.Length == 0)
            {
                Debug.Log("敌人全灭，战斗胜利");
                //TODO
            }
            //检查存活玩家单位
            else if (remainingPlayer.GetComponent<PlayerBattle>().HP == 0)
            {
                Debug.Log("我方全灭，战斗失败");
                //TODO
            }
            else
            {
                //取出参战列表第一单位，并从队列中移除
                currentActUnit = battleUnits.Dequeue();
                //重新将单位添加至参战列表末尾
                battleUnits.Enqueue(currentActUnit);

                //Debug.Log("当前攻击者：" + currentActUnit.name);

                //获取该行动单位的属性组件
                BattleStat currentActUnitStats = currentActUnit.GetComponent<BattleStat>();

                //判断取出的战斗单位是否存活
                if (!currentActUnitStats.IsDead())
                {
                    //选取攻击目标
                    FindTarget();
                }
                else
                {
                    //Debug.Log("目标死亡，跳过回合");
                    ToBattle();
                }
            }
        }

        /// <summary>
        /// 查找攻击目标，如果行动者是怪物则从剩余玩家中随机
        /// 如果行动者是玩家，则获取鼠标点击对象
        /// </summary>
        /// <returns></returns>
        void FindTarget()
        {
            if (currentActUnit.tag == Tags.monster)
            {
                //如果行动单位是怪物则攻击玩家
                currentActUnitTarget = remainingPlayer;
                LaunchAttack();
            }
            else if (currentActUnit.tag == Tags.charactor)
            {
                isWaitForPlayerToChooseSkill = true;
                battlePanel.SetActive(true);
            }
        }

        public void LaunchAttack()
        {

        }

        /// <summary>
        /// 生成怪物
        /// </summary>
        public void SpawnMonsters(List<Monster> monsters, List<Transform> parents)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                //初始化怪物
                GameObject monsterPrefab = Resources.Load<GameObject>("Prefab/Monster");
                var monsterSprite = Resources.Load<Sprite>(monsters[i].sprite);
                monsterPrefab.GetComponent<SpriteRenderer>().sprite = monsterSprite;
                GameObject monsterGo = Instantiate(monsterPrefab, parents[i], true);
                monsterGo.transform.position = parents[i].position;
                //给怪物身上的组件赋予对象
                monsterGo.GetComponent<MonsterBattle>().monster = monsters[i];
            }
            //foreach (var monster in monsters)
            //{
            //    GameObject monsterGo = Resources.Load<GameObject>(monster.sprite);
            //    Instantiate(monsterGo, parent);
            //    monsterGo.transform.position = parent.position;
            //}
            
        }

        public void SpawnCharactor(Transform parent)
        {
            //初始化角色
            GameObject monsterPrefab = Resources.Load<GameObject>("Prefab/Monster");
            var monsterSprite = Resources.Load<Sprite>(monsters[i].sprite);
            monsterPrefab.GetComponent<SpriteRenderer>().sprite = monsterSprite;
            GameObject monsterGo = Instantiate(monsterPrefab, parents[i], true);
            monsterGo.transform.position = parents[i].position;
            //给怪物身上的组件赋予对象
            monsterGo.GetComponent<MonsterBattle>().monster = monsters[i];
        }
    }
}
