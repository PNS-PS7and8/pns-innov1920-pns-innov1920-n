using System.Collections.Generic;
using System.Linq;
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
        go.GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = false);
        BoardUnit boardUnit;
        if (!go.transform.TryGetComponent(out boardUnit)) {
            boardUnit = go.AddComponent<BoardUnit>();
        }
        boardUnit.unitId = unit.Id;
        manager.History.Add(new AtqAction(manager.CurrentPlayer.Role, manager.Turn, unit.Id));
        manager.History.Add(new MovementAction(manager.CurrentPlayer.Role, manager.Turn, unit.Id, new Vector2Int(0,0)));
        return boardUnit;
    }
}