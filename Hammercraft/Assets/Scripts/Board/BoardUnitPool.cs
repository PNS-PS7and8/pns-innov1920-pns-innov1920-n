using System.Collections.Generic;
using UnityEngine;

public class BoardUnitPool : BoardBehaviour {
    private Dictionary<int, BoardUnit> boardUnits;

    private void Start() {
        boardUnits = new Dictionary<int, BoardUnit>();
    }

    private void Update() {
        foreach (var unit in board.Units)
        {
            
            if (!boardUnits.ContainsKey(unit.Id)) {
                
                boardUnits.Add(unit.Id, CreateBoardUnit(unit));
            }
        }
    }

    private BoardUnit CreateBoardUnit(Unit unit) {
        GameObject go = Instantiate(unit.Card.Model, board.LocalPosition(unit), Quaternion.identity, transform);
        BoardUnit boardUnit = go.transform.GetComponent<BoardUnit>();
        boardUnit.unitId = unit.Id;
        return boardUnit;
    }
}