using UnityEngine;

public enum ItemColor //アイテム専用の型を作成 列挙型
{
    White,
    Blue,
    Green,
    Red
}
public class ItemData : MonoBehaviour
{
    public ItemColor colors = ItemColor.White; // アイテムの色を格納する変数
    public Sprite[] itemSprites; //アイテムの絵を配列で格納する変数
    public int value = 0; //アイテムの価値を格納する変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        switch(colors)
        {
            case ItemColor.White:
                spriteRenderer.sprite = itemSprites[0];
                break;
            case ItemColor.Blue:
                spriteRenderer.sprite = itemSprites[1];
                break;
            case ItemColor.Green:
                spriteRenderer.sprite = itemSprites[2];
                break;
            case ItemColor.Red:
                spriteRenderer.sprite = itemSprites[3];
                break; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
