using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //发送消息给gameObject
        gameObject.SendMessage("try1");
        //仅发送消息给自己（以及身上的其他MonoBehaviour对象）
        SendMessage("try4", 1234);//这个函数的第二个参数是try4函数的参数
        SendMessage("try5", SendMessageOptions.DontRequireReceiver);//???
        //广播消息（向下发，所有子对象包括自己）
        BroadcastMessage("try2");
        //向上发送消息(父对象包含自己)
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
