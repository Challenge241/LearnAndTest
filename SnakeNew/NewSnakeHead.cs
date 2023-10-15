using System.Collections.Generic;
using UnityEngine;

public class NewSnakeHead : MonoBehaviour
{
    // ������ͷ�ٶ��뷽��
    private Vector2 startdirection = Vector2.right;//��ʼ����
    public int initBodyNum = 7;
    public GameObject bodyPrefab;
    public List<GameObject> bodies = new List<GameObject>();
    Rigidbody2D rb;
    //Ӱ��ǰ�����˵���
    public float forceMagnitude=3f;
    //����ı䷽�����
    public float steeringForce = 5f;
    bool isStartDirect = true;
    private Vector2 saveDirection;
    public float initFrequency=5.0f;
    public float initdampingRatio=0.5f;
    void Start()
    {
        //AddSpringToSnakeBody();
        //Debug.LogWarning("transform.forward" + transform.forward);
        // ��ȡ�������
        rb = GetComponent<Rigidbody2D>();
        // ��ʼ�������λ��
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
        // ������һ���ֵĸ������á���ʼʱ��������ͷ�ĸ��塣
        Rigidbody2D previousPartRb = transform.GetComponent<Rigidbody2D>();

        for (int i = 0; i < bodies.Count; i++)
        {
            // ��ȡ��ǰ���ֵĸ��������
            Rigidbody2D currentPartRb = bodies[i].GetComponent<Rigidbody2D>();

            // �ڵ�ǰ���������һ�� SpringJoint2D ��������������á�
            SpringJoint2D sj = bodies[i].AddComponent<SpringJoint2D>();

            // ���õ��ɵ� connectedBody Ϊ��һ���ֵĸ��塣
            sj.connectedBody = previousPartRb;

            // ���õ��ɵ��������ԡ���Щֵ������Ҫ�����Ի����ѵ���ϷЧ����
            sj.frequency = initFrequency; // ���ɳ���
            sj.dampingRatio =initdampingRatio; // �����

            // ������һ���ֵĸ������ã�Ϊ��һ��ѭ����׼����
            previousPartRb = currentPartRb;
        }
    }
        private void movement()
    {
        // ��ÿ������Ԫ�ظ�����ǰһ��Ԫ�أ�����ͷ��
        for (int i = 0; i < bodies.Count; i++)
        {
            Vector3 targetPosition;
            if (i == 0)
            {
                // ��һ������Ԫ�ظ�����ͷ
                targetPosition = transform.position;
            }
            else
            {
                // ��������Ԫ�ظ�����ǰһ������Ԫ��
                targetPosition = bodies[i - 1].transform.position;
            }
            // ������Ԫ���ƶ���Ŀ��λ�ã������ֺ㶨�ľ���
            bodies[i].transform.position = Vector3.MoveTowards(bodies[i].transform.position, targetPosition, rb.velocity.magnitude * Time.fixedDeltaTime);
        }
    }
    private void ChangeRoate()
    {
        if (rb.velocity != Vector2.zero) // ����ٶȲ�Ϊ�㣨���������ƶ���
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; // ����Ŀ��Ƕ�
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // ���������ת����ΪĿ��Ƕ�
            print("transform.forward" + transform.up); // ��2D�У�transform.up�ȼ���3D�е�transform.forward
        }
    }

    private void HandleInput()
    {
        // ��ȡ�ٶ�����
        Vector2 velocity = rb.velocity;
        //print("velocity" + velocity);//(0,-1)
        // ���㲢��ȡ�ٶ������ķ�������
        Vector2 velocityDirection = velocity.normalized;
        //print("vdirection" + velocityDirection);//(0,-1)
        // ���㴹ֱ���ٶ���������������
        Vector2 perpendicular1 = new Vector2(-velocityDirection.y, velocityDirection.x);
        Vector2 perpendicular2 = new Vector2(velocityDirection.y, -velocityDirection.x);
        //print("vdirection" + velocityDirection);
        // �� A ������ʱ��ת��
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(perpendicular1 * steeringForce);
            saveDirection = velocityDirection;
            //print("perpendicular1" + perpendicular1);
            ChangeRoate();
        }
        // �� D ����˳ʱ��ת��
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(perpendicular2 * steeringForce);
            saveDirection = velocityDirection;
            //print("perpendicular2" + perpendicular2);
            ChangeRoate();
        }

        // �� W ��������һ����ǰ�������
        if (Input.GetKey(KeyCode.W))
        {
            if (velocity.magnitude != 0)
            {//�ٶ�Ϊ0ʱ���ٶȵķ�������ҲΪ0���ᵼ����Ϊ0
                Vector2 force = velocityDirection * forceMagnitude;
                saveDirection=velocityDirection;
                //print("force"+ force);
                rb.AddForce(force);
            }
            else if(velocity.magnitude == 0&&isStartDirect==true)
            {//direction��ֹ���ʼʱ�ķ�������
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
        // �� S ��������һ���뵱ǰ�����෴����
        else if (Input.GetKey(KeyCode.S))
        {
            Vector2 force = -velocityDirection * forceMagnitude;
            rb.AddForce(force);
        }
    }
}
    