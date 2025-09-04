using NUnit.Framework.Constraints;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //名前空間（ライブラリ of）

// Class difinition: MonoBehavior Classから継承 
// public class class名 -> public にするとInspector上に表示される(=Component= Instanse)
public class PlayerController : MonoBehaviour 
{
    [Header("Playerの能力値")] //publicの場合に、Inspectorに見出しを付けることができる
    Rigidbody2D rbody; //PlayerについているRigidbody2Dを扱うための変数（Rigidbody2D型）
    float axisH; //入力の方向を記憶するための変数
    public float speed = 3.0f;  //publicでUnityのInspector/PlayerControllerに表示
    public float jumpPower = 9.0f; //ジャンプ力
    bool goJump = false; //ジャンプフラグ（On/Off）
    bool onGround = false;
    public LayerMask groundLayer; 
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Player Objectに付いているComponent情報を取得
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame. 永久ループ(=while)
    void Update()
    {
        //Velocityの元となる値の取得（右なら1.0f, 左なら-1.0f、何もなければ0）
        axisH = Input.GetAxisRaw("Horizontal");
        // Vector2(Vector型) ：一次的にメモリに値を確保して目的となる変数に参照してもらう
        //PCの性能などに依存し、そのフレームレートで更新される--> FixedUpdate()でフレームレート固定
        //rbody.linearVelocity = new Vector2(axisH, 0);

        if(axisH > 0)
        {
            //TransformのScaleはVector3型（3つのfloat x, y, zの構造体）
            //Vector3型の値はメモリ（ヒープ）領域にnew演算子で記憶 --> localScaleが参照
            //transformは利用頻度が高いためRigidbodyのようにGetComponentしなくてよい
            //MonoBehaviorがやってくれている
            transform.localScale = new Vector3(1,1,1);
        }
        else if(axisH < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    //1秒間に50回繰り返す(FixedUpdate)ように制御しながら行う繰り返しメソッド
    private void FixedUpdate()
    {
        //CircleCastを飛ばして地面判定、その結果をonGroundに代入
        onGround = Physics2D.CircleCast(
            transform.position, //発射位置=プレイヤーの位置（基準点）
            0.2f,               //調査する円の半径
            new Vector2(0, 1.0f), //発射方向(※下方向）
            0,                   //発射距離
            groundLayer);         //対象(どのgroup(=Layer)と接触したらtrueにするのか）
        
        //Velocityに値を代入
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //ジャンプフラグが立ったら（trueだったら）
        if(goJump)
        {
            //ジャンプさせる = プレイヤーを瞬間的に上に押し出す
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            //ジャンプフラグをOffに戻す。無限ジャンプにならないように
            goJump = false;
        }

        if(onGround)
        {
            if(axisH ==0) //地面の上にいる時
            {
                animator.SetBool("Run", false); //Idleアニメに切り替え
            }
            else //左右が押されている
            {
                animator.SetBool("Run", true); //Runアニメに切り替え
            }
        }
    }

    //ジャンプボタンが押されたとき（Input.GetButtonDown("Jump"))に呼び出されるメソッド
    void Jump()
    {
        if(onGround) //onGroundがtrueだったら（onGround == true）
        {
            goJump = true; //ジャンプフラグ（goJump）をOnにする
            animator.SetTrigger("Jump");
        }
           
    }
}
