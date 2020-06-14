using UnityEngine;
using TMPro;

//Classe permettant l'affichage des Hammercoins des joueurs
public class InfoGold : BoardBehaviour
{
    [System.Serializable] public enum LocalOrRemote {
        Local, Remote
    };

    [SerializeField] private LocalOrRemote role = LocalOrRemote.Local;
    [SerializeField] public TMP_Text text = null;
    private void Update() {
        if (role == LocalOrRemote.Local)
            text.text = manager.LocalPlayer.CurrentGold.ToString() + " / " + manager.LocalPlayer.Gold.ToString();
        else if (role == LocalOrRemote.Remote)
            text.text = manager.RemotePlayer.CurrentGold.ToString() + " / " + manager.RemotePlayer.Gold.ToString();
    }
}
