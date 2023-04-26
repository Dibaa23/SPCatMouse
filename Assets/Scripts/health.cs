using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class health : MonoBehaviourPunCallbacks
{
    public Camera cam;
    public GameObject Manager;
    public GameObject boom;
    public GameObject End;
    public GameObject cheesePrefab;
    public Image healthBarimg;
    public bool isHurt;
    public bool alive;
    public float HP;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        End = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        Manager = GameObject.Find("Spawner");
        healthBarimg = GameObject.Find("Canvas").transform.GetChild(5).gameObject.GetComponent<Image>();
        alive = true;
        End.SetActive(false);
        HP = 1f;
        isHurt = false;
        view = GetComponent<PhotonView>();
        if (!view.IsMine) {
            cam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) {
            GameObject cat = GameObject.FindGameObjectWithTag("Cat");
            healthBarimg = GameObject.Find("Canvas").transform.GetChild(5).gameObject.GetComponent<Image>();
            if (HP <= 0f)
            {
                GameObject clone2 = Instantiate(boom, transform.position, Quaternion.identity);
                Destroy(clone2.gameObject, 0.5f);
                clone2.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                alive = false;
                End.SetActive(true);
            }

            if (HP <= 0.25f)
            {
                healthBarimg.GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.75f);
            }

            else
            {
                healthBarimg.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.75f);
            }

            HealthFill();
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            HP -= 0.10f;
            StartCoroutine(cam.GetComponent<cameraShake>().Shaking());
        }

        if (col.gameObject.tag == "Cat")
        {
            HP -= 0.15f;
            StartCoroutine(cam.GetComponent<cameraShake>().Shaking());
        }

        if (col.gameObject.tag == "Bot" || col.gameObject.tag == "Mouse")
        {
            HP -= 0.05f;
            StartCoroutine(cam.GetComponent<cameraShake>().Shaking());
        }

        if (col.gameObject.tag == "Cheese")
        {
            if (gameObject.GetComponent<Hunger>().hunger >= 20f)
            {
                gameObject.GetComponent<Hunger>().hunger -= 20f;
            }
            else
            {
                gameObject.GetComponent<Hunger>().hunger = 0f;
            }

            if (HP <= 1f)
            {
                HP += 0.25f;
            }

            if (gameObject.GetComponent<movement>().stamina <= 10f)
            {
                gameObject.GetComponent<movement>().stamina += 2f;
            }
            GameObject clone2 = Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
            clone2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
            Destroy(clone2.gameObject, 0.5f);
            Destroy(col.gameObject);
        }
    }

    public void HealthFill()
    {
        healthBarimg.fillAmount = HP;
    }
}
