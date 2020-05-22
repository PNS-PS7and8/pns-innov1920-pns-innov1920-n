using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListing _roomListing;
    [SerializeField]
    private Transform _content;
   
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print(roomList.Count);
        foreach(RoomInfo info in roomList)
        {
            
                RoomListing listing = Instantiate(_roomListing, _content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                }
            
        }
    }

    
}
