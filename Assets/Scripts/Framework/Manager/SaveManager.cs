using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MetalMax
{
    public class SaveManager : MonoBehaviour 
	{
        private static string archiveFilePath = "Resources/Data/Archive.json";
        private static string personEquipmentInfoFilePath = "Resources/Data/PersonEquipmentInfo.json";
        private static string tankEquipmentInfoFilePath = "Resources/Data/TankEquipmentInfo.json";
        private static string bossInfoFilePath = "Resources/Data/BossInfo.json";
        private static string NPCInfoFilePath = "Resources/Data/NPCInfo.json";
        public static Archive currentArchive;

        public static string GetJsonStringFromFile(string filePath)
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

        /// <summary>
        /// Json解析字符串为对象的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">想要读取文件的相对路径。如Resources/Data/PersonEquipmentInfo.json</param>
        /// <returns>返回对象的一个列表</returns>
        public static List<T> GetObjectListFromJsonString<T>(string filePath)
        {
            string jsonText = GetJsonStringFromFile(filePath);
            List<T> itemList = JsonMapper.ToObject<List<T>>(jsonText);
            return itemList;
        }

        /// <summary>
        /// 保存一个对象到json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="filePath">想要保存文件的相对路径。如Resources/Data/PersonEquipmentInfo.json</param>
        public static void SaveObject2JsonFile<T>(T t)
        {
            string jsonText = "[" + JsonMapper.ToJson(t) + "]";
            string path = Path.Combine(Application.dataPath, archiveFilePath);
            StreamWriter sw = new StreamWriter(path);   //利用写入流创建文件
            sw.Write(jsonText);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();
        }

        /// <summary>
        /// 检查是否有存档
        /// </summary>
        /// <returns></returns>
        public static bool Check4Archive()
        {
            string path = Path.Combine(Application.dataPath, archiveFilePath);
            if (File.Exists(path))
            {
                List<Archive> archiveList = GetObjectListFromJsonString<Archive>(archiveFilePath);
                if (archiveList != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 通过人类装备的id找到对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PersonEquipment GetPersonEquipmentObjectFromListById(int id)
        {
            List<PersonEquipment> objList = GetObjectListFromJsonString<PersonEquipment>(personEquipmentInfoFilePath);
            foreach (var item in objList)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过Tank装备的id找到对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TankEquipment GetTankEquipmentObjectFromListById(int id)
        {
            List<TankEquipment> objList = GetObjectListFromJsonString<TankEquipment>(tankEquipmentInfoFilePath);
            foreach (var item in objList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过BOSS的id找到对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Boss GetBossObjectFromListById(int id)
        {
            List<Boss> objList = GetObjectListFromJsonString<Boss>(bossInfoFilePath);
            foreach (var item in objList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过NPC的id找到对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NPCInfo GetNPCjectFromListById(int id)
        {
            List<NPCInfo> objList = GetObjectListFromJsonString<NPCInfo>(NPCInfoFilePath);
            foreach (var item in objList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }


        /// <summary>
        /// 通过id找存档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Archive GetArchiveById(int id)
        {
            Archive archive = GetObjectListFromJsonString<Archive>(archiveFilePath)[id - 1];
            return archive;
        }

        /// <summary>
        /// 存档。覆盖原有存档
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="id"></param>
        public static void SaveArchiveById(Archive archive, int id = 1)
        {
            List<Archive> tmpList = new List<Archive>();

            //将其他不需要修改的存档存入在一个tmpList里
            List<Archive> archiveList = GetObjectListFromJsonString<Archive>(archiveFilePath);
            foreach (var item in archiveList)
            {
                if(item.Id != id)
                {
                    tmpList.Add(item);
                }
            }

            //将需要修改的存档也一并存入tmpList
            tmpList.Add(archive);

            //将list存入文件
            string jsonText = JsonMapper.ToJson(tmpList);
            string path = Path.Combine(Application.dataPath, archiveFilePath);
            StreamWriter sw = new StreamWriter(path);   //利用写入流创建文件
            sw.Write(jsonText);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();
        }

        /// <summary>
        /// 不覆盖，直接存档
        /// </summary>
        /// <param name="archiveList"></param>
        public static void SaveArchiveList(List<Archive> archiveList)
        {
            print(archiveList.Count);
            string jsonText = null;
            if (archiveList.Count > 0)
            {
                jsonText = JsonMapper.ToJson(archiveList);
            }
            else
            {
                jsonText = null;
            }
            //将list存入文件
            string path = Path.Combine(Application.dataPath, archiveFilePath);
            StreamWriter sw = new StreamWriter(path);   //利用写入流创建文件
            sw.Write(jsonText);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();
        }

        public static void SaveCurrentArchive(Archive archive)
        {
            if (archive != null)
            {
                currentArchive = archive;
            }
        }
    }
}
