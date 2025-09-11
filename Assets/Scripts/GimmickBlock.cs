using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    [Header("落下検知距離")]
    public float length = 0.0f; //自動落下検知距離

    [Header("落下後消滅フラグ")]
    public bool isDelete = false; //落下後消滅するかどうか

    [Header("当たり判定オブジェクト")]
    public GameObject deadObj;  //死亡当たり

    bool isFell = false; //落下Flag（落下中かどうか）
    float fadeTime = 0.5f; //フェードアウトまでの時間


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody2Dの物理挙動を停止
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        //Rigidbody2Dの物理挙動を無効化(static)
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false); //自分の子についている死亡当たりを非表示

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Playerタグのついたオブジェクトを探す

        if(player != null)
        {
            //プレイヤーとの距離を計測
            float d = Vector2.Distance(transform.position, player.transform.position);
            if(length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if(rbody.bodyType == RigidbodyType2D.Static)
                {
                    //Rigidbody2Dの物理挙動を有効化
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true); //死亡当たりを表示

                }
            }
        }

        if(isFell)
        {
            //落下した
            //透明値を変更してフェードアウトさせる
            fadeTime -= Time.deltaTime;
            //SpriteRendererコンポーネントのカラーを取り出す
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime; //透明値（alpha）を変更
            GetComponent<SpriteRenderer>().color = col; //カラーを再設定する

            if (fadeTime <= 0.0f)
            {
                //透明度が0（透明）になったら消す
                Destroy(gameObject); 
            }

        }

    }

    //接触開始
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDelete)
        {
            isFell = true; //落下Flagを立てる
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
