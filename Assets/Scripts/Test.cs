using ExcelDataReader;
using System.IO;
using UnityEngine;
using System.Data;
using System.Collections.Generic;
using System;

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
                int count = InventoryManager.Instance.itemList.Count;
                int id = UnityEngine.Random.Range(0, count);
                Item item = InventoryManager.Instance.itemList[id];
                KnapsackPanel.Instance.StoreItem(item);
            }
        }

        public void TestArray()
        {
            var filePath = Const.monsterFilePath;
            ReadExcelStream(filePath);
        }

        void ReadExcelStream(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();

            int columns = result.Tables[0].Columns.Count;
            int rows = result.Tables[0].Rows.Count;

            List<BaseAttr> monsterList = new List<BaseAttr>();
            
            for (int i = 1; i < rows; i++)
            {
                BaseAttr monster = new BaseAttr();
                for (int j = 1; j < columns; j++)
                {
                    var nValue = result.Tables[0].Rows[i][j];
                    if (j == 1)
                    {
                        monster.nameString = nValue.ToString();
                    }
                    else if (j == 2)
                    {
                        monster.id = Convert.ToInt32(nValue);
                    }
                    else if (j == 3)
                    {
                        monster.hp = Convert.ToInt32(nValue);
                    }
                    else if (j == 4)
                    {
                        monster.attackCount = Convert.ToInt32(nValue);
                    }
                    else if (j == 5)
                    {
                        monster.damage = Convert.ToInt32(nValue);
                    }
                    else if (j == 6)
                    {
                        monster.defense = Convert.ToInt32(nValue);
                    }
                    else if (j == 7)
                    {
                        monster.speed = Convert.ToInt32(nValue);
                    }
                    else if (j == 8)
                    {
                        monster.shootingRate = Convert.ToInt32(nValue);
                    }
                    else if (j == 9)
                    {
                        monster.escapeRate = Convert.ToInt32(nValue);
                    }
                    else if (j == 10)
                    {
                        monster.sprite = nValue.ToString();
                    }
                }
                monsterList.Add(monster);
            }
            MonsterJson monsterJson = new MonsterJson
            {
                infoList = monsterList
            };
            string text = JsonUtility.ToJson(monsterJson);
            print(text);
            excelDataReader.Close();
        }

    }
}