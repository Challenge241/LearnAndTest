using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CircleDataListWrapper : MonoBehaviour
{
    public static CircleDataListWrapper Instance; // ��̬ʵ��
    public List<CircleData> circleDataList;
    private void Awake()
    {
        Instance=this;
    }
    public CircleDataListWrapper(List<CircleData> circleDataList)
    {
        this.circleDataList = circleDataList;
    }
}
