using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    public GameObject[] objects; // ��������

    public void ActivateObjectByIndex(int activeObjectIndex)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetActive(i == activeObjectIndex); // ���ݱ�ż������ö���
            }
        }
    }
}
