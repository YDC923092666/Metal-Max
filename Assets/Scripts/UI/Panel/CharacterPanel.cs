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

        private Slot[] slotArray; //面板上所有的格子
        private Text nameText;
        private Text lvText;
        private Text hpText;
        private Text damageText;
        private Text defenseText;
        private Text speedText;
        private Text expText;
        private Text shootingRateText;
        private Text escapeRateText;

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
            slotArray = GetComponentsInChildren<Slot>();
            nameText = GameObject.Find("RightPanel/NameText").GetComponent<Text>();
            lvText = GameObject.Find("RightPanel/LvText").GetComponent<Text>();
            hpText = GameObject.Find("RightPanel/HpText").GetComponent<Text>();
            damageText = GameObject.Find("RightPanel/DamageText").GetComponent<Text>();
            defenseText = GameObject.Find("RightPanel/DefenseText").GetComponent<Text>();
            speedText = GameObject.Find("RightPanel/SpeedText").GetComponent<Text>();
            expText = GameObject.Find("RightPanel/ExpText").GetComponent<Text>();
            shootingRateText = GameObject.Find("RightPanel/ShootingRateText").GetComponent<Text>();
            escapeRateText = GameObject.Find("RightPanel/EscapeRateText").GetComponent<Text>();
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
        }

        /// <summary>
        /// 更新角色面板右侧数值区
        /// </summary>
        public void UpdateUI()
        {
            if (slotArray == null)
            {
                slotArray = GetComponentsInChildren<Slot>();
            }

            ChangeCharactorAttr();

            if (nameText == null) nameText = GameObject.Find("RightPanel/NameText").GetComponent<Text>();
            if (lvText == null) lvText = GameObject.Find("RightPanel/LvText").GetComponent<Text>();
            if (hpText == null) hpText = GameObject.Find("RightPanel/HpText").GetComponent<Text>();
            if(damageText == null) damageText = GameObject.Find("RightPanel/DamageText").GetComponent<Text>();
            if (defenseText == null) defenseText = GameObject.Find("RightPanel/DefenseText").GetComponent<Text>();
            if(speedText == null) speedText = GameObject.Find("RightPanel/SpeedText").GetComponent<Text>();
            if(expText == null) expText = GameObject.Find("RightPanel/ExpText").GetComponent<Text>();
            if(shootingRateText == null) shootingRateText = GameObject.Find("RightPanel/ShootingRateText").GetComponent<Text>();
            if (escapeRateText == null) escapeRateText = GameObject.Find("RightPanel/EscapeRateText").GetComponent<Text>();

            var personStatus = GameObject.FindGameObjectWithTag(Tags.charactor).GetComponent<Charactor>();
            nameText.text = personStatus.nameString;
            lvText.text = personStatus.lv.ToString();
            hpText.text = string.Format("{0}/{1}", personStatus.hp, personStatus.maxHp);
            damageText.text = personStatus.damage.ToString();
            defenseText.text = personStatus.defense.ToString();
            speedText.text = personStatus.speed.ToString();
            expText.text = string.Format("{0}/{1}", personStatus.exp, personStatus.maxExp);
            shootingRateText.text = personStatus.shootingRate.ToString();
            escapeRateText.text = personStatus.escapeRate.ToString();
        }

        /// <summary>
        /// 修改当前存档中，人物属性值
        /// </summary>
        public void ChangeCharactorAttr()
        {
            //计算装备加的属性值
            totalHp = 0;
            totalDamage = 0;
            totalDefense = 0;
            totalSpeed = 0;
            foreach (Slot slot in slotArray)
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
            //修改人物属性
            var personStatus = GameObject.FindGameObjectWithTag(Tags.charactor).GetComponent<Charactor>();
            personStatus.maxHp = personStatus.initHp + totalHp;
            personStatus.damage = personStatus.initDamage + totalDamage;
            personStatus.defense = personStatus.initDefense + totalDefense;
            personStatus.speed = personStatus.initSpeed + totalSpeed;
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