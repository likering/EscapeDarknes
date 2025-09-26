using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    //�V�[����؂�ւ���@�\�����������\�b�h�쐬
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {

            SceneManager.LoadScene(sceneName);
        }
    }
}
