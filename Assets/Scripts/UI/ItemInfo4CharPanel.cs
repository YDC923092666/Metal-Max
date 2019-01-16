using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MetalMax
{
    public class ItemInfo4CharPanel : BasePanel
    {
        private Text contentText;
        private Button takeOffButton;
        private Button moreButton;

        protected override void Start()
        {
            base.Start();
            contentText = transform.Find("Content").GetComponent<Text>();

            //给“卸下”按钮添加点击事件
            takeOffButton = transform.Find("ButtonGroup/TakeOffButton").GetComponent<Button>();
            takeOffButton.onClick.AddListener(() =>
            {
                OnTakeOffButtonClick();
            });

            //给“丢弃”按钮添加点击事件
            moreButton = transform.Find("ButtonGroup/DropButton").GetComponent<Button>();
            moreButton.onClick.AddListener(() =>
            {
                UIManager.Instance.OnDropButtonClick();
            });
        }

        public override void OnEnter(string content)
        {
            if (contentText == null)
            {
                contentText = transform.Find("Content").GetComponent<Text>();
            }
            contentText.text = content;
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
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            isShow = true;
        }

        public override void OnExit()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            isShow = false;
        }

        public void OnTakeOffButtonClick()
        {
            //弹出当前最高层（ItemInfoPanel）
            UIManager.Instance.PopPanel();
            var item = UIManager.Instance.selectedSlot.GetComponentInChildren<ItemUI>().Item;
            switch (item.itemType)
            {
                case ItemType.PersonEquipment:
                    CharacterPanel.Instance.PutOff(item);
                    DestroyImmediate(UIManager.Instance.selectedSlot.transform.GetChild(0).gameObject);
                    CharacterPanel.Instance.UpdateUI();
                    break;
                case ItemType.Tank:
                    CharacterPanel.Instance.PutOff(item);
                    DestroyImmediate(UIManager.Instance.selectedSlot.transform.GetChild(0).gameObject);
                    GameManager.Instance.isEquipTank = false;
                    SaveManager.currentArchive.currentTankID = 0;
                    break;
                case ItemType.TankEquipment:
                    TankPanel.Instance.PutOff(item);
                    DestroyImmediate(UIManager.Instance.selectedSlot.transform.GetChild(0).gameObject);
                    TankPanel.Instance.UpdateUI();
                    break;
            }
        }
    }
}