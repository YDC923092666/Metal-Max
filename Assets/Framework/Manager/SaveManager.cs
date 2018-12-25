using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace MetalMax
{
	public class SaveManager : MonoBehaviour 
	{
        private static string archiveFilePath = "Resources/Data/Archive.json";

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
        /// Json解析字符串为对象的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">想要读取文件的相对路径。如Resources/Data/PersonEquipmentInfo.json</param>
        /// <returns>返回对象的一个数组</returns>
        public static T[] GetObjectArrayFromJsonString<T>(string filePath)
        {
            string jsonText = GetJsonStringFromFile(filePath);
            T[] itemArray = JsonMapper.ToObject<T[]>(jsonText);
            return itemArray;
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
                return true;
            }
            return false;
        }

        public static T GetObjectFormArrayById<T>(int id)
        {
            T[] objArray = GetObjectArrayFromJsonString<T>(archiveFilePath);
            return objArray[0];
        }

        /// <summary>
        /// 通过id找存档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Archive GetArchiveById(int id)
        {
            Archive archive = GetObjectArrayFromJsonString<Archive>(archiveFilePath)[id - 1];
            return archive;
        }

        /// <summary>
        /// 存档。覆盖原有存档
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="id"></param>
        public static void SaveArchiveById(Archive archive, int id)
        {
            string jsonText = "[" + JsonMapper.ToJson(t) + "]";
            string path = Path.Combine(Application.dataPath, archiveFilePath);
            StreamWriter sw = new StreamWriter(path);   //利用写入流创建文件
            sw.Write(jsonText);     //写入数据
            sw.Close();     //关闭流
            sw.Dispose();
        }
    }
}
