using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;

public enum PlayerRole {
    PlayerOne = 0,
    PlayerTwo = 1,
    Spectator = 2
}

public static class PlayersExtension {
    public static PlayerRole Other(this PlayerRole player) {
        switch(player) {
            case PlayerRole.PlayerOne: return PlayerRole.PlayerTwo;
            case PlayerRole.PlayerTwo: return PlayerRole.PlayerOne;
            default: return PlayerRole.Spectator;
        }
    }

    public static PlayerRole RandomPlayer() {
        int value = Random.Range(0, 2);
        if (value == 0) {
            return PlayerRole.PlayerOne;
        } else {
            return PlayerRole.PlayerTwo;
        }
    }

    public static void RegisterLocalPlayer(PlayerRole role) {
        Hashtable hash = new Hashtable {{PhotonNetwork.LocalPlayer.UserId, (int) role}};
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public static PlayerRole LocalPlayer() {
        if (PhotonNetwork.InRoom) {
            string key = PhotonNetwork.LocalPlayer.UserId;
            Hashtable props = PhotonNetwork.CurrentRoom.CustomProperties;
            if (props.ContainsKey(key))
                return (PlayerRole) props[key];
        }
        return PlayerRole.PlayerOne;
    }

    public static PlayerRole RemotePlayer() {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            string key = PhotonNetwork.PlayerListOthers[0].UserId;
            Hashtable props = PhotonNetwork.CurrentRoom.CustomProperties;
            if (props.ContainsKey(key))
                return (PlayerRole) props[key];
        }
        return PlayerRole.PlayerTwo;
    }

    public static int LocalPlayerIndex() {
        return (int) LocalPlayer();
    }

    public static int RemotePlayerIndex() {
        return (int) RemotePlayer();
    }    
}