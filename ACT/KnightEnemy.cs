using UnityEngine;

public class KnightEnemy : MonoBehaviour
{
    public float health=100; // ���˵�����ֵ
    public float speed=1; // ���˵��ƶ��ٶ�
    public float turnInterval=10; // ���˸ı䷽���ʱ����
    private bool isAttacked; // ��־λ���������Ƿ񱻹�����
    private Transform player; // ������ҵ�Transform���
    private Rigidbody2D rb; // ���õ��˵�Rigidbody2D���
    private float turnTimer; // ��ʱ�������ڼ�����˸ı䷽���ʱ��
    private int direction; // �����ƶ��ķ���

    // �ڵ�һ֡����֮ǰ����Start����
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ��ȡRigidbody2D���
        player = GameObject.FindGameObjectWithTag("Player").transform; // �ҵ���ǩΪPlayer����Ϸ�����Transform���
        isAttacked = false; // ��ʼ�趨Ϊδ��������
        turnTimer = turnInterval; // ��ʼ����ʱ��
        direction = Random.Range(0, 2) * 2 - 1; // ��ʼ������������������
    }

    // ÿ֡����Update����
    void Update()
    {
        if (isAttacked)
        {
            // ������˱���������������ƶ�
            float AttackedDirection = (player.position - transform.position).normalized.magnitude;
            if (AttackedDirection > 0) direction = 1;
            else direction = -1;
            FlipDirection(direction);
            rb.velocity = new Vector2(direction, 0) * speed;
        }
        else
        {
            // ���δ�����������յ�ǰ�����ƶ�
            rb.velocity = new Vector2(direction, 0) * speed;
            FlipDirection(direction);
            // ��ʱ���ݼ�
            turnTimer -= Time.deltaTime;

            // ����ʱ��С��0ʱ�����ü�ʱ�����ı䷽��
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
        // ���ٵ��˵�����ֵ
        health -= damage;

        // �������Ƿ��Ѿ�����
        if (health <= 0)
        {
            // ��������Ѿ����������ٵ��˵���Ϸ����
            Destroy(gameObject);
        }
        else
        {
            // ��������ܵ��˺��������ţ���ʼ��������ƶ�
            isAttacked = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������ϰ���ı䷽��
        if (collision.gameObject.tag != "Player")
        {
            direction *= -1;
        }
    }
    void FlipDirection(int direction)
    {
        // ��ȡ��ǰ���������
        Vector3 theScale = transform.localScale;
        //print(direction);
        // ��X���Ϸ�ת����
        theScale.x = direction;

        // Ӧ���µ�����
        transform.localScale = theScale;
    }
}

