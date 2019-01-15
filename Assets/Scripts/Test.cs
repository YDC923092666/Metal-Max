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
            string ta = GetJsonStringFromFile(Const.personEquipmentInfoFilePath);
            PersonEquipmentJson jsonObject = JsonUtility.FromJson<PersonEquipmentJson>(ta);
            foreach (var item in jsonObject.infoList)
            {
                print(item.personEquipmentType.ToString());
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                int id = UnityEngine.Random.Range(1, 31);
                KnapsackPanel.Instance.StoreItem(id);
            }
        }

        private Array[] arrays;
        public void TestArray()
        {
            
            if(arrays == null)
            {
                print("1");
            }
            print("2");
        }

    }
}