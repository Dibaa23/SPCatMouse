using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject manager;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(manager.GetComponent<Spawner>().currPlayer.transform.position, transform.position) <= 15f)  
        {
            transform.position = Vector2.MoveTowards(transform.position, manager.GetComponent<Spawner>().currPlayer.transform.position, speed);
        }
    }
}
