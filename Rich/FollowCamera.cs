using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // �����Ŀ�����
    public Vector3 offset; // ��Ŀ���������ͷ��ƫ����

    private void LateUpdate() // ��ÿһ֡�����ִ�У���ȷ��Ŀ�������ƶ��Ѿ����
    {
        transform.position = target.position + offset; // ��������ͷ��λ��ΪĿ��λ�ü���ƫ����
    }
}

