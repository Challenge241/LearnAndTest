using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XClose : MonoBehaviour
{
    public GameObject targetGameObject; // ָ������Ϸ���󣬿�����Inspector�����û�ͨ����������
    public void Xone()
    {
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(false); // ��ָ������Ϸ��������Ϊ�ǻ״̬
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }
    }
}
