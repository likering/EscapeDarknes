using System.Xml;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = { 0, 0, 0 }; //�e�����̔z�u�ԍ�
    public static int key1PositionNumber; //��1�̔z�u�ԍ�
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; //�A�C�e���̔z�u�ԍ�

    public GameObject[] items = new GameObject[5]; //5�̃A�C�e���v���n�u�̓���
    public GameObject room; //�h�A�̃v���n�u

    public MessageData[] messages;//�z�u�����h�A�Ɋ���U��

    public GameObject dummyDoor; //�_�~�[�̃h�A�v���n�u
    public GameObject key; //�L�[�̃v���n�u

    public static bool positioned; //����z�u���ς��ǂ���
    public static string toRoomNumber = "fromRoom1"; //Player���z�u�����ׂ��ʒu

    GameObject player; //�v���C���[�̏��

    void Awake()
    {
        //�v���C���[���̎擾
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned) //�����z�u���܂�
        {
            StartKeysPosition(); //�L�[�̏���z�u
            StartItemsPosition(); //�A�C�e���̏���z�u
            StartDoorsPosition();//�h�A�̏���z�u
            positioned = true; //����z�u�͍ς�
        }
        else//����z�u�ς݂������ꍇ�͔z�u���Č�
        {
            LoadKeysPosition();
            LoadItemsPosition();
            LoadDoorsPosition();

        }

    }

    void StartKeysPosition()
    {
        //�SKey1�̃X�|�b�g�̎擾
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        //�����_���ɔԍ����擾 (�������ȏ� �����������j
        int rand = Random.Range(1, (keySpots.Length + 1));

        //�S�X�|�b�g���`�F�b�N���ɂ���
        foreach (GameObject spots in keySpots)
        {
            //�ЂƂЂƂ�spotNum�̒��g���m�F����rand�Ɠ������`�F�b�N
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                //�L�[1�𐶐�
                Instantiate(key, spots.transform.position, Quaternion.identity);
                //�ǂ̃X�|�b�g�ԍ��ɃL�[��z�u�������L�^
                key1PositionNumber = rand;
            }
        }

        //Key2�����Key3�̑ΏۃX�|�b�g
        GameObject keySpot;
        GameObject obj; //��������Key2�A�����Key3������\��

        //Key2�X�|�b�g�̎擾
        keySpot = GameObject.FindGameObjectWithTag("KeySpot2");
        //Key�̐�����obj�ւ̊i�[
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //��������Key�̃^�C�v��key2�ɕύX
        obj.GetComponent<KeyData>().keyType = KeyType.key2;

        //Key3�X�|�b�g�̎擾
        keySpot = GameObject.FindGameObjectWithTag("KeySpot3");
        //Key�̐�����obj�ւ̊i�[
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //��������Key�̃^�C�v��key3�ɕύX
        obj.GetComponent<KeyData>().keyType = KeyType.key3;
    }

    void StartItemsPosition()
    {
        //�S���̃A�C�e���X�|�b�g���擾
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            //�����_���Ȑ����̎擾
            //���������A�C�e������U��ς݂̔ԍ�����������A�����_����������

            int rand;//�����_���Ȑ����̎擾
            bool unique;//�d�����Ă��Ȃ����̃t���O

            do
            {
                unique = true;//���Ȃ���΂��̂܂܃��[�v�𔲂���\��
                rand = Random.Range(1, (itemSpots.Length + 1));//��Ԃ���X�|�b�g���̔ԍ��������_���Ŏ擾

                //���łɃ����_���Ɏ擾�����ԍ����ǂ����̃X�|�b�g�Ƃ��Ċ��蓖�Ă��Ă��Ȃ����AdoosPositionnumber�z��̏󋵂�S�_��
                foreach (int numbers in itemsPositionNumber)
                {
                    //���o�������ƃ����_���ԍ�����v���Ă�����d�����Ă����Ƃ������ƂɂȂ�
                    if (numbers == rand)
                    {
                        unique = false;//�B��̃��j�[�N�Ȃ��̂ł͂Ȃ�
                        break;
                    }
                }
            } while (!unique);


            //�X�|�b�g�̑S�`�F�b�N�i�����_���l�ƃX�|�b�g�ԍ��̈�v�j
            //��v���Ă���΁A�����ɃA�C�e���𐶐�
            foreach (GameObject spots in itemSpots)
            {
                if (spots.GetComponent<ItemSpot>().spotNum == rand)
                {
                    GameObject obj = Instantiate(
                        items[i],
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //�ǂ̃X�|�b�g�ԍ����ǂ̃A�C�e���Ɋ��蓖�Ă��Ă���̂����L�^
                    itemsPositionNumber[i] = rand;

                    //���������A�C�e���Ɏ��ʔԍ�������U���Ă���
                    if (obj.CompareTag("Bill"))
                    {
                        obj.GetComponent<BillData>().itemNum = i;

                    }
                    else
                    {
                        obj.GetComponent<DrinkData>().itemNum = i;
                    }

                }
            }

        }
    }
    void StartDoorsPosition()
    {
        //�S�X�|�b�g�̎擾
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //�o�����i���P�`���R�̂R�̏o�����j�̕������J��Ԃ�
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {
            int rand;//�����_���Ȑ��̎󂯎M
            bool unique;//�d�����Ă��Ȃ����̃t���O
            do
            {
                unique = true;//���Ȃ���΂��̂܂܃��[�v�𔲂���\��
                rand = Random.Range(1, (roomSpots.Length + 1));//��Ԃ���X�|�b�g���̔ԍ��������_���Ŏ擾

                //���łɃ����_���Ɏ擾�����ԍ����ǂ����̃X�|�b�g�Ƃ��Ċ��蓖�Ă��Ă��Ȃ����AdoosPositionnumber�z��̏󋵂�S�_��
                foreach (int numbers in doorsPositionNumber)
                {
                    //���o�������ƃ����_���ԍ�����v���Ă�����d�����Ă����Ƃ������ƂɂȂ�
                    if (numbers == rand)
                    {
                        unique = false;//�B��̃��j�[�N�Ȃ��̂ł͂Ȃ�
                        break;
                    }
                }
            } while (!unique);

            //�S�X�|�b�g������肵��rand�Ɠ����X�|�b�g��T��
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == rand)
                {
                    //���[���𐶐�
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //���ԃX�|�b�g���I�΂ꂽ�̂�static�ϐ��ɋL�����Ă���
                    doorsPositionNumber[i] = rand;

                    DoorSetting(
                        obj,//�ΏۃI�u�W�F�N�g
                        "fromRoom" + (i + 1),//���������h�A�̎��ʖ�
                        "Room" + (i + 1),//�����̏o�����ɐG�ꂽ�Ƃ��ǂ��ɍs���̂�
                        "Main",//�s��ƂȂ�V�[����
                        false,//�h�A�̊J���̏�
                        DoorDirection.down,//���̏o�����ɖ߂������̃v���C���[�̈ʒu
                        messages[i]
                        );
                }
            }
        }
        //�_�~�[���̐���
        foreach (GameObject spots in roomSpots)
        {
            //���ɔz�u�ς݂��ǂ���
            bool macth = false;
            foreach (int doorsNum in doorsPositionNumber)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsNum)
                {
                    macth = true;//���̃X�|�b�g�ԍ��ɂ͊��ɔz�u�ς�
                    break;
                }

            }
            //�������}�b�`���Ă��Ȃ���΂���܂ł������z�u����Ă��Ȃ��Ƃ������ƂȂ̂Ń_�~�[�h�A��z�u
            if (!macth) Instantiate
                (dummyDoor,spots.transform.position, Quaternion.identity);
        }

    }
    //���������h�A�̃Z�b�e�B���O
    void DoorSetting(GameObject obj, string roomName, string nextRoomName, string sceneName, bool openedDoor, DoorDirection direction, MessageData message)
    {
        RoomData roomData = obj.GetComponent<RoomData>();
        roomData.roomName = roomName;
        //�������Ɏw�肵���I�u�W�F�N�g��RoomData�X�N���v�g�̊e�ϐ���
        //�������ȍ~�Ŏw�肵���l����
        roomData.roomName = roomName;
        roomData.nextRoomName = nextRoomName;
        roomData.nextScene = sceneName;
        roomData.openedDoor = openedDoor;
        roomData.direction = direction;
        roomData.message = message;

        roomData.DoorOpenCheck();//�h�A�̊J�󋵃t���O���݂ăh�A��\���A��\�����\�b�h

    }

    void LoadKeysPosition()
    {
        //key1�����擾��������
        if (!GameManager.keysPickedState[0])
        {
            GameObject[] keySpots =
                GameObject.FindGameObjectsWithTag
                ("KeSpot");

            //�S�X�|�b�g�����Ԃɓ_��
            foreach (GameObject Spots in keySpots)
            {
                //�L�^���Ă���X�|�b�gNO�ƈꏏ���ǂ���
                if (Spots.GetComponent<KeySpot>().
                    spotNum == key1PositionNumber)
                {
                    //key1�̍쐬
                    Instantiate(
                        key,
                        Spots.transform.position,
                        Quaternion.identity
                        );
                }
            }
        }
        //key2�����擾��������
        if (!GameManager.keysPickedState[1])
        {
            //key2�X�|�b�g�̎擾
            GameObject keySpot2 =
                GameObject.FindGameObjectWithTag
                ("KeySpot2");
            //key�̐���
            GameObject obj = Instantiate(
                key,
                keySpot2.transform.position,
                Quaternion.identity
                );
            obj.GetComponent<KeyData>().keyType = KeyType.key2;
        }
        //��������key�̃^�C�v��ς��Ă���
        if (!GameManager.keysPickedState[2])
        {
            //key3�����擾��������
            GameObject keySpot3 =
                GameObject.FindGameObjectWithTag
                ("KeySpot3");
            //key3�X�|�b�g�̎擾
            GameObject obj = Instantiate(
                key,
                keySpot3.transform.position,
                Quaternion.identity
                );
            //��������key�̃^�C�v��ς��Ă���
            obj.GetComponent<KeyData>().keyType = KeyType.key3;
        }
    }

    void LoadItemsPosition()
    {
        //�S���̃A�C�e���X�|�b�g���擾
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            if (GameManager.itemsPickedState[i])
            {

                //�X�|�b�g�̑S�`�F�b�N�i�����_���l�ƃX�|�b�g�ԍ��̈�v�j
                //��v���Ă���΁A�����ɃA�C�e���𐶐�
                foreach (GameObject spots in itemSpots)
                {
                    if (spots.GetComponent<ItemSpot>().spotNum == itemsPositionNumber[i])
                    {
                        GameObject obj = Instantiate(
                            items[i],
                            spots.transform.position,
                            Quaternion.identity
                            );

                        itemsPositionNumber[i] = itemsPositionNumber[i];

                        //���������A�C�e���Ɏ��ʔԍ�������U���Ă���
                        if (obj.CompareTag("Bill"))
                        {
                            obj.GetComponent<BillData>().itemNum = i;

                        }
                        else
                        {
                            obj.GetComponent<DrinkData>().itemNum = i;
                        }
                    }

                }
            }
        }
    }

    void LoadDoorsPosition()
    {
        //�S�X�|�b�g�̎擾
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //�o�����i���P�`���R�̂R�̏o�����j�̕������J��Ԃ�
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {

            //�S�X�|�b�g������肵�ċL�^���ꂽ�ԍ��Ɠ����X�|�b�g��T��
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsPositionNumber[i])
                {
                    //���[���𐶐�
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //���������h�A�̃Z�b�e�B���O
                    DoorSetting(
                        obj,//�ΏۃI�u�W�F�N�g
                        "fromRoom" + (i + 1),//���������h�A�̎��ʖ�
                        "Room" + (i + 1),//�����̏o�����ɐG�ꂽ�Ƃ��ǂ��ɍs���̂�
                        "Main",//�s��ƂȂ�V�[����
                        GameManager.doorsOpenedState[i],//�h�A�̊J���̏󋵂�static����ǂݎ��
                        DoorDirection.down,//���̏o�����ɖ߂������̃v���C���[�̈ʒu
                        messages[i]
                        );
                }
            }
            //�_�~�[���̐���
            foreach (GameObject spots in roomSpots)
            {
                //���ɔz�u�ς݂��ǂ���
                bool macth = false;
                foreach (int doorsNum in doorsPositionNumber)
                {
                    if (spots.GetComponent<RoomSpot>().spotNum == doorsNum)
                    {
                        macth = true;//
                        break;
                    }

                }
                if (!macth) Instantiate
                    (dummyDoor,spots.transform.position, Quaternion.identity);
            }
        }
    }
}
