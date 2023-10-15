// ������Ҫ�������ռ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����һ��������MovedSweet�࣬�̳���Unity3D�Ļ���MonoBehaviour
public class MovedSweet : MonoBehaviour
{
    //����Ʒ�������ǵڼ��еڼ����ƶ���ָ��λ��
    // ����һ��˽�е�GameSweet���͵ı���sweet�����ڴ洢�ǹ�����Ϣ
    private GameSweet sweet;
    private IEnumerator moveCoroutine; // �ƶ���Э��
    // Awake�����ڶ��󱻳�ʼ��ʱ���ã���������sweet������ֵ
    private void Awake()
    {
        // GetComponent<GameSweet>()�Ǵӵ�ǰ���������л�ȡ����ΪGameSweet�����
        // �����ǻ�ȡ��ǰ��Ϸ�����GameSweet���������ֵ��sweet����
        sweet = GetComponent<GameSweet>();
    }

    // ����һ��������Move�����������ƶ��ǹ����µ�λ��
    public void Move(int newX, int newY, float time)
    {

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine); // ���֮ǰ�����ڽ��е��ƶ�Э�̣�ֹͣ��
        }

        moveCoroutine = MoveCoroutine(newX, newY, time); // �����µ��ƶ�Э��
        StartCoroutine(moveCoroutine); // �����ƶ�Э��
    }
    // �ƶ���Ʒ��Э��
    private IEnumerator MoveCoroutine(int newX, int newY, float time)
    {
        //print("move");
        // ���µ�λ��ֵ����sweet��X��Y����
        sweet.X = newX;
        sweet.Y = newY;

        Vector3 startPos = transform.position; // ��ʼλ��
        // ����sweet��llkGameManager��CorrectPositon������ȡ��ȷ��λ�ã�Ȼ�󽫴�λ�ø���sweet�ı���λ��
        // llkGameManager��CorrectPositon�������������ڸ�����Ϸ�������λ�õķ���
        Vector3 endPos = sweet.llkGameManager.CorrectPositon(newX, newY); // Ŀ��λ��

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            sweet.transform.position = Vector3.Lerp(startPos, endPos, t / time); // ��ֵ���㵱ǰλ��
            yield return 0; // �ȴ�һ֡
        }

        sweet.transform.position = endPos; // �ƶ�����������Ʒ����Ŀ��λ��
    }
    // Start��������Ϸ��ʼǰ�ĵ�һ֡����ʱ���ã�����Ϊ�գ���Ҫ��ʵ��ʱ��д����Ĵ���
    void Start()
    {

    }

    // Update������ÿһ֡����ʱ���ã�����Ϊ�գ���Ҫ��ʵ��ʱ��д����Ĵ���
    void Update()
    {

    }
}
