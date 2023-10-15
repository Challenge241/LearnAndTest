using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    private void OnEnable()
    {
        // ���ù�������ȵ�λ���Լ����ģʽ��
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}

