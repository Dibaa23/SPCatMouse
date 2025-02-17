using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    public Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    public ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;
    public bool change;

    Player player;


    private void Start() {
        backgroundImage = GetComponent<Image>();
    }

    void Update() {
        if (Input.GetKeyDown("left"))
        {
            OnClickLeftArrow();
        }

        if (Input.GetKeyDown("right"))
        {
            OnClickRightArrow();
        }
    }


    public void SetPlayerInfo(Player _player) {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges() {
        backgroundImage.color = highlightColor;
    }

    public void OnClickLeftArrow() {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        Debug.Log("Right");
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable playerProperties)
    {
        if (player == targetPlayer) {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player) {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else {
            playerProperties["playerAvatar"] = 0;
        }
    }
}
