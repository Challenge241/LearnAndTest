// 使用System.Collections命名空间
// 使用System.Collections.Generic命名空间
// 使用UnityEngine命名空间
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义公共类ColorSweet，继承自MonoBehaviour
public class ColorSweet : MonoBehaviour
{
    // 定义枚举ColorType，用于表示不同颜色的类型
    public enum ColorType
    {
        YELLOW, // 黄色
        PURPLE, // 紫色
        RED,    // 红色
        BLUE,   // 蓝色
        GREEN,  // 绿色
        PINK,   // 粉色
        ANY,    // 任意颜色
        COUNT   // 用于统计颜色类型的数量
    }

    // 定义结构体ColorSprite，包含ColorType类型的color和Sprite类型的sprite
    // 使用[System.Serializable]特性，使得该结构体可以在Unity编辑器中进行可视化编辑
    [System.Serializable]
    public struct ColorSprite
    {
        public ColorType color;
        public Sprite sprite;
    }

    // 定义ColorSprite类型的数组，用于存放不同颜色的Sprite
    public ColorSprite[] ColorSprites;

    // 定义字典colorSpriteDict，用于通过颜色类型快速查找对应的Sprite
    private Dictionary<ColorType, Sprite> colorSpriteDict;

    // 定义SpriteRenderer类型的变量，用于控制游戏对象的渲染
    private SpriteRenderer sprite;

    // 定义公共只读属性NumColor，返回ColorSprites数组的长度
    public int NumColors
    {
        get { return ColorSprites.Length; }
    }

    // 定义Color属性，用于获取或设置color字段的值
    public ColorType Color { get => color; set { SetColor(value); } }

    // 定义ColorType类型的字段，用于存放当前颜色类型
    private ColorType color;

    // 定义Awake方法，该方法在脚本实例被加载时被调用
    private void Awake()
    {
        // 获取子对象Sweet上的SpriteRenderer组件，存储到sprite变量中
        sprite = transform.Find("Sweet").GetComponent<SpriteRenderer>();

        // 实例化colorSpriteDict字典
        colorSpriteDict = new Dictionary<ColorType, Sprite>();

        // 遍历ColorSprites数组
        for (int i = 0; i < ColorSprites.Length; i++)
        {
            // 如果colorSpriteDict字典中不存在当前遍历到的颜色类型，就将其添加到字典中
            if (!colorSpriteDict.ContainsKey(ColorSprites[i].color))
            {
                colorSpriteDict.Add(ColorSprites[i].color, ColorSprites[i].sprite); 
            }
        }
    }

    // 定义SetColor方法，用于设置当前颜色类型，并更新SpriteRenderer组件的Sprite
    public void SetColor(ColorType newColor)
    {
        // 更新color字段的值
        color = newColor;

        // 如果colorSpriteDict字典中存在newColor，就将sprite的Sprite设置为newColor对应的Sprite
        if (colorSpriteDict.ContainsKey(newColor))
        {
            sprite.sprite = colorSpriteDict[newColor];
        }
    }
}