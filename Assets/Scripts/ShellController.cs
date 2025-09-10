using UnityEngine;

public class ShellController : MonoBehaviour
{
    [Header("生存時間")]
    public float deleteTime = 3.0f;  //削除するまでの時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, deleteTime); //deleteTime秒後にgameObjectを削除（ここでは3秒後）
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //何かにぶつかったら消える
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
