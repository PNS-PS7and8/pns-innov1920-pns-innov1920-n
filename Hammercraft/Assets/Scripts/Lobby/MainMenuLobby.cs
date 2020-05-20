using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLobby : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    public void hostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }
}
