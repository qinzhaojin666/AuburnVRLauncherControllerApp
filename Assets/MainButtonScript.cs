using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonScript : MonoBehaviour
{

    public GameObject networkManager;
    public string appID;

    Button button;
    PhotonManager photonManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        photonManager = networkManager.GetComponent<PhotonManager>();
        button.onClick.AddListener(OnPress);
    }

    // Update is called once per frame
    void OnPress()
    {
        photonManager.packageString = appID;
    }
}
