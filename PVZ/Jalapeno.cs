using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalapeno : Plant
{
    private Animator animator;
    public GameObject Jattackprefab;
    public Transform JPos;//—Ùπ‚Œª÷√
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
    }
    public void AniOver()
    {
        Instantiate(Jattackprefab, JPos.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
