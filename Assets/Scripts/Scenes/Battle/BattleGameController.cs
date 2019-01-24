using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class BattleGameController : MonoSingleton<BattleGameController> 
	{
        private Queue<GameObject> battleUnits;           //所有参战对象的列表
        private GameObject playerUnit;           //主角
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
        private Transform charParent;

        public int attackData;            //伤害值


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
                if(item.name!= "CharactorPoint")
                {
                    parents.Add(item);
                }
                
            }

            charParent = GameObject.Find("BG/CharactorPoint").transform;
            //创建怪物和角色
            SpawnMonsters(GameManager.battleMonsters, parents);
            SpawnCharactor(charParent);

            //禁用角色面板
            battlePanel = GameObject.Find("Canvas/BattlePanel");
            battlePanel.SetActive(false);

            //创建参战列表
            battleUnits = new Queue<GameObject>();

            //将怪物单位分类成速度快和速度慢的
            enemyUnits = GameObject.FindGameObjectsWithTag(Tags.battleMonster);
            foreach (GameObject enemyUnit in enemyUnits)
            {
                if(enemyUnit.GetComponent<MonsterBattle>().status.speed > playerUnit.GetComponent<BattleStat>().status.speed)
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
            battleUnits.Enqueue(playerUnit);

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
            remainingEnemyUnits = GameObject.FindGameObjectsWithTag(Tags.battleMonster);
            remainingPlayer = GameObject.FindGameObjectWithTag(Tags.battleCharactor);

            //检查存活敌人单位
            if (remainingEnemyUnits.Length == 0)
            {
                Debug.Log("敌人全灭，战斗胜利");
                //TODO
            }
            //检查存活玩家单位
            else if (remainingPlayer.GetComponent<BattleStat>().status.hp == 0)
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
            if (currentActUnit.tag == Tags.battleMonster)
            {
                //如果行动单位是怪物则攻击玩家
                currentActUnitTarget = remainingPlayer;
                MonsterLaunchAttack();
            }
            else if (currentActUnit.tag == Tags.charactor)
            {
                isWaitForPlayerToChooseSkill = true;
                battlePanel.SetActive(true);
            }
        }

        /// <summary>
        /// 怪物发动攻击
        /// </summary>
        public void MonsterLaunchAttack()
        {
            //存储攻击者和攻击目标的属性脚本
            BattleStat attackOwner = currentActUnit.GetComponent<BattleStat>();
            BattleStat attackReceiver = currentActUnitTarget.GetComponent<BattleStat>();

            //命中率-躲避率 = 命中概率。如果在这个范围内，则命中
            if(Random.Range(0, 100)<(attackOwner.status.shootingRate - attackReceiver.status.escapeRate))
            {
                //根据攻防计算伤害
                attackData = attackOwner.status.damage - attackReceiver.status.defense + Random.Range(-2, 2);
                //TODO 显示战斗信息
                print(attackOwner.gameObject.name + "造成伤害： " + attackData);
            }
            else
            {
                attackData = 0;
                //TODO 显示躲避成功的文字
                print(attackReceiver.gameObject.name + "闪避成功");
            }
            print("当前攻击者是：" + attackOwner.gameObject.name);
            //播放攻击动画
            attackOwner.Attack();

            //在对象承受伤害并进入下个单位操作前前添加1s延迟
            //StartCoroutine("WaitForTakeDamage");
        }

        /// <summary>
        /// 生成怪物
        /// </summary>
        public void SpawnMonsters(List<Monster> monsters, List<Transform> parents)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                //初始化怪物
                GameObject monsterPrefab = Resources.Load<GameObject>("Prefab/Battle_Monster");
                var monsterSprite = Resources.Load<Sprite>(monsters[i].sprite);
                monsterPrefab.GetComponent<SpriteRenderer>().sprite = monsterSprite;
                GameObject monsterGo = Instantiate(monsterPrefab, parents[i], true);
                monsterGo.transform.position = parents[i].position;
                //给怪物身上的组件赋予对象
                monsterGo.GetComponent<MonsterBattle>().status = monsters[i];
                monsterGo.name = monsters[i].nameString;
                print(monsterGo.GetComponent<MonsterBattle>().status.id);
            }
        }

        public void SpawnCharactor(Transform parent)
        {
            //初始化角色
            GameObject charPrefab = Resources.Load<GameObject>("Prefab/Battle_Char");
            playerUnit = Instantiate(charPrefab, parent, true);
            playerUnit.transform.position = parent.position;
            //给怪物身上的组件赋予对象
            playerUnit.GetComponent<PlayerBattle>().status = GameObject.FindGameObjectWithTag(Tags.charactor).GetComponent<BaseAttr>();
            print(playerUnit.GetComponent<PlayerBattle>().status.hp);
        }

        /// <summary>
        /// 延时操作函数，避免在怪物回合操作过快
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForTakeDamage()
        {
            //被攻击者承受伤害
            currentActUnitTarget.GetComponent<BattleStat>().ReceiveDamage(attackData);
            if (!currentActUnitTarget.GetComponent<BattleStat>().IsDead())
            {
                currentActUnitTarget.GetComponent<Animator>().SetTrigger("TakeDamage");
            }
            else
            {
                currentActUnitTarget.GetComponent<Animator>().SetTrigger("Dead");
            }

            yield return new WaitForSeconds(1);
            ToBattle();
        }
    }
}
