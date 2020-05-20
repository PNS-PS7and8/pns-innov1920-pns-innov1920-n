using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public UnitCard card = null;
    private List<Unit> units = new List<Unit>();

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //units.Add(new Unit(card));
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            var unit = units[0];
            unit.TakeDamage(1);
            if (unit.Health <= 0) units.RemoveAt(0);
        }
    }
}