using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指名する変数

    //タイトルへの切り替えかどうかのフラグ
    public bool toTitle;

    //シーンを切り替える機能を持るメソッド
    public void Load()
    {
        //シーンが切り替わるときはいずれにしてもステージスコアはリセットされる
        GameManager.stageScore = 0;

        //toTitleフラグがtrueの場合はタイトルに戻ることが予想されるのでトタルスコアもリセット
        //if文：命令が１つの場合は{}は省略可能
        if (toTitle) GameManager.totalScore = 0;

        //SceneManagement -> SceneManagerクラスのLoadSceneメソッド: 引数に指定したシーンに切り替えしてくれる
        SceneManager.LoadScene(sceneName);
    }

}
