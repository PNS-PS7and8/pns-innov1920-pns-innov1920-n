using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListing _roomListing = null;
    [SerializeField]
    private Transform _content = null;

    private Dictionary<string,RoomListing> ListGames = new Dictionary<string, RoomListing>();
   
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {     
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                
                Destroy(ListGames[info.Name].gameObject);
                ListGames.Remove(info.Name);
            } else
            {
                RoomListing listing = null;
                if (!ListGames.ContainsKey(info.Name)){

                    listing = Instantiate(_roomListing, _content);
                }
                if (listing != null || ListGames.ContainsKey(info.Name))
                {
                    
                    ListGames[info.Name] = (listing!=null) ? listing : ListGames[info.Name];
                    ListGames[info.Name].SetRoomInfo(info);
                }

            }
            
        }
    }

    
}
