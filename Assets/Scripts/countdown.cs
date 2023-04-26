using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countdown : MonoBehaviour
{
    public float countdownTime;
    public TMP_Text countdownDisplay;

    void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountdownStart() {
        while (countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString("0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
