using UnityEngine;
using TMPro;

public class BoardUnit : MonoBehaviour {
    public Unit unit;

    private TextMeshPro text;

    private void Start() {
        text = GetComponent<TextMeshPro>();
        UpdateText();
    }

    private void Update() {
        UpdateText();
    }

    private void UpdateText() {
        text.text = string.Format("{0} / {1}", unit.Health, unit.Card.Health);
    }
}