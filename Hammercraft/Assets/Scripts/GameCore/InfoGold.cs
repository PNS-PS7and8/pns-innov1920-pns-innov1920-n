using UnityEngine;
using TMPro;

public class InfoGold : BoardBehaviour
{
    [SerializeField] private PlayerRole role;
    [SerializeField] public TMP_Text text;
    private void Update() {
        text.text = manager.GetPlayer(role).CurrentGold.ToString() + " / " + manager.GetPlayer(role).Gold.ToString();
    }
}
