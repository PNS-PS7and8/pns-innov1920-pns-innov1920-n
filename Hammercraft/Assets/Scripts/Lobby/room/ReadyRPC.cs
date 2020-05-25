using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadyRPC : MonoBehaviourPun, Photon.Pun.IPunObservable
{
    [SerializeField]
    private PhotonView readyView;

    [SerializeField]
    private TMP_Text Text;

    public bool Ready;


    public void ChangeText(string Text)
    {
        readyView.RPC("ChangeText_RPC", RpcTarget.All, Text);
    }

    [PunRPC]
    void ChangeText_RPC(string text)
    {
        if (text=="Not Ready")
        {
            Text.color = Color.red;
        } else
        {
            Text.color = Color.green;
        }
        Text.text = text;
        Ready = !Ready;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
