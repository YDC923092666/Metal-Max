using UnityEngine;
using System.Collections;

namespace MetalMax
{
    /// <summary>
    /// 消耗品类
    /// </summary>
    public class Consumable : Item
    {
        public int hp;

        public override string GetToolTipText()
        {
            string text = base.GetToolTipText();

            string newText = string.Format("{0}\n\n<color=blue>加血：{1}\n</color>", text, hp);

            return newText;
        }

        public override string ToString()
        {
            string s = "";
            s += id.ToString();
            s += itemType;
            s += itemQuality;
            s += description;
            s += capacity;
            s += buyPrice;
            s += sellPrice;
            s += sprite;
            s += hp;
            return s;
        }

    }
}