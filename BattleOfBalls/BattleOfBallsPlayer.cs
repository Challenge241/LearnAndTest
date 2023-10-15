using UnityEngine;
public class BattleOfBallsPlayer : MonoBehaviour
{
    public GameObject smallBallPrefab; // 小球的预设物
    public float smallBallRadius = 0.5f; // 小球的半径
    public GameObject playerPrefab;  // 玩家预制体
    public float speed = 5f;
    public float growthFactor = 0.1f; // 球增长的大小
    public float speedDecreaseFactor = 0.1f; // 速度减小的程度
    private Rigidbody2D rb;
    BallFood myballFood;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool canMoveUp = true;
    private bool canMoveDown = true;
    public float ABC = 0.1f;
    public float startArea;  // 初始面积
    public float currentArea; // 当前面积
    public float startSpeed; // 初始速度
    public float tuqiuSpeed = 12f;
    public float minimumArea=0.15f;
    public float forceMagnitude = 10f;  // 力的大小
    Vector3 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myballFood = GetComponent<BallFood>();
        float radius = transform.localScale.x / 2;
        startArea = Mathf.PI * Mathf.Pow(radius, 2);
        currentArea = startArea;
        myballFood.area= startArea;
        startSpeed = speed;
}

    void Update()
    {
        float moveHorizontal = canMoveLeft && canMoveRight ? Input.GetAxis("Horizontal") : 
            (canMoveRight ? Mathf.Max(0, Input.GetAxis("Horizontal")) : Mathf.Min(0, Input.GetAxis("Horizontal")));
        float moveVertical = canMoveUp && canMoveDown ? Input.GetAxis("Vertical") : 
            (canMoveUp ? Mathf.Max(0, Input.GetAxis("Vertical")) : Mathf.Min(0, Input.GetAxis("Vertical")));
        movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        // 设置 Rigidbody 的速度
        rb.velocity = movement * speed;
        // 检查是否按下了 "K" 键
        if (Input.GetKeyDown(KeyCode.K))
        {
            Split();
        }

        // 如果玩家按下了"L键"
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpitSmallBall();
        }
        Vector2 velocity = rb.velocity;
        // 计算并获取速度向量的方向向量
        Vector2 direction = velocity.normalized;
        print(direction);
        print(transform.forward);
    }
    private void Split()
    {
        // 获取当前球的半径
        float oldRadius = transform.localScale.x / 2;

        // 如果当前球的面积小于0.1，禁止分裂
        float oldArea = Mathf.PI * Mathf.Pow(oldRadius, 2);
        if (oldArea < 0.1)
        {
            return;
        }

        // 创建弹射方向，这里可以根据你的游戏设计来修改
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

        // 计算新的位置，让新的球在旧的球旁边，不接触
        Vector3 newPosition = transform.position + direction * oldRadius * 2;

        // 创建一个新的玩家
        GameObject newPlayer = Instantiate(playerPrefab, newPosition, Quaternion.identity);

        // 将新生成的球添加到CameraController的小球列表中
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.balls.Add(newPlayer);
        }

        // 计算新的面积
        float newArea = oldArea / 2;
        myballFood.area = newArea;

        // 计算新的半径
        float newRadius = Mathf.Sqrt(newArea / Mathf.PI);
        //设置新球位置
        newPlayer.transform.position = transform.position + direction * newRadius * 2;
        // 设置当前玩家和新玩家的大小
        transform.localScale = new Vector3(newRadius * 2, newRadius * 2, transform.localScale.z);
        newPlayer.transform.localScale = new Vector3(newRadius * 2, newRadius * 2, newPlayer.transform.localScale.z);

        speed=AdjustSpeed(newArea);
        newPlayer.transform.GetComponent<BattleOfBallsPlayer>().speed= AdjustSpeed(newArea);
    }

    private void SpitSmallBall()
    {
        // 根据大球的大小计算其半径
        float bigBallRadius = transform.localScale.x / 2;

        // 计算大球的面积
        float bigBallArea = Mathf.PI * Mathf.Pow(bigBallRadius, 2);

        // 检查大球面积是否足够大
        if (bigBallArea <= minimumArea)
        {
            //Debug.LogWarning("Big ball area is too small to create a small ball!");
            return;
        }

        // 创建小球
        GameObject smallBall = Instantiate(smallBallPrefab, rb.position + new Vector2(rb.transform.up.x, rb.transform.up.y), Quaternion.identity);

        // 根据小球的大小计算其半径
        float smallBallRadius = smallBall.transform.localScale.x / 2;
        //让大小球相切，给小球一个速度，让小球离开
        Vector3 smallBallPosition;
        if (movement.x == 0 && movement.y == 0) {
            smallBallPosition = transform.position + transform.up * (bigBallRadius + smallBallRadius);
            smallBall.GetComponent<Rigidbody2D>().velocity = tuqiuSpeed * transform.up;
        }
        else {
            smallBallPosition = transform.position + movement * (bigBallRadius + smallBallRadius);
            smallBall.GetComponent<Rigidbody2D>().velocity = tuqiuSpeed * movement;
            Vector3 force = movement * forceMagnitude;
            smallBall.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
        smallBall.transform.position = smallBallPosition;

        // 计算小球的面积
        float smallBallArea = Mathf.PI * Mathf.Pow(smallBallRadius, 2);
        // 检查 bigBallArea 是否大于 smallBallArea
        if (bigBallArea > smallBallArea)
        {
            float newBigBallArea = bigBallArea - smallBallArea;
            myballFood.area = newBigBallArea;
            float newBigBallRadius = Mathf.Sqrt(newBigBallArea / Mathf.PI);

            // 根据大球的新半径调整其大小
            float bigBallDiameter = newBigBallRadius * 2;
            transform.localScale = new Vector3(bigBallDiameter, bigBallDiameter, bigBallDiameter);

            // 调整速度
            speed = AdjustSpeed(newBigBallArea);
        }
        else
        {
            Debug.LogWarning("Big ball area is smaller than the small ball area!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("BarriarLeft"))
        {
            canMoveLeft = false;
        }
        else if (collision.gameObject.CompareTag("BarriarRight"))
        {
            canMoveRight = false;
        }
        else if (collision.gameObject.CompareTag("BarriarDown"))
        {
            canMoveDown = false;
        }
        else if (collision.gameObject.CompareTag("BarriarUp"))
        {
            canMoveUp = false;
        }
        else { }
    }

    // 计算两个圆的重叠面积的方法
    float CalculateOverlapArea(float r1, float r2, float d)
    {
        // 假设 d <= r1 + r2
        float r = Mathf.Min(r1, r2);
        float R = Mathf.Max(r1, r2);

        if (d >= r + R)
        {
            return 0; // 两个圆没有重叠
        }
        else if (d <= R - r)
        {
            return Mathf.PI * r * r; // 其中一个圆完全在另一个圆内
        }
        else
        {
            float part1 = r * r * Mathf.Acos((d * d + r * r - R * R) / (2 * d * r));
            float part2 = R * R * Mathf.Acos((d * d + R * R - r * r) / (2 * d * R));
            float part3 = 0.5f * Mathf.Sqrt((-d + r + R) * (d + r - R) * (d - r + R) * (d + r + R));

            return part1 + part2 - part3; // 计算重叠区域的面积
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        // 检查碰撞体的标签是否为食物
        if (collision.gameObject.CompareTag("Food"))
        {
            // 获取食物的脚本组件，以获取食物的面积
            BallFood food = collision.GetComponent<BallFood>();
            if (food != null)
            {
                // 如果是食物，看看谁更大，大的就吞噬食物并且增大球的大小，减小速度
                //等效于小就return，大就照常吃
                if (myballFood.area < food.area)
                {
                    return;
                }
                float distance = Vector3.Distance(transform.position, collision.transform.position);
                float overlapArea=CalculateOverlapArea(transform.localScale.x / 2, collision.transform.localScale.x/2, distance);
                if (overlapArea < food.area / 2)
                {
                    return;
                }
                if (collision.GetComponent<BattleOfBallsPlayer>() != null)
                {
                    CameraController cameraController = FindObjectOfType<CameraController>();
                    cameraController.balls.Remove(collision.gameObject);
                }
                // 消除食物
                Destroy(collision.gameObject);

                // 增大球的面积
                float playerRadius = transform.localScale.x / 2;
                float playerArea = Mathf.PI * Mathf.Pow(playerRadius, 2);
                playerArea += food.area;
                myballFood.area = playerArea;
                currentArea = myballFood.area;

                // 计算新的半径
                float newRadius = Mathf.Sqrt(playerArea / Mathf.PI);

                // 更新球的大小
                transform.localScale = new Vector3(newRadius * 2, newRadius * 2, transform.localScale.z);

                speed = AdjustSpeed(currentArea);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BarriarLeft"))
        {
            canMoveLeft = true;
        }
        else if (collision.gameObject.CompareTag("BarriarRight"))
        {
            canMoveRight = true;
        }
        else if (collision.gameObject.CompareTag("BarriarDown"))
        {
            canMoveDown = true;
        }
        else if (collision.gameObject.CompareTag("BarriarUp"))
        {
            canMoveUp = true;
        }
    }
    private float AdjustSpeed(float currentArea)
    {
        // 计算面积的变化比例，然后取对数，得到 n 的值
        float areaRatio = currentArea / startArea;
        float n = Mathf.Log(areaRatio, 2);

        float newSpeed;

        // 根据面积的变化来改变速度
        if (n > 0)
        {
            // 如果面积扩大，速度减少
            newSpeed = startSpeed * Mathf.Pow((1 - ABC), n);
        }
        else if (n < 0)
        {
            // 如果面积缩小，速度增加
            newSpeed = startSpeed * Mathf.Pow((1 + ABC), -n);
        }
        else
        {
            // 如果面积未变，速度保持不变
            newSpeed = startSpeed;
        }

        return newSpeed;
    }

}
