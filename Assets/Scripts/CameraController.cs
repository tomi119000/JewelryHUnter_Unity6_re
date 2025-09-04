using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    // GameObject型の変数（player）を設定
    GameObject player;
    float x, y, z; //カメラの位置を設定するための変数

    [Header("カメラの限界値")]
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    [Header("カメラのスクロール設定")]
    public bool isScrollX; //横方向に強制スクロールするのかのフラグ
    public float scrollSpeedX = 0.5f;
    public bool isScrollY; //縦方向に強制スクロールするのかのフラグ
    public float scrollSpeedY = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //"Player"tagを持ったゲームオブジェクトを取得 .FindGameObjectWithTag("aaa");
        player = GameObject.FindGameObjectWithTag("Player");
        //カメラのｚ座標は初期値のままを維持したい
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Objectのx, y 座標を取得
        x = player.transform.position.x;
        y = player.transform.position.y;

        if(isScrollX) //isScrollX flagがtrueだったら
        {
            //前の座標にscrollSpeedXだけ足していく
            x = transform.position.x + (scrollSpeedX * Time.deltaTime); 
        }

        if(x < leftLimit)
        {
            x = leftLimit;
        }
        else if(x > rightLimit)
        {
            x = rightLimit;
        }

        if(isScrollY) //isScrollY flagがtrueだったら
        {
            y = transform.position.y + (scrollSpeedY * Time.deltaTime);
        }

        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if(y > topLimit)
        {
            y = topLimit;
        }

            //取り決めた各変数x, y, zの値をカメラのポジションとする
        transform.position = new Vector3(x, y, z);

    }
}
