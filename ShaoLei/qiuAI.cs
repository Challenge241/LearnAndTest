using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class qiuAI : Agent
{
    public Transform target;

    public float speed;

    public Rigidbody2D rBody;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //进入新一轮时调用的函数
    public override void OnEpisodeBegin()
    {
        //print("Start");
        if (System.Math.Abs(this.transform.position.x) > 9 || (System.Math.Abs(this.transform.position.y)) > 5)
        {
            this.transform.position = new Vector3(0, 0, 0);
            this.rBody.velocity = Vector3.zero;
            this.rBody.angularVelocity = 0;
        }
        target.position = new Vector3(Random.value/2, UnityEngine.Random.value / 2,0);
    }
    //收集观察的结果
    public override void CollectObservations(VectorSensor sensor)
    {
        //一共观察了8个float值
        //一个position有三个值
        sensor.AddObservation(target.position);
        sensor.AddObservation(this.transform.position);
        //
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);
    }
    //接收动作，是否给予奖励
    public override void OnActionReceived(float[] vectorAction)
    {
        //print(vectorAction[0]);
        //print(vectorAction[1]);
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.y = vectorAction[1];
        //移动小球
        rBody.AddForce(control * speed);
        //狗子出界
        if (System.Math.Abs(this.transform.position.x) > 9 || (System.Math.Abs(this.transform.position.y)) > 5)
        {
            EndEpisode();
        }
        //狗子吃到东西
        float distance = Vector3.Distance(this.transform.position, target.position);
        if(distance<0.4f)
        { 
            SetReward(1.0f);
            EndEpisode();
        }

    }
    //是否手动操作智能体
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
