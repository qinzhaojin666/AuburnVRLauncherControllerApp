using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{

    public bool isHost = false;
    public string packageString = "default";
    public string finalString = "Nothing Yet!";
    public int connectedClients;
    public GameObject textObject;

    bool isConnected = false;
    Text connectedText;
    PhotonView PV;
    string appToLoad;
    ExitGames.Client.Photon.Hashtable syncValues = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
        PV = GetComponent<PhotonView>();
        connectedText = textObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.connected)
        {
            finalString = (string)PhotonNetwork.room.CustomProperties["appToLoad"];
            connectedClients = PhotonNetwork.playerList.Length - 1;
            connectedText.text = "Number Of Connected Clients: " + connectedClients;
        }
    }
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!!!");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 21 }, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Joined Room!!!");
        isConnected = true;
        if (isHost == true)
        {
            StartCoroutine(SetVar());
        }
    }

    [PunRPC]

    private IEnumerator SetVar()
    {
        while (PhotonNetwork.connected)
        {
            syncValues["appToLoad"] = packageString;
            PhotonNetwork.room.SetCustomProperties(syncValues);

            yield return new WaitForSeconds(.5f);
        }

        yield break;
    }
}
