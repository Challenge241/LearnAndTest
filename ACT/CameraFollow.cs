using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // �������ǵ� Transform ���
    public float smoothSpeed = 0.125f; // ƽ���ٶȣ���������ƶ����ٶ�
    public Vector3 offset; // ƫ�ƣ����ڿ��������������ǵ�λ��
    public bool isFollowing = true;

    void FixedUpdate() // ʹ�� FixedUpdate ������ Update ���Եõ����ȶ��ĸ���Ч��
    {
        if (isFollowing && target != null)
        {
            // �趨һ��Ŀ��λ�ã���λ�û���Ŀ������λ�ã��ټ���һ��ƫ������
            // �����target��һ��Transform���󣬱�ʾ�����Ӧ��׷�ٵ�Ŀ�����offset����һ��Vector3���󣬱�ʾ����������Ŀ���ƫ������
            // ���д���Ľ����desiredPosition����һ��Vector3���󣬱�ʾ�����Ӧ����ÿһ֡�ƶ�����Ŀ��λ�á�
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

            // ʹ��Vector3��Lerp������ƽ���ز�ֵ�����������ǰλ�ú�Ŀ��λ��֮���һ����λ�á�
            // �����λ�ý����ӽ�Ŀ��λ�ã��ƶ����ٶ���smoothSpeed����������ֵ��0-1֮�䣬ֵԽ���ƶ�Խ�죩��
            // ���������������ƽ������Ŀ���ƶ���������˲���ƶ���Ŀ��λ�á�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // �����������λ�õ��¼����λ�á�
            // ��������һ֡ʱ����������ƶ���������µ�λ�ã������һ�������ƽ���ظ���Ŀ���ƶ��ĸо���
            transform.position = smoothedPosition;


            transform.LookAt(target); // ������������ʼ�տ������ǣ�����ȡ����һ�е�ע��
        }
    }
}
