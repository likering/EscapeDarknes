using TMPro;
using UnityEngine;
using System.Collections;

public class TalkController : MonoBehaviour
{
    public MessageData message;//ScriptbleObjectであるクラス
    private bool isPlayerInRange;//プレイヤーが領域に入ったかどうか
    private bool isTalk;//トークが開始されたかどうか
    private GameObject canvas;//トークUIを含んだCanvasオブジェクト
    private GameObject talkPanel;//対象となるトークUIパネル
    private TextMeshProUGUI nameText;//対象となるトークUIパネルの名前
    private TextMeshProUGUI messageText;//対象となるトークUIパネルのメッセージ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        talkPanel = canvas.transform.Find("TalkPanel").gameObject;
        nameText = talkPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        messageText = talkPanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && !isTalk && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();//トークを開始

        }
    }
    //トークを開始してゲームスピードをストップさせるメソッド
    void StartConversation()
    {

        isTalk = true;//トーク中フラグを立てる
        GameManager.gameState = GameState.talk;//ステータスをtalk
        talkPanel.SetActive(true);//トークUIパネルを表示
        Time.timeScale = 0;//ゲーム進行スピードを０

        //コルーチンを発動
        StartCoroutine(TalkProcess());

    }

    //TalkProcessコルーチンを作成
    IEnumerator TalkProcess()
    {
        //対象としたScriptbleObject(変数message)が扱っている配列msgArrayの数だけ繰り返す
        for (int i = 0; i < message.msgArray.Length; i++)
        {

            nameText.text = message.msgArray[i].name;
            messageText.text = message.msgArray[i].message;

            yield return new WaitForSecondsRealtime(0.1f);//０．１秒待つ

            while (!Input.GetKeyDown(KeyCode.E)) //Eキーが押されるまで
            { 
                yield return null;//何もしない
            }
        }
        EndConversation();//トーク終了の処理

    }
    void EndConversation()
    {
        talkPanel.SetActive(false);//パネルの非表示
        GameManager.gameState = GameState.playing;//ゲームステータスをPlayingに戻す
        isTalk = false;//トーク中を解除
        Time.timeScale = 1.0f;//ゲームスピードをもとに戻す
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが領域に入ったら
        if (collision.gameObject.CompareTag("Player"))
        {
            //フラグをオン
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーが領域から出たら
        if (collision.gameObject.CompareTag("Player"))
        {
            //フラグをオフ
            isPlayerInRange = false;
        }
    }
}
