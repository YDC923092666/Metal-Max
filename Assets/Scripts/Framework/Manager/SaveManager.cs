using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MetalMax
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        public static Archive currentArchive = new Archive();

        protected override void Awake()
        {
            base.Awake();
        }

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
        /// 解析字符串为Json对象
        /// </summary>
        /// <typeparam name="T">想要解析成的json对象</typeparam>
        /// <param name="filePath">想要读取文件的相对路径。如Resources/Data/PersonEquipmentInfo.json</param>
        /// <returns>返回对象</returns>
        public static T GetObjectFromJsonString<T>(string filePath)
        {
            string jsonText = GetJsonStringFromFile(filePath);
            T jsonObject = JsonUtility.FromJson<T>(jsonText);
            return jsonObject;
        }

        /// <summary>
        /// 保存一个对象到json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="filePath">想要保存文件的相对路径。如Resources/Data/PersonEquipmentInfo.json</param>
        public static void SaveObject2JsonFile<T>(T t)
        {
            string jsonText = JsonUtility.ToJson(t);
            string path = Path.Combine(Application.dataPath, Const.archiveFilePath);
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
            string path = Path.Combine(Application.dataPath, Const.archiveFilePath);
            if (File.Exists(path))
            {
                ArchiveJson archivejsonObject = GetObjectFromJsonString<ArchiveJson>(Const.archiveFilePath);
                if (archivejsonObject != null)
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
            PersonEquipmentJson jsonObject = GetObjectFromJsonString<PersonEquipmentJson>(Const.personEquipmentInfoFilePath);
            foreach (var item in jsonObject.infoList)
            {
                if(item.id == id)
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
            TankEquipmentJson jsonObject = GetObjectFromJsonString<TankEquipmentJson>(Const.tankEquipmentInfoFilePath);
            foreach (var item in jsonObject.infoList)
            {
                if (item.id == id)
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
            BossJson jsonObject = GetObjectFromJsonString<BossJson>(Const.bossInfoFilePath);
            foreach (var item in jsonObject.infoList)
            {
                if (item.id == id)
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
            NPCInfoJson jsonObject = GetObjectFromJsonString<NPCInfoJson>(Const.NPCInfoFilePath);
            foreach (var item in jsonObject.infoList)
            {
                if (item.id == id)
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
            ArchiveJson jsonObject = GetObjectFromJsonString<ArchiveJson>(Const.archiveFilePath);
            return jsonObject.infoList[id - 1];
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
            ArchiveJson jsonObject = GetObjectFromJsonString<ArchiveJson>(Const.archiveFilePath);
            foreach (var item in jsonObject.infoList)
            {
                if(item.id != id)
                {
                    tmpList.Add(item);
                }
            }

            //将需要修改的存档也一并存入tmpList
            tmpList.Add(archive);
            Dictionary<string, List<Archive>> dict = new Dictionary<string, List<Archive>>();
            dict.Add("infoList", tmpList);

            //将dict存入文件
            string jsonText = JsonUtility.ToJson(dict);
            string path = Path.Combine(Application.dataPath, Const.archiveFilePath);
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
                jsonText = JsonUtility.ToJson(archiveList);
            }
            else
            {
                jsonText = null;
            }
            //将list存入文件
            string path = Path.Combine(Application.dataPath, Const.archiveFilePath);
            StreamWriter sw = new StreamWriter(path);   //利用写入流创建文件
            sw.Write(jsonText);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();
        }

        /// <summary>
        /// 临时存档，保存在内存中。用于游戏中实时读取和修改
        /// </summary>
        /// <param name="archive"></param>
        public static void SaveCurrentArchive(Archive archive)
        {
            if (archive != null)
            {
                currentArchive = archive;
            }
        }

        public static TankStatus GetTankStatusById(int id)
        {
            foreach (var item in currentArchive.tankStatusList)
            {
                if(item.tankID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static void SaveTankStatus(TankStatus tankStatus)
        {
            if(currentArchive.tankStatusList == null)
            {
                currentArchive.tankStatusList = new List<TankStatus>();
            }
            currentArchive.tankStatusList.Add(tankStatus);
        }
    }
}
