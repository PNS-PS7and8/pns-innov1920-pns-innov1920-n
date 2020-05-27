using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class InfoPlayer : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady)
            text.text = "Tour de "+PhotonNetwork.PlayerList[boardManager.Manager.PlayerTurn].NickName;
    }
}
