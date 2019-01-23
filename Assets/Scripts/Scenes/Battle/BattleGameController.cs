using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class BattleGameController : MonoBehaviour 
	{
        private Queue<GameObject> battleUnits;           //所有参战对象的列表
        private GameObject[] playerUnits;           //所有参战玩家的列表
        private GameObject[] enemyUnits;            //所有参战敌人的列表
        private List<GameObject> fasterEnemyUnits;            //所有参战速度比主角快的敌人的列表
        private List<GameObject> slowerEnemyUnits;            //所有参战速度比主角慢的敌人的列表
        private GameObject[] remainingEnemyUnits;           //剩余参战对敌人的列表
        private GameObject[] remainingPlayerUnits;           //剩余参战对玩家的列表

        private GameObject currentActUnit;          //当前行动的单位
        private GameObject currentActUnitTarget;            //当前行动的单位的目标

        public bool isWaitForPlayerToChooseSkill = false;            //玩家选择技能UI的开关
        public bool isWaitForPlayerToChooseTarget = false;            //是否等待玩家选择目标，控制射线的开关

        /// <summary>
        /// 创建初始参战列表，存储参战单位
        /// </summary>
        void Start()
        {
            //禁用结束菜单
            //endImage = GameObject.Find("ResultImage");
            //endImage.SetActive(false);

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
            ToBattle();
        }

        /// <summary>
        /// 判断战斗进行的条件是否满足，取出参战列表第一单位，并从列表移除该单位，单位行动
        /// 行动完后重新添加单位至队列，继续ToBattle()
        /// </summary>
        public void ToBattle()
        {
            remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
            remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");

            //检查存活敌人单位
            if (remainingEnemyUnits.Length == 0)
            {
                Debug.Log("敌人全灭，战斗胜利");
                //TODO
            }
            //检查存活玩家单位
            else if (remainingPlayerUnits.Length == 0)
            {
                Debug.Log("我方全灭，战斗失败");
                //TODO
            }
            else
            {
                //取出参战列表第一单位，并从列表移除
                currentActUnit = battleUnits[0];
                battleUnits.Remove(currentActUnit);
                //重新将单位添加至参战列表末尾
                battleUnits.Add(currentActUnit);

                //Debug.Log("当前攻击者：" + currentActUnit.name);

                //获取该行动单位的属性组件
                UnitStats currentActUnitStats = currentActUnit.GetComponent<UnitStats>();

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
    }
}
