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
        headPos = gameObject.transform.localPosition;                                               //保存下来蛇头移动前的位置
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);  //蛇头向期望位置移动
        if (bodyListX.Count > 0)
        {
            //法一
            //bodyList.Last().localPosition = headPos;                                              //将蛇尾移动到蛇头移动前的位置
            //bodyList.Insert(0, bodyList.Last());                                                  //将蛇尾在List中的位置更新到最前
            //bodyList.RemoveAt(bodyList.Count - 1);                                                //移除List最末尾的蛇尾引用

            //法二
            for (int i = bodyListX.Count - 2; i >= 0; i--)                                           //从后往前开始移动蛇身
            {
                bodyListX[i + 1].localPosition = bodyListX[i].localPosition;                   //每一个蛇身都移动到它前面一个节点的位置
            }
            bodyListX[0].localPosition = headPos;                                                    //第一个蛇身移动到蛇头移动前的位置
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
