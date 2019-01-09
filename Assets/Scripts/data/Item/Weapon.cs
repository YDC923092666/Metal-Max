using UnityEngine;
using System.Collections;

namespace MetalMax
{
    /// <summary>
    /// 武器
    /// </summary>
    public class Weapon : Item
    {

        public int Damage { get; set; }

        public WeaponType WpType { get; set; }

        public enum WeaponType
        {
            None,
            OffHand,
            MainHand
        }


        public override string GetToolTipText()
        {
            string text = base.GetToolTipText();

            string wpTypeText = "";

            switch (WpType)
            {
                case WeaponType.OffHand:
                    wpTypeText = "副手";
                    break;
                case WeaponType.MainHand:
                    wpTypeText = "主手";
                    break;
            }

            string newText = string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", text, wpTypeText, Damage);

            return newText;
        }
    }
}