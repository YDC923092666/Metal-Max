using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MetalMax
{
	public class Test : MonoBehaviour
	{
        public void TestJson()
        {
            string ta = GetJsonStringFromFile(Const.NPCInfoFilePath);
            NPCInfoJson jsonObject = JsonUtility.FromJson<NPCInfoJson>(ta);
            foreach (var item in jsonObject.infoList)
            {
                print(item.name);
            }
        }

        public string GetJsonStringFromFile(string filePath)
        {
            string strReadFilePath = Path.Combine(Application.dataPath, filePath);
            StreamReader srReadFile = new StreamReader(strReadFilePath);
            string jsonText = "";
            while (!srReadFile.EndOfStream)
            {
                jsonText += srReadFile.ReadLine();
            }
            return jsonText;
        }

    }
}