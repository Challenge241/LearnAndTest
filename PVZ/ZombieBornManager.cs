using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBornManager : MonoBehaviour
{
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;
    public Transform Pos5;
    public GameObject Zombie1;
    public GameObject Zombie2;
    public GameObject Zombie3;
    public GameObject Zombie4;
    public GameObject Zombie5;
    public GameObject Zombie6;
    public GameObject Zombie7;
    public GameObject Zombie8;
    public GameObject Zombie9;
    public GameObject Zombie10;
    public GameObject Zombie11;
    public GameObject Zombie12;
    public GameObject Zombie13;
    public GameObject Zombie14;
    public GameObject temp = null;
    public float timer1=0;
    public float timer2=0;
    public float timer3=0;
    public float timer4=0;
    public float timer5=0;
    public float intervalLow=6;
    public float intervalMiddle=15;
    public float intervalGreat=60;
    public float intervalWave=150;
    public float d1=0;//时间间隔变化量
    public float d2=1;
    public float d3=2;
    public int wavenum=1;
    public float fistzombiecometime = 4;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("zombieBegin", fistzombiecometime);
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 += Time.deltaTime;
        timer4+= Time.deltaTime;
        timer5 += Time.deltaTime;
        pingshi();
        waveZombie();
    }
    public void pingshi()
    {
        if (timer1 >= intervalLow)
        {
            bornZombie("Low", 1);
            intervalLow = intervalLow - d1;
            if (intervalLow <= 0.5) { intervalLow = 0.5f; }
            timer1 = 0;
        }
        if (timer2 >= intervalMiddle)
        {
            bornZombie("Middle", 1);
            intervalMiddle = intervalMiddle - d2;
            if (intervalMiddle <= 2) { intervalMiddle = 2f; }
            timer2 = 0;
        }
        if (timer3 >= intervalGreat)
        {
            bornZombie("Great", 1);
            intervalGreat = intervalGreat - d3;
            if (intervalGreat <= 3) { intervalGreat = 3f; }
            timer3 = 0;
        }
    }
    public void waveZombie()
    {
        if (timer4>=intervalWave)
        {
            if (intervalWave <= 30)
            {
                intervalWave = 30;
            }
            if (wavenum == 1)
            { bronLowWave(1); bornZombie("Middle",3); }
            else if (wavenum == 2)
            { bronLowWave(2);bronMiddleWave(1); bornZombie("Great", 3); }
            else if (wavenum == 3)
            { bronLowWave(1); bronMiddleWave(2); bornGreatWave(2); }
            else
            {
                bronLowWave(wavenum-1); bronMiddleWave(wavenum-1); bornGreatWave(wavenum-1);
            }
            wavenum++;
            timer4 = 0;
        }
    }
    public void bornZombie(string str, int num)
    {
        for (int i = 1; i <= num; i++)
        {
            if (str == "Low") Invoke("bronZombieLow", Random.Range(0f, 1f));
            else if (str == "Middle") Invoke("bronZombieMiddle", Random.Range(0f, 1f));
            else if (str == "Great") Invoke("bronZombieGreat", Random.Range(0f, 1f));
            else { Debug.LogError("String Error:"+str); }
        }
    }
    public void bronZombieLow()
    {
        Instantiate(choiceZombieLow(), choicePos().position, Quaternion.identity);
    }
    public void bronZombieMiddle()
    {
        Instantiate(choiceZombieMiddle(), choicePos().position, Quaternion.identity);
    }
    public void bronZombieGreat()
    {
        Instantiate(choiceZombieGreat(), choicePos().position, Quaternion.identity);
    }
    public void zombieBegin()//初始时来的僵尸
    {
        bornZombie("Low", 3);
        bornZombie("Middle", 1);
    }
    public Transform choicePos()
    {
        int num = Random.Range(1, 6);
        if (num==1) { return Pos1; }
        if (num==2) { return Pos2; }
        if (num == 3) { return Pos3; }
        if (num == 4) { return Pos4; }
        if (num == 5) { return Pos5; }
        Debug.LogError("???Range(1, 6):" + num);
        return Pos1;
    }
    public GameObject choiceZombieLow()
    {
        int num = Random.Range(1, 5);
        if (num == 1) { return Zombie1; }
        if (num == 2) { return Zombie2; }
        if (num == 3) { return Zombie3; }
        if (num == 4) { return Zombie4; }
        Debug.LogError("???Range(1, 5):"+num);
        return Zombie1;
    }
    public GameObject choiceZombieMiddle()
    {
        int num = Random.Range(1, 6);
        if (num == 1) { return Zombie5; }
        if (num == 2) { return Zombie6; }
        if (num == 3) { return Zombie7; }
        if (num == 4) { return Zombie8; }
        if (num == 5) { return Zombie9; }
        Debug.LogError("???Range(1, 6):" + num);
        return Zombie1;
    }
    public GameObject choiceZombieGreat()
    {
        int num = Random.Range(1, 6);
        if (num == 1) { return Zombie10; }
        if (num == 2) { return Zombie11; }
        if (num == 3) { return Zombie12; }
        if (num == 4) { return Zombie13; }
        if (num == 5) { return Zombie14; }
        Debug.LogError("???Range(1, 6):" + num);
        return Zombie1;
    }
    public void bronLowWave(int n)
    {
        for (int i =1; i <= n; i++) {
            Instantiate(choiceZombieLow(), Pos1.position, Quaternion.identity);
            Instantiate(choiceZombieLow(), Pos2.position, Quaternion.identity);
            Instantiate(choiceZombieLow(), Pos3.position, Quaternion.identity);
            Instantiate(choiceZombieLow(), Pos4.position, Quaternion.identity);
            Instantiate(choiceZombieLow(), Pos5.position, Quaternion.identity); 
        }
    }
    public void bronLowWaveSame(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            temp = choiceZombieLow();
            Instantiate(temp, Pos1.position, Quaternion.identity);
            Instantiate(temp, Pos2.position, Quaternion.identity);
            Instantiate(temp, Pos3.position, Quaternion.identity);
            Instantiate(temp, Pos4.position, Quaternion.identity);
            Instantiate(temp, Pos5.position, Quaternion.identity);
        }
    }
    public void bronMiddleWave(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Instantiate(choiceZombieMiddle(), Pos1.position, Quaternion.identity);
            Instantiate(choiceZombieMiddle(), Pos2.position, Quaternion.identity);
            Instantiate(choiceZombieMiddle(), Pos3.position, Quaternion.identity);
            Instantiate(choiceZombieMiddle(), Pos4.position, Quaternion.identity);
            Instantiate(choiceZombieMiddle(), Pos5.position, Quaternion.identity);
        }
    }
    public void bronMiddleWaveSame(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            temp = choiceZombieMiddle();
            Instantiate(temp, Pos1.position, Quaternion.identity);
            Instantiate(temp, Pos2.position, Quaternion.identity);
            Instantiate(temp, Pos3.position, Quaternion.identity);
            Instantiate(temp, Pos4.position, Quaternion.identity);
            Instantiate(temp, Pos5.position, Quaternion.identity);
        }
    }
    public void bornGreatWave(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Instantiate(choiceZombieGreat(), Pos1.position, Quaternion.identity);
            Instantiate(choiceZombieGreat(), Pos2.position, Quaternion.identity);
            Instantiate(choiceZombieGreat(), Pos3.position, Quaternion.identity);
            Instantiate(choiceZombieGreat(), Pos4.position, Quaternion.identity);
            Instantiate(choiceZombieGreat(), Pos5.position, Quaternion.identity);
        }
    }
    public void bornGreatWaveSame(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            temp = choiceZombieGreat();
            Instantiate(temp, Pos1.position, Quaternion.identity);
            Instantiate(temp, Pos2.position, Quaternion.identity);
            Instantiate(temp, Pos3.position, Quaternion.identity);
            Instantiate(temp, Pos4.position, Quaternion.identity);
            Instantiate(temp, Pos5.position, Quaternion.identity);
        }
    }
    public void intervalControl()
    {
        
    }
}
