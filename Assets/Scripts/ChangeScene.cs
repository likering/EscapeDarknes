using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    //�V�[����؂�ւ���@�\�����������\�b�h�쐬
    public void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            //�����Ɏw�肵�����O�̃V�[���ɐ؂�ւ����Ă���郁�\�b�h�̌Ăяo��

            SceneManager.LoadScene("Boss");
        }
    }
}
