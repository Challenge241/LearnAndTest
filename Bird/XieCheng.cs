using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XieCheng : MonoBehaviour
{
    public Transform pos;
    public GameObject zombie;
    public int x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        //Э�̵�����
        //StartCoroutine("ChangeState");
        //StartCoroutine(ChangeState());
        //IEnumerator ie = ChangeState();
        //StartCoroutine(ie);

        //Э�̵�ֹͣ
        //StopCoroutine("ChangeState");//ֹͣ������ΪChangeState��Э��

        //��StopCoroutine(ChangeState())�޷�ֹͣЭ��

        //StopCoroutine(ie);//ֹͣ����ie�������Э��
        //StopAllCoroutines();//ֹͣ����Э��
        StartCoroutine(CreateZombie(x));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CreateZombie(int x)
    {
        for(int i = 1; i <= x; i++)
        {
            yield return new WaitForSeconds(2);//ͣ2��
            yield return new WaitForEndOfFrame();//�ȴ���֡���������磺��֡����һ�룬ִ�д�����ȵ���֡������ִ����һ�����
            yield return new WaitForSeconds(y);//ͣy��
            yield return null;//ͣ1֡
            Instantiate(zombie, pos.position, Quaternion.identity);
        }
    }
}
