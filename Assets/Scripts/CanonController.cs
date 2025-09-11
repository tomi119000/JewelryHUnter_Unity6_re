using System.Security.Claims;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    [Header("生成プレハブ/時間/速度/範囲")]
    public GameObject objPrefab;
    public float delayTime = 3.0f;
    public float fireSpeed = 4.0f;
    public float length = 8.0f;

    public Transform gateTransform;

    GameObject player;
    float passedTimes = 0;

    //AudioSource audioSource; 
    //public AudioClip se_Shoot; 

    //距離チェック
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length >= d)
        {
            ret = true;
        }
        return ret; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        //FindGameObjectWithTagメソッド（重い）        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            passedTimes += Time.deltaTime;

            if (CheckLength(player.transform.position))
            {
                if (passedTimes >= delayTime)
                {
                    passedTimes = 0;
                    //砲弾をプレハブから生成する
                    Vector2 pos = new Vector2(gateTransform.position.x,
                    gateTransform.position.y);

                    //引数：誰に、どこへ、回転（ここでは無回転）
                    GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

                    Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                    float angleZ = transform.localEulerAngles.z;
                    float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); //ラジアンに変換
                    float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);

                    Vector2 v = new Vector2(x, y) * fireSpeed;
                    rbody.AddForce(v, ForceMode2D.Impulse);  //AddForceに入れる型はVector2型
                }
            }
        }
    }

    //キャノンの範囲を円（Sphere）で表示
    void OnDrrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length); 
    }
}
