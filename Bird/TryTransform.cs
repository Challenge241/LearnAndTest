using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryTransform : MonoBehaviour
{
    public GameObject grisGo;
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        //�����е�ÿ��������һ���任����� �����ڴ洢�Ͳ��������λ�á���ת�����š�
        //1.�������ȡ
        Debug.Log(this.transform);
        Debug.Log(grisGo.transform);
        Transform grisTrans = grisGo.transform;
        //2.��Ա����
        Debug.Log("Gris�任��������ص���Ϸ���������ǣ�" + grisTrans.name);
        Debug.Log("Gris�任��������ص���Ϸ���������ǣ�" + grisTrans.gameObject);
        Debug.Log("Gris�µ��Ӷ���ָTransform���ĸ����ǣ�" + grisTrans.childCount);
        Debug.Log("Gris����ռ��е�����λ���ǣ�" + grisTrans.position);
        Debug.Log("Gris����Ԫ����ʽ��ʾ����ת�ǣ�" + grisTrans.rotation);
        Debug.Log("Gris��ŷ������ʽ��ʾ����ת���Զ���Ϊ��λ����" + grisTrans.eulerAngles);
        Debug.Log("Gris�ĸ���Transform�ǣ�" + grisTrans.parent);
        Debug.Log("Gris����ڸ������λ�������ǣ�" + grisTrans.localPosition);
        Debug.Log("Gris����ڸ���������Ԫ����ʽ��ʾ����ת�ǣ�" + grisTrans.localRotation);
        Debug.Log("Gris����ڸ�������ŷ������ʽ��ʾ����ת���Զ���Ϊ��λ���ǣ�" + grisTrans.localEulerAngles);
        Debug.Log("Gris����ڸ�����ı任�����ǣ�" + grisTrans.localScale);
        Debug.Log("Gris������������ǰ����Z���������ǣ�" + grisTrans.forward);
        Debug.Log("Gris�������������ҷ���X���������ǣ�" + grisTrans.right);
        Debug.Log("Gris�������������Ϸ���Y���������ǣ�" + grisTrans.up);
        //���з���
        //3.����
        Debug.Log("��ǰ�ű����ص���Ϸ�����µĽ�Gris���Ӷ������ϵ�Transform����ǣ�" + transform.Find("Gris"));
        Debug.Log("��ǰ�ű����ص���Ϸ�����µĵ�һ����0���������Ӷ����Transform�����ǣ�" + transform.GetChild(0));
        Debug.Log("Gris��ǰ�ڴ˸�����ͬ�������ڵ�����λ�ã�" + grisTrans.GetSiblingIndex());
        //��̬����
        //Transform.Destroy(grisTrans);
        //Transform.Destroy(grisTrans.gameObject);
        //Transform.FindObjectOfType();
        //Transform.Instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        //�ƶ�
        //0.�ڶ����������ʵ���������������ϵ�ƶ�,space.self��
        //grisGo.transform.Translate(Vector2.left*moveSpeed);//��������ϵ
        //grisGo.transform.Translate(-grisGo.transform.right*moveSpeed);//��������ϵ
        //1.��һ����������������ϵ�ƶ����ڶ�������ָ����������ϵ��ʵ���������������ϵ�ƶ���
        //grisGo.transform.Translate(Vector2.left*moveSpeed,Space.World);
        //2.��һ����������������ϵ�ƶ����ڶ�������ָ����������ϵ��ʵ���������������ϵ�ƶ���
        //grisGo.transform.Translate(Vector2.left * moveSpeed, Space.Self);
        //3.��һ����������������ϵ�ƶ����ڶ�������ָ����������ϵ��ʵ���������������ϵ�ƶ���
        //grisGo.transform.Translate(-grisGo.transform.right * moveSpeed, Space.World);
        //4.��һ����������������ϵ�ƶ����ڶ�������ָ����������ϵ��ʵ���������������ϵ�ƶ���(һ�㲻ʹ��)
        //grisGo.transform.Translate(-grisGo.transform.right * moveSpeed, Space.Self);
        //��ת
        //grisGo.transform.Rotate(new Vector3(0,0,1));
        grisGo.transform.Rotate(Vector3.forward, 1);
    }
}
