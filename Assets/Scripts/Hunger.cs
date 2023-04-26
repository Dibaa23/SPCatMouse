using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Hunger : MonoBehaviourPunCallbacks
{
    public bool energy;
    public float hunger;
    public Image hungerBarimg;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        energy = true;
        hunger = 0f;
        view = GetComponent<PhotonView>();
        hungerBarimg = GameObject.Find("Canvas").transform.GetChild(6).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) {
            hungerBarimg = GameObject.Find("Canvas").transform.GetChild(6).gameObject.GetComponent<Image>();
            if (hunger <= 100f)
            {
                hunger += (3 * Time.deltaTime);
            }

            if (hunger <= 10f)
            {
                energy = true;
            }

            else if (hunger <= 40f)
            {
                energy = false;
                gameObject.GetComponent<health>().HP -= (0.015f * Time.deltaTime);

            }

            else if (hunger <= 70)
            {
                energy = false;
                gameObject.GetComponent<health>().HP -= (0.025f * Time.deltaTime);
                gameObject.GetComponent<movement>().stamina -= (0.25f * Time.deltaTime);
            }
            else
            {
                energy = false;
                gameObject.GetComponent<health>().HP -= (0.05f * Time.deltaTime);
                gameObject.GetComponent<movement>().stamina -= (0.5f * Time.deltaTime);
            }

            HungerFill();
        }
        
    }

    public void HungerFill()
    {
        if (gameObject.GetComponent<movement>().ready) {
            hungerBarimg.fillAmount = 1 - (hunger / 100);
        }
    }
}
