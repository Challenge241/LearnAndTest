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
        // 设置光标纹理，热点位置以及光标模式。
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}

