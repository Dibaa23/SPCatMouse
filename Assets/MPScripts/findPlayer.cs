using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayer : MonoBehaviour
{
    public GameObject manager;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Spawner");
    }


    // Update is called once per frame
    void Update()
    {
        canvas.worldCamera = manager.GetComponent<Spawner>().currPlayer.GetComponentInChildren<Camera>();
    }

}
