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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeCnt = GetComponent<TimeController>(); //TimeController script(Component)を取得
        buttonPanel.SetActive(false);  //存在を非表示
        Invoke("InactiveImage", 1.0f);  //時間差(ここでは1秒後)でメソッドを発動する
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true); //gameclearステートでボタンパネル復活
            mainImage.SetActive(true); //メイン画像復活
            //メイン画像オブジェクトのImageComponentが所持しているsprite（変数）にステージクリアの絵を挿入
            mainImage.GetComponent<Image>().sprite = gameClearSprite;

            //リトライボタンObjectのButtonComponentが所持している変数interactableを無効化
            retryButton.GetComponent<Button>().interactable = false; 
        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //gameclear stateでボタンパネル復活
            mainImage.SetActive(true); //メイン画像復活
            //メイン画像オブジェクトのImageComponentが所持しているsprite（変数）にステージクリアの絵を挿入
            mainImage.GetComponent<Image>().sprite = gameOverSprite;

            //NextボタンObjectのButtonComponentが所持している変数interactableを無効化
            nextButton.GetComponent<Button>().interactable = false;
        }
        else if(GameManager.gameState == "playing")
        {
            //いったんdisplaytimeの数字を変数：timesに渡しておく
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();
            // toString methodでfloat型のtimesをstring型に変換してtextに挿入
            //Mathf.Ceil methodで四捨五入


        }
    }

    //メイン画像を非表示にするためだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false); 
    }
}
