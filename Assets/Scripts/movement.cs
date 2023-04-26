using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class movement : MonoBehaviourPunCallbacks
{
    public GameObject Manager;
    public Camera cam;
    public GameObject Fireworks;
    public static float coins = 0;
    public static float keys = 0;
    public Rigidbody2D rb2D;
    public ParticleSystem dust;
    public Image staminaBarimg;
    private float speed;
    public float NormalSpeed = 15f;
    public float stamina = 10f;
    public TMP_Text countdownDisplay;
    public bool ready;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        countdownDisplay = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();
        //Cursor.visible = false; 
        ready = false;
        Manager = GameObject.Find("Spawner");
        speed = NormalSpeed;
        view = GetComponent<PhotonView>();
        staminaBarimg = GameObject.Find("Canvas").transform.GetChild(7).gameObject.GetComponent<Image>();
        cam.orthographicSize = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<health>().alive && countdownDisplay.text == "GO!" && view.IsMine) {
            ready = true;
            staminaBarimg = GameObject.Find("Canvas").transform.GetChild(7).gameObject.GetComponent<Image>();
            rotation();
            thrust();
            StaminaFill();
        }
    }


    public void rotation() {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(new Vector3(0,0,5), mousePos - transform.position);
    }

    public void thrust() {
        rb2D.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        dust.Play();
        if (stamina > 0f && ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.UpArrow))))
        {
            speed = 400f;
            stamina -= (2 * Time.deltaTime);
        }

        else if (stamina < 10f && !((Input.GetMouseButton(0) || Input.GetKey(KeyCode.UpArrow))))
        {
            StartCoroutine("Regen", 3f);
        }

        else {

            speed = NormalSpeed;

        }
    }


    IEnumerator Regen(float duration)
    {
        speed = NormalSpeed;
        yield return new WaitForSeconds(duration);
        if (stamina < 10f && !((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) && gameObject.GetComponent<Hunger>().energy) {
            stamina += Time.deltaTime;
        }
    }

    public void StaminaFill()
    {
        staminaBarimg.fillAmount = stamina / 10;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Key")
        {
            keys++;
            GameObject clone2 = Instantiate(Fireworks, col.gameObject.transform.position, Quaternion.identity);
            Destroy(clone2.gameObject, 1f);
            Destroy(col.gameObject);
        }
    }

}
