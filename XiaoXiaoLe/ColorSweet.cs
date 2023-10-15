// ʹ��System.Collections�����ռ�
// ʹ��System.Collections.Generic�����ռ�
// ʹ��UnityEngine�����ռ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���幫����ColorSweet���̳���MonoBehaviour
public class ColorSweet : MonoBehaviour
{
    // ����ö��ColorType�����ڱ�ʾ��ͬ��ɫ������
    public enum ColorType
    {
        YELLOW, // ��ɫ
        PURPLE, // ��ɫ
        RED,    // ��ɫ
        BLUE,   // ��ɫ
        GREEN,  // ��ɫ
        PINK,   // ��ɫ
        ANY,    // ������ɫ
        COUNT   // ����ͳ����ɫ���͵�����
    }

    // ����ṹ��ColorSprite������ColorType���͵�color��Sprite���͵�sprite
    // ʹ��[System.Serializable]���ԣ�ʹ�øýṹ�������Unity�༭���н��п��ӻ��༭
    [System.Serializable]
    public struct ColorSprite
    {
        public ColorType color;
        public Sprite sprite;
    }

    // ����ColorSprite���͵����飬���ڴ�Ų�ͬ��ɫ��Sprite
    public ColorSprite[] ColorSprites;

    // �����ֵ�colorSpriteDict������ͨ����ɫ���Ϳ��ٲ��Ҷ�Ӧ��Sprite
    private Dictionary<ColorType, Sprite> colorSpriteDict;

    // ����SpriteRenderer���͵ı��������ڿ�����Ϸ�������Ⱦ
    private SpriteRenderer sprite;

    // ���幫��ֻ������NumColor������ColorSprites����ĳ���
    public int NumColors
    {
        get { return ColorSprites.Length; }
    }

    // ����Color���ԣ����ڻ�ȡ������color�ֶε�ֵ
    public ColorType Color { get => color; set { SetColor(value); } }

    // ����ColorType���͵��ֶΣ����ڴ�ŵ�ǰ��ɫ����
    private ColorType color;

    // ����Awake�������÷����ڽű�ʵ��������ʱ������
    private void Awake()
    {
        // ��ȡ�Ӷ���Sweet�ϵ�SpriteRenderer������洢��sprite������
        sprite = transform.Find("Sweet").GetComponent<SpriteRenderer>();

        // ʵ����colorSpriteDict�ֵ�
        colorSpriteDict = new Dictionary<ColorType, Sprite>();

        // ����ColorSprites����
        for (int i = 0; i < ColorSprites.Length; i++)
        {
            // ���colorSpriteDict�ֵ��в����ڵ�ǰ����������ɫ���ͣ��ͽ�����ӵ��ֵ���
            if (!colorSpriteDict.ContainsKey(ColorSprites[i].color))
            {
                colorSpriteDict.Add(ColorSprites[i].color, ColorSprites[i].sprite); 
            }
        }
    }

    // ����SetColor�������������õ�ǰ��ɫ���ͣ�������SpriteRenderer�����Sprite
    public void SetColor(ColorType newColor)
    {
        // ����color�ֶε�ֵ
        color = newColor;

        // ���colorSpriteDict�ֵ��д���newColor���ͽ�sprite��Sprite����ΪnewColor��Ӧ��Sprite
        if (colorSpriteDict.ContainsKey(newColor))
        {
            sprite.sprite = colorSpriteDict[newColor];
        }
    }
}