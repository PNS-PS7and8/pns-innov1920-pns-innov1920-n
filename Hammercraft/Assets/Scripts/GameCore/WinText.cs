using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//Affichage du texte de victoire ou de défaite
public class WinText : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    public void OnWin(GameState state) {
        string txt = "";
        if (PlayersExtension.LocalPlayer() == state.Winner()){
            txt = "You win !";
        } else {
            txt = "You loose !";
        }
        text.text = "Game finished !\n"+txt; 
    }
}