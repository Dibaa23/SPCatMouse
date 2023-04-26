using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coins : MonoBehaviour
{
    public GameObject manager;
    public TMP_Text cointxt;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Spawner");
        cointxt = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = manager.GetComponent<Spawner>().currPlayer;
        cointxt.text = "×" + movement.coins.ToString("0");
    }
}
