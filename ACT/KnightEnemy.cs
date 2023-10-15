using UnityEngine;

public class KnightEnemy : MonoBehaviour
{
    public float health=100; // 敌人的生命值
    public float speed=1; // 敌人的移动速度
    public float turnInterval=10; // 敌人改变方向的时间间隔
    private bool isAttacked; // 标志位，检查敌人是否被攻击过
    private Transform player; // 引用玩家的Transform组件
    private Rigidbody2D rb; // 引用敌人的Rigidbody2D组件
    private float turnTimer; // 计时器，用于计算敌人改变方向的时间
    private int direction; // 敌人移动的方向

    // 在第一帧更新之前调用Start方法
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取Rigidbody2D组件
        player = GameObject.FindGameObjectWithTag("Player").transform; // 找到标签为Player的游戏对象的Transform组件
        isAttacked = false; // 初始设定为未被攻击过
        turnTimer = turnInterval; // 初始化计时器
        direction = Random.Range(0, 2) * 2 - 1; // 初始化方向，随机向左或向右
    }

    // 每帧调用Update方法
    void Update()
    {
        if (isAttacked)
        {
            // 如果敌人被攻击，朝向玩家移动
            float AttackedDirection = (player.position - transform.position).normalized.magnitude;
            if (AttackedDirection > 0) direction = 1;
            else direction = -1;
            FlipDirection(direction);
            rb.velocity = new Vector2(direction, 0) * speed;
        }
        else
        {
            // 如果未被攻击，按照当前方向移动
            rb.velocity = new Vector2(direction, 0) * speed;
            FlipDirection(direction);
            // 计时器递减
            turnTimer -= Time.deltaTime;

            // 当计时器小于0时，重置计时器并改变方向
            if (turnTimer < 0)
            {
                direction *= -1;
                FlipDirection(direction);
                turnTimer = turnInterval;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        // 减少敌人的生命值
        health -= damage;

        // 检查敌人是否已经死亡
        if (health <= 0)
        {
            // 如果敌人已经死亡，销毁敌人的游戏对象
            Destroy(gameObject);
        }
        else
        {
            // 如果敌人受到伤害但还活着，开始朝向玩家移动
            isAttacked = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰到障碍物，改变方向
        if (collision.gameObject.tag != "Player")
        {
            direction *= -1;
        }
    }
    void FlipDirection(int direction)
    {
        // 获取当前物体的缩放
        Vector3 theScale = transform.localScale;
        //print(direction);
        // 在X轴上反转缩放
        theScale.x = direction;

        // 应用新的缩放
        transform.localScale = theScale;
    }
}

