using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CatMove : MonoBehaviourPunCallbacks
{
    public float offset;
    public GameObject boom;
    public Rigidbody2D rb2D;
    public TMP_Text countdownDisplay;
    public bool ready;
    public Camera cam;
    private float speed;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        countdownDisplay = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();
        //Cursor.visible = false;
        ready = false;
        view = GetComponent<PhotonView>();
        cam.orthographicSize = 15f;
        speed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CathyHealth>().alive && countdownDisplay.text == "GO!" && view.IsMine)
        {
            ready = true;
            rotation();
            thrust();
        }
    }


    public void rotation()
    {
        //catPos = cam.ScreenToWorldPoint(Input.mousePosition);
        //transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 5), catPos - transform.position);

        Vector2 positionOnScreen = cam.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2) cam.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + offset));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void thrust()
    {
        rb2D.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Cheese")
        {
            GameObject clone2 = Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
            clone2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
            Destroy(clone2.gameObject, 0.5f);
            Destroy(col.gameObject);

        }
    }
}
