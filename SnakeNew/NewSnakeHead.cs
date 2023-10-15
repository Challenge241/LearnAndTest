using System.Collections.Generic;
using UnityEngine;

public class NewSnakeHead : MonoBehaviour
{
    // 设置蛇头速度与方向
    private Vector2 startdirection = Vector2.right;//初始方向
    public int initBodyNum = 7;
    public GameObject bodyPrefab;
    public List<GameObject> bodies = new List<GameObject>();
    Rigidbody2D rb;
    //影响前进后退的力
    public float forceMagnitude=3f;
    //负责改变方向的力
    public float steeringForce = 5f;
    bool isStartDirect = true;
    private Vector2 saveDirection;
    public float initFrequency=5.0f;
    public float initdampingRatio=0.5f;
    void Start()
    {
        //AddSpringToSnakeBody();
        //Debug.LogWarning("transform.forward" + transform.forward);
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        // 初始化蛇身的位置
        /*for (int i = 0; i < initBodyNum; i++)
        {
            GameObject body = bodies[i];
            //var body = Instantiate(bodyPrefab, transform.position - i * direction, Quaternion.identity);
            //bodies.Add(body);
        }*/
    }

    void FixedUpdate()
    {  
        HandleInput();
        movement();
    }
    private void AddSpringToSnakeBody()
    {
        // 保存上一部分的刚体引用。初始时，这是蛇头的刚体。
        Rigidbody2D previousPartRb = transform.GetComponent<Rigidbody2D>();

        for (int i = 0; i < bodies.Count; i++)
        {
            // 获取当前部分的刚体组件。
            Rigidbody2D currentPartRb = bodies[i].GetComponent<Rigidbody2D>();

            // 在当前部分上添加一个 SpringJoint2D 组件，并保存引用。
            SpringJoint2D sj = bodies[i].AddComponent<SpringJoint2D>();

            // 设置弹簧的 connectedBody 为上一部分的刚体。
            sj.connectedBody = previousPartRb;

            // 设置弹簧的其他属性。这些值可能需要调整以获得最佳的游戏效果。
            sj.frequency = initFrequency; // 弹簧常数
            sj.dampingRatio =initdampingRatio; // 阻尼比

            // 更新上一部分的刚体引用，为下一次循环做准备。
            previousPartRb = currentPartRb;
        }
    }
        private void movement()
    {
        // 让每个蛇身元素跟随其前一个元素（或蛇头）
        for (int i = 0; i < bodies.Count; i++)
        {
            Vector3 targetPosition;
            if (i == 0)
            {
                // 第一个蛇身元素跟随蛇头
                targetPosition = transform.position;
            }
            else
            {
                // 其他蛇身元素跟随其前一个蛇身元素
                targetPosition = bodies[i - 1].transform.position;
            }
            // 让蛇身元素移动到目标位置，但保持恒定的距离
            bodies[i].transform.position = Vector3.MoveTowards(bodies[i].transform.position, targetPosition, rb.velocity.magnitude * Time.fixedDeltaTime);
        }
    }
    private void ChangeRoate()
    {
        if (rb.velocity != Vector2.zero) // 如果速度不为零（物体正在移动）
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; // 计算目标角度
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // 把物体的旋转设置为目标角度
            print("transform.forward" + transform.up); // 在2D中，transform.up等价于3D中的transform.forward
        }
    }

    private void HandleInput()
    {
        // 获取速度向量
        Vector2 velocity = rb.velocity;
        //print("velocity" + velocity);//(0,-1)
        // 计算并获取速度向量的方向向量
        Vector2 velocityDirection = velocity.normalized;
        //print("vdirection" + velocityDirection);//(0,-1)
        // 计算垂直于速度向量的两个方向
        Vector2 perpendicular1 = new Vector2(-velocityDirection.y, velocityDirection.x);
        Vector2 perpendicular2 = new Vector2(velocityDirection.y, -velocityDirection.x);
        //print("vdirection" + velocityDirection);
        // 按 A 键，逆时针转向
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(perpendicular1 * steeringForce);
            saveDirection = velocityDirection;
            //print("perpendicular1" + perpendicular1);
            ChangeRoate();
        }
        // 按 D 键，顺时针转向
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(perpendicular2 * steeringForce);
            saveDirection = velocityDirection;
            //print("perpendicular2" + perpendicular2);
            ChangeRoate();
        }

        // 按 W 键，给蛇一个当前方向的力
        if (Input.GetKey(KeyCode.W))
        {
            if (velocity.magnitude != 0)
            {//速度为0时，速度的方向向量也为0，会导致力为0
                Vector2 force = velocityDirection * forceMagnitude;
                saveDirection=velocityDirection;
                //print("force"+ force);
                rb.AddForce(force);
            }
            else if(velocity.magnitude == 0&&isStartDirect==true)
            {//direction静止或初始时的方向向量
                Vector2 force = startdirection * forceMagnitude;
                rb.AddForce(force);
                isStartDirect = false;
            }
            else
            {
                //Vector2 force = transform.forward * forceMagnitude;
                Vector2 force = forceMagnitude * saveDirection;
                rb.AddForce(force);
            }
        }
        // 按 S 键，给蛇一个与当前方向相反的力
        else if (Input.GetKey(KeyCode.S))
        {
            Vector2 force = -velocityDirection * forceMagnitude;
            rb.AddForce(force);
        }
    }
}
    