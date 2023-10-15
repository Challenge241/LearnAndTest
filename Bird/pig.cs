using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject pigboom;
    public GameObject pigScore;
    public bool isPig = false;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        //print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > maxSpeed)//����ٶȴ�������ٶȣ�����ֱ������
        {
            pigdie();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed&&collision.relativeVelocity.magnitude>maxSpeed)
        {
            render.sprite = hurt;
        }
        else
        {

        }
    }
    void pigdie()
    {
        if (isPig == true)
        {
            AngryBirdManager._instance.pigs.Remove(this);
        }
        Instantiate(pigboom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(pigScore, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);
        Destroy(gameObject);
        addKillPigNum();
    }
    private void OnTriggerEnter2D(Collider2D collision)//�������
    {


    }
    private void addKillPigNum()
    {
        if (isPig == true)
        {
            killPigNumManager.instance.killPigNum = killPigNumManager.instance.killPigNum + 1;
            killPigNumManager.instance.UpdateText();
            PlayerPrefs.SetInt("killPigNum", killPigNumManager.instance.killPigNum);
        }
    }
}
