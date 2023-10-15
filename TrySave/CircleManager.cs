using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public static CircleManager Instance; // ��̬ʵ��
    public GameObject circlePrefab; // Բ�ε�Ԥ����
    private void Awake()
    {
        // ���þ�̬ʵ��Ϊ��ǰʵ��
        Instance = this;
    }
    private void Start()
    {
        
    }
    // ������Ϸ���ݵ��浵
    public void SaveGame()
    {
        // ���֮ǰ�Ĵ浵����
        PlayerPrefs.DeleteAll();
        //print(circleDataList.Count);
        // ����ÿ��Բ������
        for (int i = 0; i < CircleDataListWrapper.Instance.circleDataList.Count; i++)
        {
            CircleData circleData = CircleDataListWrapper.Instance.circleDataList[i];
            string key = "Circle" + i.ToString();

            // ����Բ��λ��
            PlayerPrefs.SetFloat(key + "_PosX", circleData.position.x);
            PlayerPrefs.SetFloat(key + "_PosY", circleData.position.y);

            // ����Բ�Ĵ�С
            PlayerPrefs.SetFloat(key + "_Size", circleData.size);

            // ����Բ����ɫ
            PlayerPrefs.SetFloat(key + "_ColorR", circleData.color.r);
            PlayerPrefs.SetFloat(key + "_ColorG", circleData.color.g);
            PlayerPrefs.SetFloat(key + "_ColorB", circleData.color.b);
        }

        // ����Բ������
        PlayerPrefs.SetInt("CircleCount", CircleDataListWrapper.Instance.circleDataList.Count);

        // ����浵
        PlayerPrefs.Save();
        // ��ӵ�����Ϣ
        //Debug.Log("Saved " + circleDataList.Count + " circles.");
    }

    // �Ӵ浵�м�����Ϸ����
    public void LoadGame()
    {
        // ����֮ǰ��Բ����Ϸ����
        //˼·һ�������и����ٵ�������Ϊĳ����������Ӷ���Ȼ�������������Ӷ���
        /*foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }*/
        //˼·���������п����ٵ��������һ������������ٵĽű����������иýű����ص���Ϸ����
        //˼·��������Բ
        // �ҵ�������֮ǰ��Բ����Ϸ����
        CircleData[] previousCircles = FindObjectsOfType<CircleData>();
        foreach (CircleData circleData in previousCircles)
        {
            Destroy(circleData.gameObject);
        }

        // ���֮ǰ��Բ������
        CircleDataListWrapper.Instance.circleDataList.Clear();

        // ��ȡԲ������
        int circleCount = PlayerPrefs.GetInt("CircleCount", 0);
        // ��ӵ�����Ϣ
        //Debug.Log("Loaded " + circleCount + " circles.");
        // ����ÿ��Բ������
        for (int i = 0; i < circleCount; i++)
        {
            string key = "Circle" + i.ToString();

            // ��ӵ�����Ϣ
            //Debug.Log("Loading data for circle " + i);

            // ��ȡԲ��λ��
            float posX = PlayerPrefs.GetFloat(key + "_PosX");
            float posY = PlayerPrefs.GetFloat(key + "_PosY");
            Vector3 position = new Vector3(posX, posY, 0f);

            // ��ȡԲ�Ĵ�С
            float size = PlayerPrefs.GetFloat(key + "_Size");

            // ��ȡԲ����ɫ
            float colorR = PlayerPrefs.GetFloat(key + "_ColorR");
            float colorG = PlayerPrefs.GetFloat(key + "_ColorG");
            float colorB = PlayerPrefs.GetFloat(key + "_ColorB");
            Color color = new Color(colorR, colorG, colorB);

            // ����Բ�ζ�����������
            GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity);
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = color;
            circle.transform.localScale = new Vector3(size, size, 1f);

            // ��ȡ�����CircleData�ű�
            CircleData circleDataScript = circle.GetComponent<CircleData>();
            if (circleDataScript == null)
            {
                circleDataScript = circle.AddComponent<CircleData>();
            }

            // ��CircleData�ű���ֵ
            circleDataScript.position = position;
            circleDataScript.size = size;
            circleDataScript.color = color;

            // ��ӵ�CircleManager��circleDataList�б���
            CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);
        }
    }

    // ����浵����
    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}