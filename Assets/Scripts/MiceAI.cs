using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MiceAI : MonoBehaviourPunCallbacks
{
    public GameObject Manager;
    public GameObject Center;
    public GameObject cheesePrefab;
    public GameObject coinPrefab;
    public List<GameObject> Cat;
    public GameObject Cheese;
    public GameObject boom;
    public Rigidbody2D rb2D;
    public ParticleSystem dust;
    private float speed;
    private float size;
    private float HP;
    private float rotateRate1;
    private float rotateRate2;
    private bool runAway;
    private bool runTo;
    private bool boundrybreach;
    public TMP_Text countdownDisplay;


    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Spawner");
        Cheese = GameObject.Find("Cheese(Clone)");
        Center = GameObject.Find("Center");
        countdownDisplay = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();

        speed = Random.Range(20f, 40f);
        size = 40f / speed;
        transform.localScale = new Vector2(size, size);
        HP =  size * 3f;

        rotateRate1 = Random.Range(0.25f, 0.50f);
        rotateRate2 = Random.Range(2.5f, 5.0f);

        runAway = false;
        runTo = false;
        boundrybreach = false;
       
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        InvokeRepeating("RotateSmall", 1f, rotateRate1);
        InvokeRepeating("RotateBig", 1f, rotateRate2);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownDisplay.text == "GO!") {
            thrust();
            Flee();
            Feed();
            Breach();
        }
        
        if (HP <= 0f)
        {
            foreach (GameObject cat in Cat)
            {
                cat.GetComponent<CatBot>().mice.Remove(gameObject);
            }
            GameObject clone2 = Instantiate(boom, transform.position, Quaternion.identity);
            clone2.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
            Destroy(clone2.gameObject, 0.5f);
            Destroy(gameObject);
        }

    }
    void RotateSmall()
    {
        if (!runAway || !runTo)
        {
            transform.Rotate(0f, 0f, Random.Range(-22.5f, 22.5f));
        }
    }

    void RotateBig()
    {
        if (!runAway || !boundrybreach || !runAway)
        {
            transform.Rotate(0f, 0f, Random.Range(-45, 45));  
        }
    }

    public void thrust()
    {
        rb2D.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        dust.Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Cat")
        {
            HP -= 2f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1924528f, 0.1924528f);
            StartCoroutine(getHurt(2f));
        }

        if (col.gameObject.tag == "Bullet")
        {
            HP -= 1f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1924528f, 0.1924528f);
            StartCoroutine(getHurt(2f));
            speed /= 2f;
            StartCoroutine(slowDown(2f));
        }

        if (col.gameObject.tag == "Cheese")
        {
            speed *= 1.5f;
            StartCoroutine(speedDown(2f));
            GameObject clone2 = Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
            clone2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
            Destroy(clone2.gameObject, 0.5f);
            Destroy(col.gameObject);
        }

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

        if (col.gameObject.tag == "Key" || col.gameObject.tag == "Obstacle")
        {
            transform.Rotate(0f, 0f, 90);
        }

        else if (col.gameObject.tag == "Bot" || col.gameObject.tag == "Mouse")
        {
            HP -= 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1924528f, 0.1924528f);
            StartCoroutine(getHurt(1f));
            transform.Rotate(0f, 0f, 90);
        }
    }

    public void Flee()
    {
        bool x = false;
        foreach (GameObject cat in Cat)
        {
            if (Vector2.Distance(cat.transform.position, transform.position) <= (Random.Range(5f, 10f)) && !runTo)
            {
                x = true;
                transform.Rotate(0f, 0f, Random.Range(0f, 1f));
                transform.position = Vector2.MoveTowards(transform.position, cat.transform.position, Time.deltaTime * ((Random.Range(-1f, -0.50f)) * speed));
            }
        }

        runAway = x;
    }

    public void Feed() {
        Cheese = GameObject.FindGameObjectWithTag("Cheese");
        if (Vector2.Distance(Cheese.transform.position, transform.position) <= (Random.Range(25f, 50f)) && !runAway)
        {
            runTo = true;
            transform.position = Vector2.MoveTowards(transform.position, Cheese.transform.position, Time.deltaTime * ((Random.Range(0.50f, 1f)) * speed));
        }

        else
        {
            runTo = false;
        }

    }

    public void Breach()
    {
        if (boundrybreach)
        {
            transform.position = Vector2.MoveTowards(transform.position, Center.transform.position, Time.deltaTime * ((Random.Range(0.05f, 0.50f)) * speed));
        }
    }

    IEnumerator Normalize(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        boundrybreach = false;
    }


    IEnumerator slowDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        speed *= 2f;
    }

    IEnumerator speedDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        speed /= 1.5f;
    }

    IEnumerator getHurt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }



}
