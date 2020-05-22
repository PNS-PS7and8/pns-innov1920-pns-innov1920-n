using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name;
    
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        _name.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }
}
