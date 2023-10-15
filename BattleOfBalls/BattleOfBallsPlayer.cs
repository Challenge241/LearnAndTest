using UnityEngine;
public class BattleOfBallsPlayer : MonoBehaviour
{
    public GameObject smallBallPrefab; // С���Ԥ����
    public float smallBallRadius = 0.5f; // С��İ뾶
    public GameObject playerPrefab;  // ���Ԥ����
    public float speed = 5f;
    public float growthFactor = 0.1f; // �������Ĵ�С
    public float speedDecreaseFactor = 0.1f; // �ٶȼ�С�ĳ̶�
    private Rigidbody2D rb;
    BallFood myballFood;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private bool canMoveUp = true;
    private bool canMoveDown = true;
    public float ABC = 0.1f;
    public float startArea;  // ��ʼ���
    public float currentArea; // ��ǰ���
    public float startSpeed; // ��ʼ�ٶ�
    public float tuqiuSpeed = 12f;
    public float minimumArea=0.15f;
    public float forceMagnitude = 10f;  // ���Ĵ�С
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
        // ���� Rigidbody ���ٶ�
        rb.velocity = movement * speed;
        // ����Ƿ����� "K" ��
        if (Input.GetKeyDown(KeyCode.K))
        {
            Split();
        }

        // �����Ұ�����"L��"
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpitSmallBall();
        }
        Vector2 velocity = rb.velocity;
        // ���㲢��ȡ�ٶ������ķ�������
        Vector2 direction = velocity.normalized;
        print(direction);
        print(transform.forward);
    }
    private void Split()
    {
        // ��ȡ��ǰ��İ뾶
        float oldRadius = transform.localScale.x / 2;

        // �����ǰ������С��0.1����ֹ����
        float oldArea = Mathf.PI * Mathf.Pow(oldRadius, 2);
        if (oldArea < 0.1)
        {
            return;
        }

        // �������䷽��������Ը��������Ϸ������޸�
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

        // �����µ�λ�ã����µ����ھɵ����Աߣ����Ӵ�
        Vector3 newPosition = transform.position + direction * oldRadius * 2;

        // ����һ���µ����
        GameObject newPlayer = Instantiate(playerPrefab, newPosition, Quaternion.identity);

        // �������ɵ�����ӵ�CameraController��С���б���
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.balls.Add(newPlayer);
        }

        // �����µ����
        float newArea = oldArea / 2;
        myballFood.area = newArea;

        // �����µİ뾶
        float newRadius = Mathf.Sqrt(newArea / Mathf.PI);
        //��������λ��
        newPlayer.transform.position = transform.position + direction * newRadius * 2;
        // ���õ�ǰ��Һ�����ҵĴ�С
        transform.localScale = new Vector3(newRadius * 2, newRadius * 2, transform.localScale.z);
        newPlayer.transform.localScale = new Vector3(newRadius * 2, newRadius * 2, newPlayer.transform.localScale.z);

        speed=AdjustSpeed(newArea);
        newPlayer.transform.GetComponent<BattleOfBallsPlayer>().speed= AdjustSpeed(newArea);
    }

    private void SpitSmallBall()
    {
        // ���ݴ���Ĵ�С������뾶
        float bigBallRadius = transform.localScale.x / 2;

        // �����������
        float bigBallArea = Mathf.PI * Mathf.Pow(bigBallRadius, 2);

        // ����������Ƿ��㹻��
        if (bigBallArea <= minimumArea)
        {
            //Debug.LogWarning("Big ball area is too small to create a small ball!");
            return;
        }

        // ����С��
        GameObject smallBall = Instantiate(smallBallPrefab, rb.position + new Vector2(rb.transform.up.x, rb.transform.up.y), Quaternion.identity);

        // ����С��Ĵ�С������뾶
        float smallBallRadius = smallBall.transform.localScale.x / 2;
        //�ô�С�����У���С��һ���ٶȣ���С���뿪
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

        // ����С������
        float smallBallArea = Mathf.PI * Mathf.Pow(smallBallRadius, 2);
        // ��� bigBallArea �Ƿ���� smallBallArea
        if (bigBallArea > smallBallArea)
        {
            float newBigBallArea = bigBallArea - smallBallArea;
            myballFood.area = newBigBallArea;
            float newBigBallRadius = Mathf.Sqrt(newBigBallArea / Mathf.PI);

            // ���ݴ�����°뾶�������С
            float bigBallDiameter = newBigBallRadius * 2;
            transform.localScale = new Vector3(bigBallDiameter, bigBallDiameter, bigBallDiameter);

            // �����ٶ�
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

    // ��������Բ���ص�����ķ���
    float CalculateOverlapArea(float r1, float r2, float d)
    {
        // ���� d <= r1 + r2
        float r = Mathf.Min(r1, r2);
        float R = Mathf.Max(r1, r2);

        if (d >= r + R)
        {
            return 0; // ����Բû���ص�
        }
        else if (d <= R - r)
        {
            return Mathf.PI * r * r; // ����һ��Բ��ȫ����һ��Բ��
        }
        else
        {
            float part1 = r * r * Mathf.Acos((d * d + r * r - R * R) / (2 * d * r));
            float part2 = R * R * Mathf.Acos((d * d + R * R - r * r) / (2 * d * R));
            float part3 = 0.5f * Mathf.Sqrt((-d + r + R) * (d + r - R) * (d - r + R) * (d + r + R));

            return part1 + part2 - part3; // �����ص���������
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        // �����ײ��ı�ǩ�Ƿ�Ϊʳ��
        if (collision.gameObject.CompareTag("Food"))
        {
            // ��ȡʳ��Ľű�������Ի�ȡʳ������
            BallFood food = collision.GetComponent<BallFood>();
            if (food != null)
            {
                // �����ʳ�����˭���󣬴�ľ�����ʳ�ﲢ��������Ĵ�С����С�ٶ�
                //��Ч��С��return������ճ���
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
                // ����ʳ��
                Destroy(collision.gameObject);

                // ����������
                float playerRadius = transform.localScale.x / 2;
                float playerArea = Mathf.PI * Mathf.Pow(playerRadius, 2);
                playerArea += food.area;
                myballFood.area = playerArea;
                currentArea = myballFood.area;

                // �����µİ뾶
                float newRadius = Mathf.Sqrt(playerArea / Mathf.PI);

                // ������Ĵ�С
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
        // ��������ı仯������Ȼ��ȡ�������õ� n ��ֵ
        float areaRatio = currentArea / startArea;
        float n = Mathf.Log(areaRatio, 2);

        float newSpeed;

        // ��������ı仯���ı��ٶ�
        if (n > 0)
        {
            // �����������ٶȼ���
            newSpeed = startSpeed * Mathf.Pow((1 - ABC), n);
        }
        else if (n < 0)
        {
            // ��������С���ٶ�����
            newSpeed = startSpeed * Mathf.Pow((1 + ABC), -n);
        }
        else
        {
            // ������δ�䣬�ٶȱ��ֲ���
            newSpeed = startSpeed;
        }

        return newSpeed;
    }

}
