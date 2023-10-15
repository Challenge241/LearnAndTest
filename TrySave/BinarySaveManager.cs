using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveManager : MonoBehaviour
{
    [SerializeField]
    private string saveFileName = "circle_save.dat"; // 存档文件名

    public void SaveCircles(List<CircleData> circleDataList)
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        // 创建二进制序列化器
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        // 创建文件流
        FileStream fileStream = File.Create(filePath);

        // 序列化圆的数据并保存到文件中
        binaryFormatter.Serialize(fileStream, circleDataList);

        fileStream.Close();
    }

    public List<CircleData> LoadCircles()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            // 创建二进制序列化器
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // 打开文件流
            FileStream fileStream = File.Open(filePath, FileMode.Open);

            // 反序列化数据并获取圆的数据列表
            List<CircleData> circleDataList = (List<CircleData>)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            return circleDataList;
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return new List<CircleData>();
        }
    }
}
