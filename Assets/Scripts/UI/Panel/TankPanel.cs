using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
	public class TankPanel : BasePanel 
	{
        private static TankPanel _instance;
        public static TankPanel Instance
        {
            get
            {
                return _instance;
            }
        }

        private Slot[] slotArray; //面板上所有的格子
        private Text nameText;
        private Text spText;
        private Text damageText;
        private Text defenseText;
        private Text normalBulletText;
        private Text ironBulletText;
        private Text smokeBulletText;

        private int totalSp = 0;
        private int totalDamage = 0;
        private int totalDefense = 0;
        private int totalnormalBullet = 0;
        private int totalironBullet = 0;
        private int totalsmokeBullet = 0;

        private void Awake()
        {
            _instance = this;
        }

        protected override void Start()
        {
            base.Start();
            slotArray = GetComponentsInChildren<Slot>();
            nameText = transform.Find("RightPanel/Text/NameText").GetComponent<Text>();
            spText = transform.Find("RightPanel/Text/SpText").GetComponent<Text>();
            damageText = transform.Find("RightPanel/Text/DamageText").GetComponent<Text>();
            defenseText = transform.Find("RightPanel/Text/DefenseText").GetComponent<Text>();
            normalBulletText = transform.Find("RightPanel/Text/NormalBulletText").GetComponent<Text>();
            ironBulletText = transform.Find("RightPanel/Text/IronBulletText").GetComponent<Text>();
            smokeBulletText = transform.Find("RightPanel/Text/SmokeBulletText").GetComponent<Text>();
        }

        /// <summary>
        /// 装上装备
        /// </summary>
        /// <param name="item"></param>
        public void PutOn(Item item)
        {
            Item exitItem = null;
            if (slotArray == null)
            {
                slotArray = GetComponentsInChildren<Slot>();
            }
            foreach (Slot slot in slotArray)
            {
                TankEquipmentSlot equipmentSlot = (TankEquipmentSlot)slot;
                //找到符合item type的格子

                if (equipmentSlot.IsRightItem(item))
                {
                    //如果格子里有装备了，则交换装备
                    if (equipmentSlot.transform.childCount > 0)
                    {
                        ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                        exitItem = currentItemUI.Item;
                        currentItemUI.SetItem(item, 1);
                    }
                    //如果没有装备，则装备上
                    else
                    {
                        equipmentSlot.StoreItem(item);
                    }
                    break;
                }
            }
            UpdateUI();
            //如果对象不为空，则代表有装备被换下来，换下来的装备要装入背包中
            if (exitItem != null)
            {
                KnapsackPanel.Instance.StoreItem(exitItem);
            }
        }


        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="item"></param>
        public void PutOff(Item item)
        {
            KnapsackPanel.Instance.StoreItem(item);
        }

        /// <summary>
        /// 更新坦克面板右侧数值区
        /// </summary>
        public void UpdateUI()
        {
            if (slotArray == null)
            {
                slotArray = GetComponentsInChildren<Slot>();
            }

            ChangeCurrentArchive();
            var tankStatus = SaveManager.GetTankStatusById(SaveManager.currentArchive.currentTankID);

            if (nameText == null) nameText = transform.Find("RightPanel/Text/NameText").GetComponent<Text>();
            if (spText == null) spText = transform.Find("RightPanel/Text/SpText").GetComponent<Text>();
            if (damageText == null) damageText = transform.Find("RightPanel/Text/DamageText").GetComponent<Text>();
            if (defenseText == null) defenseText = transform.Find("RightPanel/Text/DefenseText").GetComponent<Text>();
            if (normalBulletText == null) normalBulletText = transform.Find("RightPanel/Text/NormalBulletText").GetComponent<Text>();
            if (ironBulletText == null) ironBulletText = transform.Find("RightPanel/Text/IronBulletText").GetComponent<Text>();
            if (smokeBulletText == null) smokeBulletText = transform.Find("RightPanel/Text/SmokeBulletText").GetComponent<Text>();

            nameText.text = tankStatus.tankName;
            spText.text = tankStatus.tankSp.ToString();
            damageText.text = tankStatus.tankDamage.ToString();
            defenseText.text = tankStatus.tankDefense.ToString();
            normalBulletText.text = tankStatus.tankBulletCountDict[TankBulletType.Nomal].ToString();
            ironBulletText.text = tankStatus.tankBulletCountDict[TankBulletType.Iron].ToString();
            smokeBulletText.text = tankStatus.tankBulletCountDict[TankBulletType.Smoke].ToString();
        }

        /// <summary>
        /// 修改当前存档中，人物属性值
        /// </summary>
        public void ChangeCurrentArchive()
        {
            totalSp = 0;
            totalDamage = 0;
            totalDefense = 0;
            totalnormalBullet = 0;
            totalironBullet = 0;
            totalsmokeBullet = 0;
            foreach (Slot slot in slotArray)
            {
                if (slot.transform.childCount > 0)
                {
                    TankEquipment item = (TankEquipment)slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                    totalSp += item.sp;
                    totalDamage += item.damage;
                    totalDefense += item.defense;
                    totalnormalBullet += item.normalBulletCount;
                    totalironBullet += item.ironBulletCount;
                    totalsmokeBullet += item.smokeBulletCount;
                }
            }
            var tankStatus = SaveManager.GetTankStatusById(SaveManager.currentArchive.currentTankID);
            tankStatus.tankSp = totalSp;
            tankStatus.tankDamage = totalDamage;
            tankStatus.tankDefense = totalDefense;
            if(tankStatus.tankBulletCountDict == null)
            {
                tankStatus.tankBulletCountDict = new Dictionary<TankBulletType, int>();
            }

            if (tankStatus.tankBulletCountDict.ContainsKey(TankBulletType.Nomal))
            {
                tankStatus.tankBulletCountDict[TankBulletType.Nomal] = totalnormalBullet;
            }
            else
            {
                tankStatus.tankBulletCountDict.Add(TankBulletType.Nomal, totalnormalBullet);
            }


            if (tankStatus.tankBulletCountDict.ContainsKey(TankBulletType.Iron))
            {
                tankStatus.tankBulletCountDict[TankBulletType.Iron] = totalironBullet;
            }
            else
            {
                tankStatus.tankBulletCountDict.Add(TankBulletType.Iron, totalironBullet);
            }

            if (tankStatus.tankBulletCountDict.ContainsKey(TankBulletType.Smoke))
            {
                tankStatus.tankBulletCountDict[TankBulletType.Smoke] = totalsmokeBullet;
            }
            else
            {
                tankStatus.tankBulletCountDict.Add(TankBulletType.Smoke, totalsmokeBullet);
            }
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
            UIManager.Instance.canClickUIButton = false;
            UpdateUI();
        }

        public override void OnPause()
        {

        }

        public override void OnResume()
        {

        }

        public override void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isShow = false;
            UIManager.Instance.canClickUIButton = true;
        }
    }
}
