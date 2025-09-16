using UnityEngine;
using UnityEngine.UI; //UI Objectを認識するため
using TMPro; //TextMeshProを認識するため

public class UIController : MonoBehaviour
{
    public GameObject mainImage; //アナウンスをする画像
    public GameObject buttonPanel; //ボタンをグループ化しているパネル

    public GameObject retryButton; //リトライボタン
    public GameObject nextButton;  //ネクストボタン

    public Sprite gameClearSprite; //ゲームクリアの絵
    public Sprite gameOverSprite;  //ゲームオーバーの絵

    TimeController timeCnt; //TimeController スクリプトを扱うための変数
    public GameObject timeText; //timeText Objectを扱うための変数

    public GameObject scoreText; //scoreText Objectを扱うための変数

    //GameOverとGameClearのAudioはUIで担当してもらう
    AudioSource audio;
    SoundController soundController; //自作したスクリプト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeCnt = GetComponent<TimeController>(); //TimeController script(Component)を取得
        buttonPanel.SetActive(false);  //存在を非表示
        Invoke("InactiveImage", 1.0f);  //時間差(ここでは1秒後)でメソッドを発動する
        
        UpdateScore(); //UIに最初の数字を反映するメソッドを呼び出す

        //"Canvas"のAudioSource componentとSoundController componentを取得する 
        audio = GetComponent<AudioSource>();
        soundController = GetComponent<SoundController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true); //ボタンパネル復活
            mainImage.SetActive(true); //メイン画像復活
            //メイン画像オブジェクトのImageComponentが所持しているsprite（変数）にステージクリアの絵を挿入
            mainImage.GetComponent<Image>().sprite = gameClearSprite;

            //リトライボタンObjectのButtonComponentが所持している変数interactableを無効化
            retryButton.GetComponent<Button>().interactable = false;

            //Stageクリアによってステージスコアが確定したので
            //トータルスコアに加算する
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0; //次面に備えてステージスコアリセット

            timeCnt.isTimeOver = true; //タイムカウントを停止

            float times = timeCnt.displayTime;
            if(timeCnt.isCountDown)
            {
                //残時間がボーナスとしてスコアに加算される
                GameManager.totalScore += (int)times * 10; 
            }
            else //カウントアップの場合
            {
                float gameTime = timeCnt.gameTime; //基準時間の取得
                GameManager.totalScore +=(int) (gameTime - times)*10;
            }

            UpdateScore(); //UIに最終的な数字を反映するメソッドを呼び出す

            //BGMを止める
            audio.Stop();
            //一回だけ音を鳴らす（soundCntroller script(Component)のbgm_GameClearを指定）
            audio.PlayOneShot(soundController.bgm_GameClear); 

            //ゲーム終了Stateに変更
            //2重3重にスコアを加算しないようにするため
            GameManager.gameState = "gameend"; 
        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //gameclear stateでボタンパネル復活
            mainImage.SetActive(true); //メイン画像復活
            //メイン画像オブジェクトのImageComponentが所持しているsprite（変数）にステージクリアの絵を挿入
            mainImage.GetComponent<Image>().sprite = gameOverSprite;

            //NextボタンObjectのButtonComponentが所持している変数interactableを無効化
            nextButton.GetComponent<Button>().interactable = false;

            timeCnt.isTimeOver = true; //タイムカウントを停止

            //BGMを止める
            audio.Stop();
            //一回だけ音を鳴らす
            audio.PlayOneShot(soundController.bgm_GameOver);

            GameManager.gameState = "gameend"; //ゲーム終了Stateに変更
        }

        else if(GameManager.gameState == "playing")
        {
            //いったんdisplaytimeの数字を変数：timesに渡しておく
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();
            // toString methodでfloat型のtimesをstring型に変換してtextに挿入
            //Mathf.Ceil methodで四捨五入

            if (timeCnt.isCountDown)
            {
                if (timeCnt.displayTime <= 0)
                {
                    //Playerを見つけて、そのPlayerController（Component）のGameOverメソッドを実行
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                if (timeCnt.displayTime >= timeCnt.gameTime)
                {
                    GameManager.gameState = "gameover";
                }
            }

        }
    }

    //メイン画像を非表示にするためだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false); 
    }

    //ScoreBoardを更新するためのメソッド
    void UpdateScore()
    {   
        //Total Scoreを計算
        int score = GameManager.stageScore + GameManager.totalScore;

        //ScoreText ObjectのTextMeshPro Componentが所持しているtext変数にscoreを挿入
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
