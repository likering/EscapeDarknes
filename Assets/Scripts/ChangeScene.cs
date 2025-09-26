using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    //シーンを切り替える機能をもったメソッド作成
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {

            SceneManager.LoadScene(sceneName);
        }
    }
}
