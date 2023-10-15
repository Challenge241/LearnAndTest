using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isClick = false;
   
    public float maxDis;
    [HideInInspector]//ʹ���б���sp���ڼ��������ʾ
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    protected TrailRenderer tr;
    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;
    protected bool isfly;
    protected bool isuse;//�Ƿ�ʹ�ü���

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
    protected void LineControl()//������Ƥ��ĳ���
    {
        if (isClick == true)
        {
            //����õ���������Ļ���꣬С���õ�����������
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);//��С���z�������10
            transform.position -= new Vector3(0, 0, -Camera.main.transform.position.z);//��С���z������������z������
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//��λ������,�õ���λ����
                pos *= maxDis;//������������
                transform.position = pos + rightPos.position;
            }
            Line();
        }
        //�������//��Ϊʲô���������ã�ø��ĸ�С��Ϊʲô����Ƥ��ʱ�����棬�ɿ������棬�����
        float posX = transform.position.x;//����ǰС��ĺ����긳��PosX
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y,
            Camera.main.transform.position.z), smooth * Time.deltaTime);//��������y�ᡢz�᲻�䣬x���
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
    protected void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        if (tr.enabled == true)
        { tr.enabled = false; }
    }
    /*void Fly()
    {
        sp.enabled = false;
    }*/

    protected void Line()//����
    {
        right.SetPosition(0, rightPos.position);//0�����ߵ����
        right.SetPosition(1, transform.position);//1�����ߵ���һ�㣬2��ȷ��һ��ֱ��

        left.SetPosition(0, leftPos.position);//0�����ߵ����
        left.SetPosition(1, transform.position);//1�����ߵ���һ�㣬2��ȷ��һ��ֱ��
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
