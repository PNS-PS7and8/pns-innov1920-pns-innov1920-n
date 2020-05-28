using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WinText : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    public void OnWin() {
        string txt = "";
        if (PhotonNetwork.LocalPlayer.ActorNumber-1 == boardManager.Manager.CurrentPlayer.Id){
            txt = "You win !";
        } else {
            txt = "You loose !";
        }
        text.text = "Game finished !\n"+txt; 
    }
}