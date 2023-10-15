using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isClick = false;
   
    public float maxDis;
    [HideInInspector]//使公有变量sp不在监视面板显示
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    protected TrailRenderer tr;
    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;
    protected bool isfly;
    protected bool isuse;//是否使用技能

    public float smooth = 3;

    public GameObject birdboom;
    // Start is called before the first frame update
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg= GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineControl();
    }
    protected void LineControl()//限制橡皮筋的长度
    {
        if (isClick == true)
        {
            //鼠标用的坐标是屏幕坐标，小鸟用的是世界坐标
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);//令小鸟的z轴坐标加10
            transform.position -= new Vector3(0, 0, -Camera.main.transform.position.z);//令小鸟的z轴坐标减摄像机z轴坐标
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位向量化,得到单位向量
                pos *= maxDis;//给向量赋长度
                transform.position = pos + rightPos.position;
            }
            Line();
        }
        //相机跟随//可为什么相机不会迷茫该跟哪个小鸟，为什么拉橡皮筋时不跟随，松开后会跟随，真奇怪
        float posX = transform.position.x;//将当前小鸟的横坐标赋给PosX
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y,
            Camera.main.transform.position.z), smooth * Time.deltaTime);//相机坐标的y轴、z轴不变，x轴变
    }
    protected void OnMouseDown()
    {
        if (isfly == true)
        {
            return;
        }
        isClick = true;
        left.enabled = true;
        right.enabled = true;
        //rg.isKinematic = true;
    }
    protected void OnMouseUp()
    {
        if (isfly == true)
        {
            return;
        }
        isClick = false;
        sp.enabled = false;
        Invoke("Next", 5);
        left.enabled = false;
        right.enabled = false;
        tr.enabled = true;
        isfly = true;
        //rg.isKinematic = false;
        //Invoke("Fly", 0.5f);
    }
    protected void OnCollisionEnter2D(Collision2D collision)//碰撞检测
    {
        if (tr.enabled == true)
        { tr.enabled = false; }
    }
    /*void Fly()
    {
        sp.enabled = false;
    }*/

    protected void Line()//画线
    {
        right.SetPosition(0, rightPos.position);//0代表画线的起点
        right.SetPosition(1, transform.position);//1代表画线的下一点，2点确立一条直线

        left.SetPosition(0, leftPos.position);//0代表画线的起点
        left.SetPosition(1, transform.position);//1代表画线的下一点，2点确立一条直线
    }
    protected void Next()
    {
        AngryBirdManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(birdboom, transform.position, Quaternion.identity);
        AngryBirdManager._instance.NextBird();
    }
    protected virtual void skill()
    {
        
    }
}
