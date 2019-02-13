using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace MetalMax
{
	public class HotFix : MonoBehaviour
	{
        private LuaEnv luaEnv;

        private void Awake()
        {
            luaEnv = new LuaEnv();
            luaEnv.AddLoader(MyLoader);
            luaEnv.DoString("require 'Main'");
        }

        private byte[] MyLoader(ref string filePath)
        {
            string absPath = @"C:\Users\YDC\Documents\UnityProjects\Metal-Max\Assets\Hotfix\" + filePath + ".lua.txt";
            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
        }

        private void OnDisable()
        {
            luaEnv.DoString("require 'MainDispose'");
        }

        private void OnDestroy()
        {
            luaEnv.Dispose();
        }
    }
}