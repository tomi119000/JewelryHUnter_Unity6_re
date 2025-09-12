using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    public AudioClip bgm_Title; //タイトルBGM
    public AudioClip bgm_Stage;
    public AudioClip bgm_Result;
    public AudioClip bgm_GameClear;
    public AudioClip bgm_GameOver;

    AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AudioSourceコンポーネントの情報を取得
        audio = GetComponent<AudioSource>();

        //現在シーン情報の取得
        string currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName == "Title")
        {
            //タイトルの曲を再生する処理
            PlayBGM(bgm_Title);
        }
        else if(currentSceneName =="Result")
        {
            //Resultの曲を再生する処理
            PlayBGM(bgm_Result);

        }
        else
        {
            //Stageの曲を再生する処理
            PlayBGM(bgm_Stage);
        }
    }

    void PlayBGM(AudioClip clip)
    {
        audio.clip = clip;
        audio.loop = true; //ループ再生する設定
        audio.Play();  //再生する
    }

}
