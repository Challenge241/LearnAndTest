using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float duration;//�������ʱ��
    void destroySun()
    {
        GameObject.Destroy(gameObject, duration);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, duration);
        //InvokeRepeating("destroySun", duration, duration);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        //�����������������
        GameManager.instance.ChangeSunNum(25);
        //Debug.Log("OnMouseDown");
        //WaitToDo�ɵ�UI��������λ��Ȼ������
        GameObject.Destroy(gameObject);
    }
}
