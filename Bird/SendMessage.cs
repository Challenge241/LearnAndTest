using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //������Ϣ��gameObject
        gameObject.SendMessage("try1");
        //��������Ϣ���Լ����Լ����ϵ�����MonoBehaviour����
        SendMessage("try4", 1234);//��������ĵڶ���������try4�����Ĳ���
        SendMessage("try5", SendMessageOptions.DontRequireReceiver);//???
        //�㲥��Ϣ�����·��������Ӷ�������Լ���
        BroadcastMessage("try2");
        //���Ϸ�����Ϣ(����������Լ�)
        SendMessageUpwards("try3");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void try4(int x)
    {
        print(x);
    }
    void try1()
    {
        print("try1");
    }
    void try2()
    {
        print("try2");
    }
    void try3()
    {
        print("try3");
    }
}
