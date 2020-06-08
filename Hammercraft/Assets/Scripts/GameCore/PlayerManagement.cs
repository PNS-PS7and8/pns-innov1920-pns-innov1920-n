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

    public static void RegisterLocalPlayer(PlayerRole role, Deck deck) {
        Hashtable hash = new Hashtable {
            { "role", role },
            { "deck", deck }};
        LocalPhotonPlayer().SetCustomProperties(hash);
    }

    public static Deck GetDeckLocalPlayer() {
        if (PhotonNetwork.InRoom) {
            return (Deck) LocalPhotonPlayer().CustomProperties["deck"];
        }
        return null;
    }

    public static Deck GetDeckRemotePlayer() {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            return (Deck) RemotePhotonPlayer().CustomProperties["deck"];
        }
        return null;
    }

    public static PlayerRole LocalPlayer() {
        if (PhotonNetwork.InRoom) {
            return (PlayerRole) LocalPhotonPlayer().CustomProperties["role"];
        }
        return PlayerRole.PlayerOne;
    }

    public static PlayerRole RemotePlayer() {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            return (PlayerRole) RemotePhotonPlayer().CustomProperties["role"];
        }
        return PlayerRole.PlayerTwo;
    }

    public static int LocalPlayerIndex() {
        return (int) LocalPlayer();
    }

    public static int RemotePlayerIndex() {
        return (int) RemotePlayer();
    }

    public static Photon.Realtime.Player LocalPhotonPlayer() {
        return PhotonNetwork.LocalPlayer;
    }

    public static Photon.Realtime.Player RemotePhotonPlayer() {
        return PhotonNetwork.LocalPlayer.GetNext();
    }
}