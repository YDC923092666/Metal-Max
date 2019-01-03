using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MetalMax
{
    public enum UIPanelType
    {
        ItemMessage,
        Knapsack,
        MainMenu,
        System,
        Task,
    }

    public class UIPanelInfo
    { 
        public string panelType;
        public string path;
    }
}