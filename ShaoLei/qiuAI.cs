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
    //������һ��ʱ���õĺ���
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
    //�ռ��۲�Ľ��
    public override void CollectObservations(VectorSensor sensor)
    {
        //һ���۲���8��floatֵ
        //һ��position������ֵ
        sensor.AddObservation(target.position);
        sensor.AddObservation(this.transform.position);
        //
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);
    }
    //���ն������Ƿ���轱��
    public override void OnActionReceived(float[] vectorAction)
    {
        //print(vectorAction[0]);
        //print(vectorAction[1]);
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.y = vectorAction[1];
        //�ƶ�С��
        rBody.AddForce(control * speed);
        //���ӳ���
        if (System.Math.Abs(this.transform.position.x) > 9 || (System.Math.Abs(this.transform.position.y)) > 5)
        {
            EndEpisode();
        }
        //���ӳԵ�����
        float distance = Vector3.Distance(this.transform.position, target.position);
        if(distance<0.4f)
        { 
            SetReward(1.0f);
            EndEpisode();
        }

    }
    //�Ƿ��ֶ�����������
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
