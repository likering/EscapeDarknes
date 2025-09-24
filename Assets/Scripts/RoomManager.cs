using System.Xml;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = { 0, 0, 0 }; //各入口の配置番号
    public static int key1PositionNumber; //鍵1の配置番号
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; //アイテムの配置番号

    public GameObject[] items = new GameObject[5]; //5つのアイテムプレハブの内訳
    public GameObject room; //ドアのプレハブ

    public MessageData[] messages;//配置したドアに割り振る

    public GameObject dummyDoor; //ダミーのドアプレハブ
    public GameObject key; //キーのプレハブ

    public static bool positioned; //初回配置が済かどうか
    public static string toRoomNumber = "fromRoom1"; //Playerが配置されるべき位置

    GameObject player; //プレイヤーの情報

    void Awake()
    {
        //プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned) //初期配置がまだ
        {
            StartKeysPosition(); //キーの初回配置
            StartItemsPosition(); //アイテムの初回配置
            StartDoorsPosition();//ドアの初回配置
            positioned = true; //初回配置は済み
        }
        else//初回配置済みだった場合は配置を再現
        {
            LoadKeysPosition();
            LoadItemsPosition();
            LoadDoorsPosition();

        }

    }

    void StartKeysPosition()
    {
        //全Key1のスポットの取得
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        //ランダムに番号を取得 (第一引数以上 第二引数未満）
        int rand = Random.Range(1, (keySpots.Length + 1));

        //全スポットをチェックしにいく
        foreach (GameObject spots in keySpots)
        {
            //ひとつひとつspotNumの中身を確認してrandと同じかチェック
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                //キー1を生成
                Instantiate(key, spots.transform.position, Quaternion.identity);
                //どのスポット番号にキーを配置したか記録
                key1PositionNumber = rand;
            }
        }

        //Key2およびKey3の対象スポット
        GameObject keySpot;
        GameObject obj; //生成したKey2、およびKey3が入る予定

        //Key2スポットの取得
        keySpot = GameObject.FindGameObjectWithTag("KeySpot2");
        //Keyの生成とobjへの格納
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //生成したKeyのタイプをkey2に変更
        obj.GetComponent<KeyData>().keyType = KeyType.key2;

        //Key3スポットの取得
        keySpot = GameObject.FindGameObjectWithTag("KeySpot3");
        //Keyの生成とobjへの格納
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        //生成したKeyのタイプをkey3に変更
        obj.GetComponent<KeyData>().keyType = KeyType.key3;
    }

    void StartItemsPosition()
    {
        //全部のアイテムスポットを取得
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            //ランダムな数字の取得
            //※ただしアイテム割り振り済みの番号を引いたら、ランダム引き直し

            int rand;//ランダムな数字の取得
            bool unique;//重複していないかのフラグ

            do
            {
                unique = true;//問題なければそのままループを抜ける予定
                rand = Random.Range(1, (itemSpots.Length + 1));//一番からスポット数の番号をランダムで取得

                //すでにランダムに取得した番号がどこかのスポットとして割り当てられていないか、doosPositionnumber配列の状況を全点検
                foreach (int numbers in itemsPositionNumber)
                {
                    //取り出した情報とランダム番号が一致していたら重複していたということになる
                    if (numbers == rand)
                    {
                        unique = false;//唯一のユニークなものではない
                        break;
                    }
                }
            } while (!unique);


            //スポットの全チェック（ランダム値とスポット番号の一致）
            //一致していれば、そこにアイテムを生成
            foreach (GameObject spots in itemSpots)
            {
                if (spots.GetComponent<ItemSpot>().spotNum == rand)
                {
                    GameObject obj = Instantiate(
                        items[i],
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //どのスポット番号がどのアイテムに割り当てられているのかを記録
                    itemsPositionNumber[i] = rand;

                    //生成したアイテムに識別番号を割り振っていく
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
        //全スポットの取得
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //出入口（鍵１～鍵３の３つの出入口）の分だけ繰り返し
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {
            int rand;//ランダムな数の受け皿
            bool unique;//重複していないかのフラグ
            do
            {
                unique = true;//問題なければそのままループを抜ける予定
                rand = Random.Range(1, (roomSpots.Length + 1));//一番からスポット数の番号をランダムで取得

                //すでにランダムに取得した番号がどこかのスポットとして割り当てられていないか、doosPositionnumber配列の状況を全点検
                foreach (int numbers in doorsPositionNumber)
                {
                    //取り出した情報とランダム番号が一致していたら重複していたということになる
                    if (numbers == rand)
                    {
                        unique = false;//唯一のユニークなものではない
                        break;
                    }
                }
            } while (!unique);

            //全スポットを見回りしてrandと同じスポットを探す
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == rand)
                {
                    //ルームを生成
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //何番スポットが選ばれたのかstatic変数に記憶していく
                    doorsPositionNumber[i] = rand;

                    DoorSetting(
                        obj,//対象オブジェクト
                        "fromRoom" + (i + 1),//生成したドアの識別名
                        "Room" + (i + 1),//そこの出入口に触れたときどこに行くのか
                        "Main",//行先となるシーン名
                        false,//ドアの開錠の状況
                        DoorDirection.down,//この出入口に戻った時のプレイヤーの位置
                        messages[i]
                        );
                }
            }
        }
        //ダミー扉の生成
        foreach (GameObject spots in roomSpots)
        {
            //既に配置済みかどうか
            bool macth = false;
            foreach (int doorsNum in doorsPositionNumber)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsNum)
                {
                    macth = true;//そのスポット番号には既に配置済み
                    break;
                }

            }
            //数字がマッチしていなければこれまでも何も配置されていないということなのでダミードアを配置
            if (!macth) Instantiate
                (dummyDoor,spots.transform.position, Quaternion.identity);
        }

    }
    //生成したドアのセッティング
    void DoorSetting(GameObject obj, string roomName, string nextRoomName, string sceneName, bool openedDoor, DoorDirection direction, MessageData message)
    {
        RoomData roomData = obj.GetComponent<RoomData>();
        roomData.roomName = roomName;
        //第一引数に指定したオブジェクトのRoomDataスクリプトの各変数に
        //第二引数以降で指定した値を代入
        roomData.roomName = roomName;
        roomData.nextRoomName = nextRoomName;
        roomData.nextScene = sceneName;
        roomData.openedDoor = openedDoor;
        roomData.direction = direction;
        roomData.message = message;

        roomData.DoorOpenCheck();//ドアの開閉状況フラグをみてドアを表示、非表示メソッド

    }

    void LoadKeysPosition()
    {
        //key1が未取得だったら
        if (!GameManager.keysPickedState[0])
        {
            GameObject[] keySpots =
                GameObject.FindGameObjectsWithTag
                ("KeSpot");

            //全スポットを順番に点検
            foreach (GameObject Spots in keySpots)
            {
                //記録しているスポットNOと一緒かどうか
                if (Spots.GetComponent<KeySpot>().
                    spotNum == key1PositionNumber)
                {
                    //key1の作成
                    Instantiate(
                        key,
                        Spots.transform.position,
                        Quaternion.identity
                        );
                }
            }
        }
        //key2が未取得だったら
        if (!GameManager.keysPickedState[1])
        {
            //key2スポットの取得
            GameObject keySpot2 =
                GameObject.FindGameObjectWithTag
                ("KeySpot2");
            //keyの生成
            GameObject obj = Instantiate(
                key,
                keySpot2.transform.position,
                Quaternion.identity
                );
            obj.GetComponent<KeyData>().keyType = KeyType.key2;
        }
        //生成したkeyのタイプを変えておく
        if (!GameManager.keysPickedState[2])
        {
            //key3が未取得だったら
            GameObject keySpot3 =
                GameObject.FindGameObjectWithTag
                ("KeySpot3");
            //key3スポットの取得
            GameObject obj = Instantiate(
                key,
                keySpot3.transform.position,
                Quaternion.identity
                );
            //生成したkeyのタイプを変えておく
            obj.GetComponent<KeyData>().keyType = KeyType.key3;
        }
    }

    void LoadItemsPosition()
    {
        //全部のアイテムスポットを取得
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            if (GameManager.itemsPickedState[i])
            {

                //スポットの全チェック（ランダム値とスポット番号の一致）
                //一致していれば、そこにアイテムを生成
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

                        //生成したアイテムに識別番号を割り振っていく
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
        //全スポットの取得
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        //出入口（鍵１～鍵３の３つの出入口）の分だけ繰り返し
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {

            //全スポットを見回りして記録された番号と同じスポットを探す
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == doorsPositionNumber[i])
                {
                    //ルームを生成
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    //生成したドアのセッティング
                    DoorSetting(
                        obj,//対象オブジェクト
                        "fromRoom" + (i + 1),//生成したドアの識別名
                        "Room" + (i + 1),//そこの出入口に触れたときどこに行くのか
                        "Main",//行先となるシーン名
                        GameManager.doorsOpenedState[i],//ドアの開錠の状況をstaticから読み取る
                        DoorDirection.down,//この出入口に戻った時のプレイヤーの位置
                        messages[i]
                        );
                }
            }
            //ダミー扉の生成
            foreach (GameObject spots in roomSpots)
            {
                //既に配置済みかどうか
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
