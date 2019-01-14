using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class CharacterPanel : BasePanel
    {
        private static CharacterPanel _instance;
        public static CharacterPanel Instance
        {
            get
            {
                return _instance;
            }
        }

        public Slot[] slotList; //面板上所有的格子
        private Text nameText;
        private Text lvText;
        private Text hpText;
        private Text damageText;
        private Text defenseText;
        private Text speedText;
        private Text expText;

        private int totalHp = 0;
        private int totalDamage = 0;
        private int totalDefense = 0;
        private int totalSpeed = 0;

        private void Awake()
        {
            _instance = this;
        }

        protected override void Start()
        {
            base.Start();
            slotList = GetComponentsInChildren<Slot>();
            nameText = GameObject.Find("RightPanel/NameText").GetComponent<Text>();
            lvText = GameObject.Find("RightPanel/LvText").GetComponent<Text>();
            hpText = GameObject.Find("RightPanel/HpText").GetComponent<Text>();
            damageText = GameObject.Find("RightPanel/DamageText").GetComponent<Text>();
            defenseText = GameObject.Find("RightPanel/DefenseText").GetComponent<Text>();
            speedText = GameObject.Find("RightPanel/SpeedText").GetComponent<Text>();
            expText = GameObject.Find("RightPanel/ExpText").GetComponent<Text>();
        }

        /// <summary>
        /// 装上装备
        /// </summary>
        /// <param name="item"></param>
        public void PutOn(Item item)
        {
            Item exitItem = null;
            if(slotList == null)
            {
                slotList = GetComponentsInChildren<Slot>();
            }
            foreach (Slot slot in slotList)
            {
                EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
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
            UpdateUI();
        }

        /// <summary>
        /// 更新角色面板右侧数值区
        /// </summary>
        public void UpdateUI()
        {
            if (slotList == null)
            {
                slotList = GetComponentsInChildren<Slot>();
            }
            var personStatus = SaveManager.currentArchive.personStatus;
            
            if (nameText == null) nameText = GameObject.Find("RightPanel/NameText").GetComponent<Text>();
            if (lvText == null) lvText = GameObject.Find("RightPanel/LvText").GetComponent<Text>();
            if (hpText == null) hpText = GameObject.Find("RightPanel/HpText").GetComponent<Text>();
            if(damageText == null) damageText = GameObject.Find("RightPanel/DamageText").GetComponent<Text>();
            if (defenseText == null) defenseText = GameObject.Find("RightPanel/DefenseText").GetComponent<Text>();
            if(speedText == null) speedText = GameObject.Find("RightPanel/SpeedText").GetComponent<Text>();
            if(expText == null) expText = GameObject.Find("RightPanel/ExpText").GetComponent<Text>();

            nameText.text = personStatus.personName;
            lvText.text = personStatus.personLv.ToString();
            hpText.text = string.Format("{0}/{1}", personStatus.personCurrentHp, personStatus.personMaxHp);
            damageText.text = personStatus.personDamage.ToString();
            defenseText.text = personStatus.personDefense.ToString();
            speedText.text = personStatus.personSpeed.ToString();
            expText.text = string.Format("{0}/{1}", personStatus.personExp, personStatus.personCurrentLvNeedExp);
        }

        public void ChangeArchive()
        {
            foreach (Slot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    PersonEquipment item = (PersonEquipment)slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                    totalHp += item.hp;
                    totalDamage += item.damage;
                    totalDefense += item.defense;
                    totalSpeed += item.speed;
                }
            }
            var personStatus = SaveManager.currentArchive.personStatus;
            //personStatus.personMaxHp =
        }

        public override void OnEnter(string content)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isShow = true;
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
        }
    }
}