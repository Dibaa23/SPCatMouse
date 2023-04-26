using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CatBot : MonoBehaviour
{
    public GameObject Manager;
    public GameObject[] micePlayers;
    public Rigidbody2D rb2D;
    private float speed;
    private Vector3 offset = new Vector3(0, 1.2f, 0);
    public List<GameObject> mice = new List<GameObject>();
    public TMP_Text countdownDisplay;
    private float rotateRate1;
    private float rotateRate2;
    public GameObject closestMice;
    public GameObject boom;
    public GameObject Center;
    public bool miceFound = false;
    public Transform ball;
    public GameObject bullet;
    public bool ready;
    public bool gameReady;
    private bool boundrybreach;
    public GameObject obs;
    public bool stuck;


    // Start is called before the first frame update
    void Start()
    {

        boundrybreach = false;
        stuck = false;

        speed = 16f;
        rotateRate1 = Random.Range(0.25f, 0.50f);
        rotateRate2 = Random.Range(2.5f, 5.0f);

        Center = GameObject.Find("Center");
        Manager = GameObject.Find("Spawner");
        countdownDisplay = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();

        InvokeRepeating("FindMice", 1f, 3f);
        InvokeRepeating("RotateSmall", 1f, rotateRate1);
        InvokeRepeating("RotateBig", 1f, rotateRate2);

        Invoke("FindPlayers", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        micePlayers = GameObject.FindGameObjectsWithTag("Mouse");
        if (countdownDisplay.text == "GO!")
        {
            gameReady = true;
            thrust2();

            if (closestMice == null)
            {
                FindMice();
            }

            else if (!stuck)
            {
                rotation();
            }
        }
    }

    public void avoidObstacle()
    {
    }

    public void FindPlayers()
    {
        mice.AddRange(micePlayers);
    }

    public IEnumerator CMice()
    {
        miceFound = false;
        yield return new WaitForSeconds(3f);
        miceFound = true;
    }
    public void FindMice()
    {
        GameObject closest = null;
        float distance = 10000.0f;
        Vector3 position = transform.position;
        foreach (GameObject g in mice)
        {
            Vector3 diff = g.transform.position - position;
            if (diff.sqrMagnitude < distance)
            {
                closest = g;
                distance = diff.sqrMagnitude;
            }
        }

        closestMice = closest;

    }


    public void rotation()
    {
        if (!miceFound)
        {
            FindMice();
            StartCoroutine(CMice());
        }

        Vector3 dir = closestMice.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.x - 1.5f, dir.y) * Mathf.Rad2Deg;
        angle = 90 - angle;
        if (angle > 180)
        {
            angle -= 360;
        }

        angle -= transform.rotation.z * 180;
        if (angle > 0)
        {
            transform.Rotate(0f, 0f, 2f);
        }

        if (angle < 0)
        {
            transform.Rotate(0f, 0f, -2f);
        }

        if (angle > -4 && angle < 4)
        {
            shoot();
        }
    }

    void shoot()
    {
        if (!ready)
        {
            return;
        }
        StartCoroutine(CD());
    }

    public IEnumerator CD()
    {
        ready = false;
        GameObject clone = (GameObject)Instantiate(bullet, ball.position, ball.rotation);
        yield return new WaitForSeconds(Random.Range(0.25f, 2.5f));
        ready = true;
        Destroy(clone, 2f);
    }

    void RotateSmall()
    {
        rotateRate1 = Random.Range(0.25f, 0.50f);

        if (closestMice == null)
        {
            transform.Rotate(0f, 0f, Random.Range(-22.5f, 22.5f));
        }
    }

    void RotateBig()
    {
        rotateRate2 = Random.Range(2.5f, 5.0f);

        if (closestMice == null)
        {
            transform.Rotate(0f, 0f, Random.Range(-45, 45));
        }
    }


    public void thrust2()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            boundrybreach = true;
            transform.Rotate(0f, 0f, 180);
            StartCoroutine(Normalize(Random.Range(0.5f, 2f)));
        }

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

        if (col.gameObject.tag == "Key" || col.gameObject.tag == "Obstacle")
        {
            transform.Rotate(0f, 0f, 90);
        }
    }

    IEnumerator Normalize(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        boundrybreach = false;
    }
}
