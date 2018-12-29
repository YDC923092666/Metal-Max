using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
	public class GUIManager: MonoSingleton<GUIManager>
	{
        protected override void Awake()
        {
            base.Awake();
        }

        private static Dictionary<string, GameObject> mPanelDict = new Dictionary<string, GameObject>();

        public void SetResolution(float width, float height, float macthWidthOrHeight)
        {
            var canvas = GameObject.Find("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = macthWidthOrHeight;
        }

        public void UnLoadPanel(string panelName)
        {
            if (mPanelDict.ContainsKey(panelName))
            {
                Object.Destroy(mPanelDict[panelName]);
            }
        }

        public void LoadPanel(string filePath, string panelName, GameObject parent)
        {

        }

        #region 00_Home
        /// <summary>
        /// 选择存档页面，点击“重新开始”按钮
        /// </summary>
        public void OnRestartGameButtonClick()
        {

        }
        #endregion
    }
}
