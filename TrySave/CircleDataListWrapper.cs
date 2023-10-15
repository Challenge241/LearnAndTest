using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CircleDataListWrapper : MonoBehaviour
{
    public static CircleDataListWrapper Instance; // ¾²Ì¬ÊµÀý
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
