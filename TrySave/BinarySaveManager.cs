using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveManager : MonoBehaviour
{
    [SerializeField]
    private string saveFileName = "circle_save.dat"; // �浵�ļ���

    public void SaveCircles(List<CircleData> circleDataList)
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        // �������������л���
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        // �����ļ���
        FileStream fileStream = File.Create(filePath);

        // ���л�Բ�����ݲ����浽�ļ���
        binaryFormatter.Serialize(fileStream, circleDataList);

        fileStream.Close();
    }

    public List<CircleData> LoadCircles()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            // �������������л���
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // ���ļ���
            FileStream fileStream = File.Open(filePath, FileMode.Open);

            // �����л����ݲ���ȡԲ�������б�
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
