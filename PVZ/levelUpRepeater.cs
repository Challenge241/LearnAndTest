using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpRepeater : MonoBehaviour
{
    //��������������⣬ʵ����������ڸ�����������ֻ���㶹��������Ϊ˫������
    //��Ϊ��һ��ʹ�øýű��������㶹��������Ϊ˫�����֣���ʱû�뵽�ýű���������˸�
    private GameObject f;
    private GameObject p;
    private GameObject LevelUpPlant;
    public GameObject LevelUpPlantPrefab;//�������ֲ��
    // Start is called before the first frame update
    void Start()
    {
        f = transform.parent.gameObject;//�õ��������㶹����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
            //�õ�����Ԥ�Ƽ�
            LevelUpPlant = Instantiate(LevelUpPlantPrefab);
            //�õ��㶹���ֵĸ���������
            p = f.transform.parent.gameObject;
            //�����ظ���������ֲ��ĸ�����
            LevelUpPlant.transform.parent = p.transform;
            LevelUpPlant.transform.localPosition = Vector3.zero;
            //�����㶹���֣����������ص�
            Destroy(f.gameObject);
    }
}
