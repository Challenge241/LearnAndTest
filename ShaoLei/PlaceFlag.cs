using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFlag : MonoBehaviour
{
    // 定义一个公开的GameObject类型变量flagPrefab，这个变量用于存放你的旗帜预制体
    public GameObject flagPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //placeflag();
    }
    /*在Unity中，使用鼠标操作UI元素（例如Button，Image等）和操作3D或2D游戏对象有所不同。
     * 对于UI元素，我们通常使用EventSystem和IPointerClickHandler，IPointerEnterHandler，IPointerExitHandler等接口，而不是使用射线投射。*/
    void placeflag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("1");
            // 从摄像机发射一条射线，射线的方向是从摄像机到鼠标光标在屏幕上的位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 创建一个RaycastHit变量，这个变量将会被用于存储射线投射的结果
            RaycastHit hit;

            // 如果射线投射命中了一个物体
            if (Physics.Raycast(ray, out hit))
            {
                print("y");
                // 如果命中的物体上没有名为"Flag"的子物体（也就是说，这个物体没有被标记）
                if (hit.transform.Find("Flag") == null)
                {
                    // 在命中的物体的位置实例化一个旗帜预制体
                    GameObject flag = Instantiate(flagPrefab, hit.transform.position, Quaternion.identity);

                    // 将实例化的旗帜的名字设置为"Flag"
                    flag.name = "Flag";

                    // 将实例化的旗帜设置为命中的物体的子物体
                    flag.transform.SetParent(hit.transform);
                    hit.transform.GetComponent<SLGrid>().isFlag = true;
                }
                // 如果命中的物体上已经有名为"Flag"的子物体
                else
                {
                    // 销毁名为"Flag"的子物体（也就是说，移除标记）
                    Destroy(hit.transform.Find("Flag").gameObject);
                    hit.transform.GetComponent<SLGrid>().isFlag = false;
                }
            }
        }
    }
}
