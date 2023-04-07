using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Timer : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public GameObject Mist;
    public GameObject Lightrays;
    public Light2D spotLight;
    public Light2D global;
    public float timeLeft;
    public float tempTime;
    public TMP_Text time;
    public TMP_Text end;
    public Volume volume;
    public bool labMap;

    // Start is called before the first frame update
    void Start()
    {
        volume.weight = 1f;
        Mist.SetActive(false);
        time = GetComponent<TMP_Text>();
        tempTime = (timeLeft * 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<health>().alive)
        {
            if (timeLeft <= 40 && timeLeft >= 39.85f && labMap)
            {

                global.intensity = 0f;
                volume.weight = 1f;
            }

            else if (timeLeft <= 39.85f && timeLeft >= 39f && labMap)
            {
                global.intensity = 1f;
                volume.weight = 0f;
            }
            else if (timeLeft <= 39f && timeLeft >= 38.85f && labMap)
            {
                global.intensity = 0f;
                volume.weight = 1f;
            } 
            else if (timeLeft <= 38.85f && timeLeft >= 38f && labMap)
            {
                global.intensity = 1f;
                volume.weight = 0f;
            }
            else if (timeLeft < 38 && labMap)
            {
                global.intensity = 0f;
                volume.weight = 1f;
            }
            if (player.GetComponent<movement>().ready) {
                timeLeft -= (Time.deltaTime);
            }

            if (volume.weight <= 1f)
            {
                volume.weight += (Time.deltaTime / (tempTime - 1f)) * .3f;
            }

            if (volume.weight >= 0.75f && spotLight.intensity >= 0f)
            {
                spotLight.intensity -= (Time.deltaTime / 10f);
            }

            else if (volume.weight >= 0.50f && !labMap)
            {
                Mist.SetActive(true);
            }
            if (volume.weight >= 0.25f)
            {
                Lightrays.SetActive(false);
            }

            time.text = ((int)(timeLeft / 60)).ToString("0") + ":" + (timeLeft % 60).ToString("00");

            if (timeLeft < 1)
            {
                time.text = "ROUND WON";
                end.text = "LEVEL PASSED";
                player.GetComponent<health>().alive = false;
            }

            if (cam.orthographicSize >= 10f)
            {
                cam.orthographicSize -= 0.2f * ((timeLeft / (1.5f * tempTime)) * Time.deltaTime);
            }
        }
        //else {
          //  gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40f, -579f);
        //    volume.weight = 1f;
          //  spotLight.intensity -= 0f;
            //Mist.SetActive(true);
         //   Lightrays.SetActive(false);
        //}
    }
}
