using UnityEngine;
using TMPro;

public class InfoGold : BoardBehaviour
{
    [SerializeField] public TMP_Text text;
    private void Update() {
        text.text = manager.CurrentPlayer.CurrentGold.ToString() + " / " + manager.CurrentPlayer.Gold.ToString();
    }
}
