using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public List<Transform> bodyListX = new List<Transform>();    //???
    public float velocity = 0.35f;
    public int step;
    private int x;
    private int y;
    private Vector3 headPos;
    private Transform canvas;
    private bool isDie = false;
    public GameObject gameover;
    //public AudioClip eatClip;
    //public AudioClip dieClip;
    //public GameObject dieEffect;
    public GameObject bodyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0; y = step;
    }

    // Update is called once per frame
    void Update()
    {
        snakegetkey();
    }
    void snakegetkey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W) && y != -step && isDie == false)
        {
            //gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0; y = step;
        }
        if (Input.GetKey(KeyCode.S) && y != step && isDie == false)
        {
            //gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0; y = -step;
        }
        if (Input.GetKey(KeyCode.A) && x != step && isDie == false)
        {
            //gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step; y = 0;
        }
        if (Input.GetKey(KeyCode.D) && x != -step && isDie == false)
        {
            //gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step; y = 0;
        }
    }
    void Move()
    {
        headPos = gameObject.transform.localPosition;                                               //����������ͷ�ƶ�ǰ��λ��
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);  //��ͷ������λ���ƶ�
        if (bodyListX.Count > 0)
        {
            //��һ
            //bodyList.Last().localPosition = headPos;                                              //����β�ƶ�����ͷ�ƶ�ǰ��λ��
            //bodyList.Insert(0, bodyList.Last());                                                  //����β��List�е�λ�ø��µ���ǰ
            //bodyList.RemoveAt(bodyList.Count - 1);                                                //�Ƴ�List��ĩβ����β����

            //����
            for (int i = bodyListX.Count - 2; i >= 0; i--)                                           //�Ӻ���ǰ��ʼ�ƶ�����
            {
                bodyListX[i + 1].localPosition = bodyListX[i].localPosition;                   //ÿһ�������ƶ�����ǰ��һ���ڵ��λ��
            }
            bodyListX[0].localPosition = headPos;                                                    //��һ�������ƶ�����ͷ�ƶ�ǰ��λ��
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeBody")
        {
            Die();
        }
        if (collision.tag == "Barriar")
        {
            Die();
        }
    }
    void Grow()
    {
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000, 2000, 0), Quaternion.identity);
        bodyListX.Add(body.transform);
    }
    void Die()
    {
        CancelInvoke();
        isDie = true;
        gameover.SetActive(true);
    }
}
