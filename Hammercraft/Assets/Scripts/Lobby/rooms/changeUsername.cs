using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class changeUsername : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text username;
    [SerializeField]
    private MasterManager MasterManager;


    public void On_Click_Change_Username() {
        MasterManager.GameSettings.setNickname(username.text);
        PhotonNetwork.NickName = MasterManager.GameSettings.Nickname;
    }
}
