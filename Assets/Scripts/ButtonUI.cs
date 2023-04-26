using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ButtonUI : MonoBehaviourPunCallbacks
{
    public GameObject manager;
    public GameObject options;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void playMouse() {
        SceneManager.LoadScene("Mouse");
    }

    public void playCat()
    {
        SceneManager.LoadScene("Cat");
    }

    public void Back() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Title");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Pause()
    {
        options.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume() {
        options.SetActive(false);
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Loading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void DisconnectPlayer() {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad() {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom) {
            yield return null;
        }
        PhotonNetwork.LoadLevel("Title");
    }

    public void OnMasterClientSwitched() {
        Debug.Log("Host Switched");
    }
}
