using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    //シーンを切り替える機能をもったメソッド作成
    public void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            //引数に指定した名前のシーンに切り替えしてくれるメソッドの呼び出し

            SceneManager.LoadScene("Boss");
        }
    }
}
