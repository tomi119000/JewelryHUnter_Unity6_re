using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //静的なgameState変数を用意（静的メンバ）
    //静的メンバ（static）を設定することで外から見れる
    public static string gameState;

    public static int totalScore;  //ゲーム全般を通してのスコア
    public static int stageScore; //各ステージで獲得したスコア

    private void Awake() //Startよりも前に実行される（Awake）
    {
        //ゲームの初期状態をplayingとする
        gameState = "playing";

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameState); //gameStateの中身を確認するためにログを出力
    }
}
