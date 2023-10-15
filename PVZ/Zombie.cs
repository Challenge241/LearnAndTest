using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1, 0, 0);
    public float speed = 1;
    public float damage;
    public float damageInterval = 0.5f;
    public float damageTimer;
    public float health = 100;
    public float currentHealth;
    protected bool isWalk;
    protected bool isDie;
    protected bool isBoom;
    protected Animator animator;
    public float def=0;
    public int coin=10;

    public float weightgold = 30;
    public float weightdiamond = 40;
    public float weightnothing = 30;
    public GameObject gold;
    public GameObject diamond;
    public Transform diaoluoPos;
    public void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Move()
    {
        if (isWalk)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
   

    public void OnDestroy()
    {
        float a = Random.Range(0f, weightgold + weightdiamond + weightnothing);
        if (a <= weightgold)
        {
            //Debug.LogWarning("gold");
            Instantiate(gold,diaoluoPos.position, Quaternion.identity);
            //如果只写Instantiate(gold)会出严重问题
            /*其会说UnassignedReferenceException: The variable gold of ZombieNormal has not been assigned.
            You probably need to assign the gold variable of the ZombieNormal script in the inspector*/
        }
        else if (a <= weightgold + weightdiamond)
        {
            //Debug.LogWarning("diamond");
            Instantiate(diamond,diaoluoPos.position, Quaternion.identity); ;
        }
        else { }
    }
}
