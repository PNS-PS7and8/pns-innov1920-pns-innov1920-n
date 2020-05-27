using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class InfoGold : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    private void Update() {
        text.text = boardManager.Manager.CurrentPlayer.CurrentGold.ToString();
    }
}
