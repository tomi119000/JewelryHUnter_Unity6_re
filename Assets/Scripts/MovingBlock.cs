using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("移動距離/時間/間隔")]

    public float moveX = 0.0f; //X移動距離
    public float moveY = 0.0f; //Y移動距離
    public float times = 0.0f; //時間
    public float wait = 0.0f; //停止時間

    [Header("乗ってから動くフラグ")]
    public bool isMoveWhenOn = false; //乗ってから動くかどうか
    bool isCanMove = true; //動くフラグ
    Vector3 startPos; //初期位置
    Vector3 endPos; //移動位置
    bool isReverse = false; //反転フラグ
    float movep = 0; //移動補完値（進捗率）


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; //初期位置を保存
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY); //移動位置を計算

        if(isMoveWhenOn)
        {
            isCanMove = false; //乗った時に動くので最初は動かさない
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isCanMove)
        {
            float distance = Vector2.Distance(startPos, endPos);
            float ds = distance / times; //1秒あたりの移動距離
            float df = ds * Time.deltaTime; //1フレームあたりの移動距離
            movep += df/distance; //終点までの進捗率を更新（0～1.0f）

            if(isReverse)
            {
                //Lerpメソッド ：Lerp(A,B,t) AからBへtの割合で移動
                //始点、終点、進捗率（movep：0~1.0） *進捗率で滑らかに移動するようにする
                transform.position = Vector2.Lerp(endPos, startPos, movep); //逆移動
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, movep); //正移動
            }
            if (movep >= 1.0f)
            {
                movep = 0.0f;  //移動補完値をリセット
                isReverse = !isReverse; //反転
                isCanMove = false; //動かないようにする
                if(isMoveWhenOn)
                {
                    //乗ったときに動くフラグOff
                    Invoke("Move", wait); //wait秒後にMoveメソッドを呼び出す
                }
            }
        }   
    }

    public void Move()
    {
        isCanMove = true; //動くようにする
    }

    public void Stop()
    {
        isCanMove = false; //動かないようにする
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") //Player objectと接触したら
        {
            //乗ったオブジェクトを子オブジェクトにする. 
            collision.transform.SetParent(transform); 
            
            //Playerタグのついたオブジェクトが乗った
            if (isMoveWhenOn)
            { //乗った時に動くフラグON
                isCanMove = true;   //移動フラグを立てる
            }
        }
    }
　   //接触終了
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //離れたのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //移動線書く
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }

}
