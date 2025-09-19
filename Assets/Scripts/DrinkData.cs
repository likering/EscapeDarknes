using UnityEngine;

public class DrinkData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum;//ƒAƒCƒeƒ€‚Ì¯•Ê”Ô†

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player‚ÌHP‚ªÅ‘å‚È‚ç‰½‚à‚µ‚È‚¢
            if (GameManager.playerHP < 3)
            {
                GameManager.playerHP++;

            }
            //ŠY“–‚·‚é¯•Ê”Ô†‚ğæ“¾Ï‚İ
            GameManager.itemsPickedState[itemNum] = true;

            GetComponent<CircleCollider2D>().enabled = false;

            rbody.bodyType = RigidbodyType2D.Dynamic;

            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

            Destroy(gameObject, 0.5f);



        }
    }
}
