using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int sunNum = 50;
   // public int coinNum = 0;
    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float createZombieTi;
   // private int zOrderIndex = 0;//��ʬ��ͼ������
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //CreateZombie();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSunNum(int changeNum)
    {
        //Debug.Log("ChangeSunNum");
        sunNum += changeNum;
        //Debug.Log(sunNum);
        if (sunNum <= 0)
        {
            sunNum = 0;
        }
        //wait to do ������ֵ�ı䣬֪ͨ��Ƭѹ�ڵ�
        UIManager.instance.UpdateUI();
    }
    /*
    //Э�̷����ɽ�ʬ
    public void CreateZombie()
    {
        StartCoroutine(DalayCreateZombie());
    }
    IEnumerator DalayCreateZombie()//Э�̺����ص㣬�䷵��ֵҪΪIEnumerator
    {//������Э�̣����컹�Ǵ����ü�ʱ��������ʬ����ϵͳ
        //�ȴ�
        yield return new WaitForSeconds(createZombieTi);//��ο���Э�̵ĵȴ�ʱ�䣿
        //����
        GameObject zombie = Instantiate(zombiePrefab);
        int index=Random.Range(1, 6);
        Transform zombieLine = bornParent.transform.Find("born" + index.ToString());//�����ˣ��ⷽ���
        zombie.transform.parent = zombieLine;
        zombie.transform.localPosition = Vector3.zero;
        //ʹ������ֵĽ�ʬ��ʾ���ϲ�
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex;
        zOrderIndex += 1;
        //�ٴ�������ʱ��
        StartCoroutine(DalayCreateZombie());
    }*/
}