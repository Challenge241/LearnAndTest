using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAttack = false;
    //������ҵĻ����һ��ԭ���򸳸�temp����temp���³���ĳ�����������ҳ���ģ������õ���Vector3.one
    Vector3 flippedScale = new Vector3(-1,1,1);
    private Rigidbody2D rigi;
    private Animator animator;
    BoxCollider2D boxCollider;
    public float moveX;
    public float moveY;
    public float moveSpeed=10f;
    public float jumpForce =230f;
    public float jumpTimer = 0f;
    int moveChangeAni;//Ӱ���ƶ��ı䶯������������
    bool isOnGround;
    public bool isJumping = false; //����һ������������ɫ�Ƿ�����Ծ
    private int jumpCount = 0;   // ��Ծ����������
    private int maxJump = 2;     // �����Ծ����
    public float delaySecondJump = 0.5f;  // �ӳٵڶ�����Ծ��ʱ��
    // ��������Ͷ�䵽�����������
    public float groundCheckDistance = 0.1f;
    // ����һ����ǣ����������Щ���ǵ����
    public LayerMask groundLayers;
    // �����ڵ�Ԥ��
    public GameObject bulletPrefab;
    // �ڵ�������ٶ�
    public float bulletSpeed = 10f;
    public GameObject ghostPrefab; // ��������Ԥ��
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
            isJumping = true; //�趨��ɫ������Ծ
        }
    }
    private void raydown() // ����һ����Ϊraydown()��˽�з���
    {
        float characterHeight = boxCollider.size.y; // ��ȡ��ɫ��boxCollider����ĸ߶ȣ����洢��characterHeight������

        // �ڽ�ɫ��λ�õ��·�һ��ĸ߶ȣ�����ɫ�ĵײ�������һ���µĶ�άʸ��rayOrigin
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - characterHeight / 2);

        // �趨������ľ���Ϊ��ɫboxCollider�ĸ߶ȵ�ʮ��֮һ�����洢��groundCheckDistance������
        groundCheckDistance = boxCollider.size.y / 10.0f;

        // ��rayOriginλ�����·���һ������ΪgroundCheckDistance�����ߣ�ֻ��groundLayersͼ������彻������������洢��hit������
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayers);

        // ��������Ƿ���ĳ�������ཻ������ɫ�Ƿ��ڵ�����
        if (hit.collider != null) // �������������ĳ�����壨�������˵��棩
        {
            isJumping = false; // ��isJumping��������Ϊfalse����ʾ��ɫ������Ծ
            jumpCount = 0; // ������Ծ����Ϊ0
            animator.SetBool("isOnGround", true); // ʹ��Animator�����SetBool������"isOnGround"��������Ϊtrue����ʾ��ɫ�ڵ�����
        }
    }

    private void SecondJump()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;  // ������ʱ������0ʱ�����ٵ���ʱ����ֵ
        }
        if (Input.GetKey(KeyCode.K) && jumpCount < maxJump && jumpTimer <= 0)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, 0f); // �����ǰ�Ĵ�ֱ�ٶ�
            rigi.AddForce(new Vector2(0f, jumpForce));
            animator.SetTrigger("jump");
            isJumping = true;
            animator.SetBool("isOnGround", false);
            jumpCount++; // ������Ծ����
            jumpTimer = delaySecondJump;  // ÿ����Ծ�����õ���ʱ����ֵ
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
        // ����һ���ڵ���ʵ��
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.localScale = transform.localScale;
        // ���ڵ����һ���ٶ�ʹ����ǰ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-transform.localScale.x,0,0) * bulletSpeed;
    }
    /*
    private void AttackEnd()
    {
        isAttack = false;
    }*/
}
