using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float speed = 2.0f; // �����������ٶ�
    public float lifetime = 10.0f; // ������ڵ�ʱ��
    public int scoreValue = 2; // �������õ��ķ���
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnMouseDown()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.IncreaseScore(scoreValue);
        }
        else if (OnlyQiQiuManager.instance != null)
        {
            OnlyQiQiuManager.instance.IncreaseScore(scoreValue);
        }

        // ������Ϸ����
        Destroy(gameObject);
    }

}
