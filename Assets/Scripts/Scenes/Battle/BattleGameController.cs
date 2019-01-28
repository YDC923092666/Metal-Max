using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        public GameObject currentActUnit;          //当前行动的单位
        public GameObject currentActUnitTarget;            //当前行动的单位的目标

        public bool isWaitForPlayerToChooseTarget = false;            //是否等待玩家选择目标，控制射线的开关

        private GameObject battlePanel; //玩家选择面板
        private GameObject battleInfoPanel; //战斗信息面板
        private BattleInfoPanel battleInfoPanelScript;
        private List<Transform> parents;    //怪物生成的位置
        private Transform charParent;   //主角生成的位置
        private Charactor charStatus;

        public int escapeRate = 90; //可逃跑的几率
        public int attackData;            //伤害值
        public int exp; //获得经验值
        public int gold;    //获得金币

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// 创建初始参战列表，存储参战单位
        /// </summary>
        public void StartGame()
        {
            //获取人物属性
            charStatus = GameObject.FindGameObjectWithTag(Tags.charactor).GetComponent<Charactor>();

            //寻找需要初始化怪物的位置
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

            //更新battleInfoPanel信息
            if (battleInfoPanel == null)
            {
                battleInfoPanel = GameObject.Find("Canvas/BattleInfoPanel");
            }
            battleInfoPanelScript = battleInfoPanel.GetComponent<BattleInfoPanel>();
            
            string infoText = null;
            foreach (var item in GameManager.battleMonsters)
            {
                infoText += string.Format("{0}出现了!\n", item.nameString);
            }
            battleInfoPanelScript.ChangeBattleInfoText(infoText);

            //更新人物状态面板
            var charactorStatus = playerUnit.GetComponent<BattleStat>().status;
            string battleStatusText = null;
            battleStatusText = string.Format("HP: {0}", charactorStatus.hp);
            battleInfoPanelScript.ChangeStatusText(battleStatusText);

            //创建参战列表
            battleUnits = new Queue<GameObject>();

            //将怪物单位分类成速度快和速度慢的
            enemyUnits = GameObject.FindGameObjectsWithTag(Tags.battleMonster);
            foreach (GameObject enemyUnit in enemyUnits)
            {
                if(enemyUnit.GetComponent<BattleStat>().status.speed > playerUnit.GetComponent<BattleStat>().status.speed)
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
            StartCoroutine(ToBattle());
        }

        /// <summary>
        /// 判断战斗进行的条件是否满足，取出参战列表第一单位，并从列表移除该单位，单位行动
        /// 行动完后重新添加单位至队列，继续ToBattle()
        /// </summary>
        public IEnumerator ToBattle()
        {
            //先等待4秒
            yield return new WaitForSeconds(4);
            remainingEnemyUnits = GameObject.FindGameObjectsWithTag(Tags.battleMonster);
            remainingPlayer = GameObject.FindGameObjectWithTag(Tags.battleCharactor);

            //检查存活敌人单位
            if (remainingEnemyUnits.Length == 0)
            {
                Debug.Log("敌人全灭，战斗胜利");
                battleInfoPanelScript.ChangeBattleInfoText(string.Format("战斗胜利！\n获得经验值：{0}，金币：{1}！",exp,gold)); ;
                if (charStatus.Check4Upgrade(exp))
                {
                    charStatus.Upgrade();
                }
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

                //获取该行动单位的属性组件
                BattleStat currentActUnitStats = currentActUnit.GetComponent<BattleStat>();

                //判断取出的战斗单位是否存活
                if (!currentActUnitStats.IsDead())
                {
                    //重新将单位添加至参战列表末尾
                    battleUnits.Enqueue(currentActUnit);
                    //选取攻击目标
                    FindTarget();
                }
                else
                {
                    Debug.Log("目标死亡，跳过回合");
                    Destroy(currentActUnit);
                    StartCoroutine(ToBattle());
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
            BattleStat currentActUnitStats = currentActUnit.GetComponent<BattleStat>();
            //显示battleInfo
            var battleInfoPanelScript = battleInfoPanel.GetComponent<BattleInfoPanel>();
            battleInfoPanelScript.ChangeBattleInfoText(currentActUnitStats.status.nameString + "开始攻击！");
            if (currentActUnit.tag == Tags.battleMonster)
            {
                //如果行动单位是怪物
                currentActUnitTarget = remainingPlayer;
                StartCoroutine(LaunchAttack());
            }
            else if (currentActUnit.tag == Tags.battleCharactor)
            {
                //TODO
                battleInfoPanel.SetActive(false);
                battlePanel.SetActive(true);
                battlePanel.GetComponent<BattlePanel>().InitPanel();
            }
        }

        /// <summary>
        /// 生成怪物
        /// </summary>
        public void SpawnMonsters(List<BaseAttr> monsters, List<Transform> parents)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                //初始化怪物
                GameObject monsterPrefab = Resources.Load<GameObject>("Prefab/Battle_Monster");
                var monsterSprite = Resources.Load<Sprite>(monsters[i].sprite);
                monsterPrefab.GetComponent<SpriteRenderer>().sprite = monsterSprite;
                GameObject monsterGo = Instantiate(monsterPrefab, parents[i], true);
                monsterGo.transform.position = parents[i].position;

                //添加碰撞器
                monsterGo.AddComponent<BoxCollider2D>();
                //给怪物身上的组件赋予对象
                monsterGo.GetComponent<MonsterBattle>().status = monsters[i];
                monsterGo.name = monsters[i].nameString;
            }
        }

        public void SpawnCharactor(Transform parent)
        {
            //初始化角色
            GameObject charPrefab = Resources.Load<GameObject>("Prefab/Battle_Char");
            playerUnit = Instantiate(charPrefab, parent, true);
            playerUnit.transform.position = parent.position;

            //获取人物当前属性，复制给战斗场景的人物
            var playerUnitScript = (PlayerBattle)playerUnit.GetComponent<BattleStat>();
            playerUnitScript.status = charStatus;
        }

        /// <summary>
        /// 延时操作函数，避免在怪物回合操作过快
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForNextTurn()
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(ToBattle());
        }

        IEnumerator WaitForTakeDamage()
        {
            yield return new WaitForSeconds(1.5f);
            //被攻击者承受伤害
            currentActUnitTarget.GetComponent<BattleStat>().ReceiveDamage(attackData);
            yield return new WaitForSeconds(2f);
        }

        /// <summary>
        /// 发动攻击
        /// </summary>
        public IEnumerator LaunchAttack()
        {
            battlePanel.SetActive(false);
            battleInfoPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            var battleInfoPanelScript = battleInfoPanel.GetComponent<BattleInfoPanel>();
            //存储攻击者和攻击目标的属性脚本
            BattleStat attackOwner = currentActUnit.GetComponent<BattleStat>();
            BattleStat attackReceiver = currentActUnitTarget.GetComponent<BattleStat>();

            //播放攻击动画
            attackOwner.Attack();
            yield return new WaitForSeconds(1f);

            print("当前攻击者是：" + attackOwner.gameObject.name);
            print("攻击力：" + attackOwner.status.damage + " 防御力：" + attackReceiver.status.defense);
            print("命中率：" + attackOwner.status.shootingRate + " 躲避率" + attackReceiver.status.escapeRate);
            //命中率-躲避率 = 命中概率。如果在这个范围内，则命中
            if (Random.Range(0, 100) < (attackOwner.status.shootingRate - attackReceiver.status.escapeRate))
            {
                //根据攻防计算伤害
                attackData = attackOwner.status.damage - attackReceiver.status.defense + Random.Range(-2, 2);
                if(attackData < 0)
                {
                    attackData = 0;
                }
                //显示战斗信息
                battleInfoPanelScript.ChangeBattleInfoText(attackOwner.gameObject.name + "造成伤害： " + attackData);
                StartCoroutine(WaitForTakeDamage());
            }
            else
            {
                attackData = 0;
                //显示躲避成功的文字
                battleInfoPanelScript.ChangeBattleInfoText(attackReceiver.gameObject.name + "闪避成功!");
            }

            //在对象承受伤害并进入下个单位操作前前添加1s延迟
            StartCoroutine(WaitForNextTurn());
        }

        public void BattleWin()
        {

        }

        public void BattleLose()
        {

        }

        public void BattleEscape()
        {
            battlePanel.SetActive(false);
            battleInfoPanel.SetActive(true);
            if (Random.Range(0, 100) < escapeRate)
            {
                battleInfoPanelScript.ChangeBattleInfoText("逃跑成功！");
                StartCoroutine(UnloadScene());
            }
            else
            {
                battleInfoPanelScript.ChangeBattleInfoText("逃跑失败！");
                StartCoroutine(WaitForNextTurn());
            }
            //battleInfoPanelScript.ChangeBattleInfoText("逃跑失败！");
            //StartCoroutine(WaitForNextTurn());
        }

        IEnumerator UnloadScene()
        {
            yield return new WaitForSeconds(2);
            SceneManager.UnloadSceneAsync("Battle");
        }
    }
}
