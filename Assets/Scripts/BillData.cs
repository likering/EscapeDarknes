using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum;//�A�C�e���̎��ʔԍ�


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();//Rigidbody2D�R���|�[�l���g�̎擾
        rbody.bodyType = RigidbodyType2D.Static;//Rigidbody�̋�����Î~
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill++;//1���₷
            //�Y������擾�t���O���I��
            GameManager.itemsPickedState[itemNum] = true;

            //�A�C�e���擾���o

            //�P�R���C�_�[�𖳌���
            GetComponent<CircleCollider2D>().enabled = false;
            //�QRigidbody2D�̕����iDynamic�ɂ���j
            rbody.bodyType = RigidbodyType2D.Dynamic;
            //�R��ɑł��グ�i������T�̗́j
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            //�S�������g�𖕏��i�O�D�T�b��j
            Destroy(gameObject, 0.5f);
        }
    }
}
