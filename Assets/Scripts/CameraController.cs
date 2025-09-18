using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;//�^�[�Q�b�g�ƂȂ�v���C���[�̏��

    public float followSpeed = 5.0f;//�v���C���[�ɒǂ����X�s�[�h

    void Start()
    {
        //�v���C���[�����擾
        player = GameObject.FindGameObjectWithTag("Player");

        //�X�^�[�g�����u�Ԃ̃J�����̌��ݒn
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return;//�Q�[���I�[�o�[�̎��̃G���[���

        //�ڎw���ׂ��|�C���g
        Vector3 nextPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        Vector3 nowPos = transform.position;

        //���ݒn�_���ڎw���ׂ��n�_�܂ł̕⊮
        transform.position = Vector3.Lerp(nowPos, nextPos, followSpeed * Time.deltaTime);

    }
}
