using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指名する変数
    
    //シーンを切り替える機能を持るメソッド
    public void Load()
    {
        //シーンが切り替わるときはいずれにしてもステージスコアはリセットされる
        GameManager.stageScore = 0; 
        //SceneManagerクラスのLoadSceneメソッド: 引数に指定したシーンに切り替えしてくれる
        SceneManager.LoadScene(sceneName);
    }

}
