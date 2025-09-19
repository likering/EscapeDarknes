using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum;//アイテムの識別番号


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();//Rigidbody2Dコンポーネントの取得
        rbody.bodyType = RigidbodyType2D.Static;//Rigidbodyの挙動を静止
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill++;//1増やす
            //該当する取得フラグをオン
            GameManager.itemsPickedState[itemNum] = true;

            //アイテム取得演出

            //１コライダーを無効化
            GetComponent<CircleCollider2D>().enabled = false;
            //２Rigidbody2Dの復活（Dynamicにする）
            rbody.bodyType = RigidbodyType2D.Dynamic;
            //３上に打ち上げ（上向き５の力）
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            //４自分自身を抹消（０．５秒後）
            Destroy(gameObject, 0.5f);
        }
    }
}
