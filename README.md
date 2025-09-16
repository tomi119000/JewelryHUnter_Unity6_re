# JewelryHunter_Unity6_re

[ゲームのサンプルプレイ](https://github.com/tomi119000/JewelryHunter_web.git)

![ゲーム画面](readmeImg/title_screen.png)

## 訓練校で学習する初2D作品
* Playerの操作
* アニメーションの作り方
* タグやレイヤーの使い方  
など、基本を学んでいます。

## 制作のポイント
### アニメーションをトランジションで作成
Playerのアニメの切り替えには各クリップをトランジションでつないでフラグで管理しました。
トランジションを組むことで、アニメ切り替えが滑らかになり、且つコーディングが効率的なものになりました。
![トラジションの絵](readmeImg/animator_image.png)

## ItemのCodingの効率化
Itemは列挙型のItemColorを自作して、ItemColor型の変数次第で何色が選ばれているのかにより、見た目が変わるようなCodingの工夫を行いました。
![トランジションの絵](readmeImg/ItemColorSelection_image.png)

```C#
using UnityEngine;

public enum ItemColor
{
    White,
    Blue,
    Green,
    Red
}

public class ItemData : MonoBehaviour
{
    public ItemColor colors = ItemColor.White;
    public Sprite[] itemSprites;

    public int value = 0;       // 整数値を設定できる

    void Start()
    {        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        switch (colors)
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
}
```

## TextMeshProのデザイン切り分け
TextMeshProのデザインを細かく切り分けて、データを用意するなど工夫しました。
![TextMeshProデザインの絵](readmeImg/TextMeshPro.jpg)

*******************************
--------------------------
見出し
# H1見出し
## H2見出し
#### H4見出し

H1見出し（イコール行を挿入。）
==================
H2見出し（ハイフン行を挿入。）
---------------------------------

改行
行末に半角スペース2つ  
または普通に改行

**太字**
__これも太字__
_斜体_
~~取り消し~~
<ins>下線</ins>

水平線
3つ以上のハイフン、アスタリスクをならべる。間にスペースをいれても良い
***
---------------------------------------
* * *


リスト
* リスト1
* リストリスト1-2
* リスト2

1. 順番つきリスト
2. 順番つきリスト
3. 順番つきリスト


リンク
<http://google.com/>
文字にリンクをつける
[google](http://google.com/)


コード埋め込み
```C#
string s1 = "バッククォーテーション３つで囲みます";
string s2 = "先頭のクォーテーション後に言語名でハイライトされます";
if(s1 != null && s2 != null)
{
	Debug.Log(s1 + s2);
}
```


引用
> 引用本文引用本文引用本文引用本文


画像
![サンプル画像](readmeImg/samplesample.png)
