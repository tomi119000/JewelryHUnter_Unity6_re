using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Result画面のscoreTextオブジェクトが持つ
        //TextMeshProUGUIのtext欄に
        //GameManagerクラスのtotalScore変数の値を文字列に変換したものを代入
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.totalScore.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
