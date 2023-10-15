using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAttack = false;
    //如果是我的话，我会把原朝向赋给temp，用temp与新朝向改朝向或者用左右朝向改，这里用的是Vector3.one
    Vector3 flippedScale = new Vector3(-1,1,1);
    private Rigidbody2D rigi;
    private Animator animator;
    BoxCollider2D boxCollider;
    public float moveX;
    public float moveY;
    public float moveSpeed=10f;
    public float jumpForce =230f;
    public float jumpTimer = 0f;
    int moveChangeAni;//影响移动改变动画的整型数据
    bool isOnGround;
    public bool isJumping = false; //定义一个变量来检测角色是否在跳跃
    private int jumpCount = 0;   // 跳跃次数计数器
    private int maxJump = 2;     // 最大跳跃次数
    public float delaySecondJump = 0.5f;  // 延迟第二次跳跃的时间
    // 这是射线投射到地面的最大距离
    public float groundCheckDistance = 0.1f;
    // 这是一个标记，用来标记哪些层是地面层
    public LayerMask groundLayers;
    // 引用炮弹预设
    public GameObject bulletPrefab;
    // 炮弹发射的速度
    public float bulletSpeed = 10f;
    public GameObject ghostPrefab; // 引用幽灵预设
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Direction();
        //Jump();
        SecondJump();
        Attack();
    }
    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        rigi.velocity = new Vector2(moveX*moveSpeed,rigi.velocity.y);
        if (moveX > 0)
        {
            moveChangeAni = 1;
        }else if(moveX < 0)
        {
            moveChangeAni = -1;
        }
        else
        {
            moveChangeAni = 0;
        }
        animator.SetInteger("movement", moveChangeAni);
    }
    private void Direction()
    {
        //print(moveX);
        if (moveX > 0)
        {
            transform.localScale = flippedScale;
        }else if(moveX<0)
        {
            transform.localScale = Vector3.one; 
        }
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Z) && !isJumping)
        {
            rigi.AddForce(new Vector2(0,jumpForce),ForceMode2D.Force);
            animator.SetTrigger("jump");
            isJumping = true; //设定角色正在跳跃
        }
    }
    private void raydown() // 定义一个名为raydown()的私有方法
    {
        float characterHeight = boxCollider.size.y; // 获取角色的boxCollider组件的高度，并存储到characterHeight变量中

        // 在角色的位置的下方一半的高度（即角色的底部）创建一个新的二维矢量rayOrigin
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - characterHeight / 2);

        // 设定地面检测的距离为角色boxCollider的高度的十分之一，并存储到groundCheckDistance变量中
        groundCheckDistance = boxCollider.size.y / 10.0f;

        // 从rayOrigin位置向下发射一个长度为groundCheckDistance的射线，只与groundLayers图层的物体交互，并将结果存储到hit变量中
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayers);

        // 检查射线是否与某个物体相交，即角色是否在地面上
        if (hit.collider != null) // 如果射线碰到了某个物体（即碰到了地面）
        {
            isJumping = false; // 则将isJumping变量设置为false，表示角色不再跳跃
            jumpCount = 0; // 重置跳跃次数为0
            animator.SetBool("isOnGround", true); // 使用Animator组件的SetBool方法将"isOnGround"参数设置为true，表示角色在地面上
        }
    }

    private void SecondJump()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;  // 当倒计时器大于0时，减少倒计时器的值
        }
        if (Input.GetKey(KeyCode.K) && jumpCount < maxJump && jumpTimer <= 0)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, 0f); // 清除当前的垂直速度
            rigi.AddForce(new Vector2(0f, jumpForce));
            animator.SetTrigger("jump");
            isJumping = true;
            animator.SetBool("isOnGround", false);
            jumpCount++; // 增加跳跃次数
            jumpTimer = delaySecondJump;  // 每次跳跃后，设置倒计时器的值
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        raydown();
    }
    
    private void Attack()
    {
        /*if (isAttack==true)
        {
            return;
        }*/
        if (Input.GetKey(KeyCode.W) &&  Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("UpAttack");
            //isAttack = true;
        }else if(Input.GetKey(KeyCode.S) &&  Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("DownAttack");
            //isAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
            //isAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            
            animator.SetTrigger("KnightSkill");
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            transform.Find("Klight").gameObject.SetActive(true);
            animator.SetTrigger("KnightSkill");

        }else if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger("KnightSkill");
            Instantiate(ghostPrefab, transform.position, Quaternion.identity);

        }

    }
    void Fire()
    {
        // 创建一个炮弹的实例
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.localScale = transform.localScale;
        // 给炮弹添加一个速度使其向前飞行
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-transform.localScale.x,0,0) * bulletSpeed;
    }
    /*
    private void AttackEnd()
    {
        isAttack = false;
    }*/
}
