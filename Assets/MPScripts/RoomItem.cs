using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    LobbyManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomName(string name) {
        roomName.text = name;
    }

    public void OnClickItem() {
        manager.JoinRoom(roomName.text);
    }
}
