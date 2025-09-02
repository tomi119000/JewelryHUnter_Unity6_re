using UnityEngine;  //名前空間（ライブラリ of）

// Class difinition: MonoBehavior Classから継承 
// public class class名 -> public にするとInspector上に表示される(=Component= Instanse)
public class PlayerController : MonoBehaviour 
{
    Rigidbody2D rbody; //PlayerについているRigidbody2Dを扱うための変数（Rigidbody2D型）
    float axisH; //入力の方向を記憶するための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>( ); //Player Objectに付いているComponent情報を取得
    }

    // Update is called once per frame. 永久ループ(=while)
    void Update()
    {
        //Velocityの元となる値の取得（右なら1.0f, 左なら-1.0f、何もなければ0）
        axisH = Input.GetAxisRaw("Horizontal");
        // Vector2(Vector型) ：一次的にメモリに値を確保して目的となる変数に参照してもらう
        rbody.linearVelocity = new Vector2(axisH * 3.0f, rbody.linearVelocity.y);
    }
}
