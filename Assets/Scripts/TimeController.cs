using UnityEngine;

public class TimeController : MonoBehaviour
{
    //変数設定
    //カウントダウンするかどうかのフラグ
    public bool isCountDown = true;

    //ゲームの基準となる時間
    public float gameTime = 0;

    //カウントを止めるか否かのフラグ
    public bool isTimeOver = false;

    //Userに見せる時間
    public float displayTime = 0;

    //ゲームの経過時間
    float times = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //もしCountDownであれば、基準時間をユーザーにみせる時間に代入する
        if (isCountDown)
        {
            displayTime = gameTime;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimeOver) //isTimeOverがfalseのとき「！」
        {
            //停止フラグが立っていないので処理したいが
            //ゲームステータスがplayingでなくなった時は止めたい
            if(GameManager.gameState != "playing")
            {
                isTimeOver = true; 
            }

            //カウントの処理をする
            times  += Time.deltaTime; //デルタタイムの蓄積

            if (isCountDown)
            {
                //ユーザーに見せたい時間（残時間）
                displayTime = gameTime - times;
                if(displayTime <= 0)
                {
                    displayTime = 0;
                    isTimeOver = true;
                    GameManager.gameState = "gameover"; 
                }
            }
            else //カウントアップ形式だった場合
                 //経過時間をユーザーに見せる
            {
                displayTime = times; 
                if(displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true; 
                    GameManager.gameState = "gameover";
                }
            }
            Debug.Log(displayTime); 
        }
    }
}
