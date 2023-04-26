using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noRotate : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x * -1, player.transform.rotation.eulerAngles.y * -1, player.transform.rotation.eulerAngles.z * -1);
    }
}
