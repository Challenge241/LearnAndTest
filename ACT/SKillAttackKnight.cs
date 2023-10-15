using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SKillAttackKnight : MonoBehaviour
{
    public int damage = 30; // �˺�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�Ķ����Ƿ��ǵ���
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ����ǵ��ˣ����������˺�
            collision.gameObject.GetComponent<KnightEnemy>().TakeDamage(damage);
        }
    }
    public void SkillEnd()
    {
        transform.gameObject.SetActive(false);
    }
}
