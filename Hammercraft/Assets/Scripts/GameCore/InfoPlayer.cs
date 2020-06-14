using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//Classe affichant le nom des joueurs sur le plateau
public class InfoPlayer : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    [SerializeField] public TMP_Text text2;
    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady)
            text.text = BoardManager.RemotePhotonPlayer.NickName;
            text2.text = BoardManager.LocalPhotonPlayer.NickName;
    }
}
