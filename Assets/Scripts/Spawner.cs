using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPunCallbacks
{
    public GameObject currPlayer;
    public GameObject botPrefab;
    public GameObject cheesePrefab;
    public GameObject catPrefab;
    public GameObject coinPrefab;
    public GameObject[] playerPrefabs;
    public GameObject[] forestPrefabs;
    public List<GameObject> catPrefabs;
    public float numBots;
    public float numCats;
    public float forestObstacles;
    public float numCheese;
    public float numCoins;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        currPlayer = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        currPlayer.GetComponentInChildren<Camera>().enabled = true;
        PhotonNetwork.Instantiate(currPlayer.name, new Vector2(Random.Range(-50f, 50f), Random.Range(-33f, 33f)), Quaternion.identity);

        if (photonView.IsMine) {
            spawnCats();
            spawnBots();
            spawnForestObstacles();
            InvokeRepeating("spawnCheese", 1f, 10f);
            InvokeRepeating("spawnCoins", 1f, 20f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnCats() {

        for (int i = 0; i < numCats; i++)
        {
            GameObject catClone = PhotonNetwork.Instantiate(catPrefab.name, new Vector2(Random.Range(-45f, 45f), Random.Range(-30f, 30f)), Quaternion.identity);
            catPrefabs.Add(catClone);
        }
    }


    void spawnBots() {
        for (int i = 0; i < numBots; i++)
        {
            GameObject botClone = PhotonNetwork.Instantiate(botPrefab.name, new Vector2(Random.Range(-50f, 50f), Random.Range(-33f, 33f)), Quaternion.identity);
            foreach (GameObject catPrefab in catPrefabs)
            {
                catPrefab.GetComponent<CatBot>().mice.Add(botClone);
                botClone.GetComponent<MiceAI>().Cat.Add(catPrefab);
            }
        }
    }

    public void spawnCheese()
    {
        for (int i = 0; i < numCheese; i++)
        {
            PhotonNetwork.Instantiate(cheesePrefab.name, new Vector2(Random.Range(-50f, 50f), Random.Range(-33f, 33f)), Quaternion.identity);
        }
    }

    public void spawnCoins()
    {
        for (int i = 0; i < numCoins; i++)
        {
            PhotonNetwork.Instantiate(coinPrefab.name, new Vector2(Random.Range(-50f, 50f), Random.Range(-33f, 33f)), Quaternion.identity);
        }
    }

    void spawnForestObstacles()
    {
        for (int i = 0; i < forestObstacles; i++)
        {
            PhotonNetwork.Instantiate(forestPrefabs[Random.Range(0, 13)].name, new Vector2(Random.Range(-50f, 50f), Random.Range(-33f, 33f)), Quaternion.identity);
        }
    }
}
