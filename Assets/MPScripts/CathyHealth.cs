using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CathyHealth : MonoBehaviourPunCallbacks
{
    public Camera cam;
    public float HP;
    public bool alive;
    public GameObject End;
    public GameObject boom;
    public Image healthBarimg;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        HP = 1f;
        alive = true;
        view = GetComponent<PhotonView>();
        End = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        End.SetActive(false);
        if (!view.IsMine)
        {
            cam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) {
            GameObject mouse = GameObject.FindGameObjectWithTag("Mouse");
            if (HP <= 0f)
            {
                GameObject clone2 = Instantiate(boom, transform.position, Quaternion.identity);
                Destroy(clone2.gameObject, 0.5f);
                clone2.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                clone2.transform.localScale = new Vector3(9f, 9f, 9f);
                alive = false;
                End.SetActive(true);
            }
        }
        HealthFill();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bot" || col.gameObject.tag == "Mouse")
        {
            HP -= 0.05f;
        }

        if (col.gameObject.tag == "Cat")
        {
            HP -= 0.10f;
        }

        if (col.gameObject.tag == "Bullet")
        {
            HP -= 0.05f;
        }
    }

    public void HealthFill()
    {
        healthBarimg.fillAmount = HP;
    }
}
