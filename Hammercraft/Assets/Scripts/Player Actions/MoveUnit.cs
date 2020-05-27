using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private SelectUnit selectUnit = null;
    [SerializeField] private SelectCell selectCell = null;
    [SerializeField] private ShowUnitMoves unitMoves = null;

    // Update is called once per frame
    void Update()
    {
        Unit selectedUnit = selectUnit.SelectedUnit;
        Cell selectedCell = selectCell.SelectedCell;
        if(unitMoves.cellInCellsWalkable(selectedCell) && selectedUnit.Health > 0 && boardManager.Manager.History.isAvailable("move"+selectedUnit.Id) && boardManager.Manager.MyTurn()) {
            board.GetCell(selectedUnit).cellState = Cell.CellState.Free;
            selectedUnit.position = selectedCell.position;
            selectedCell.cellState = Cell.CellState.Occupied;
            boardManager.Manager.History.addHistory("move"+selectedUnit.Id);
            selectUnit.SetSelectedUnit(board.GetUnit(selectedCell), selectedCell);
            selectCell.ResetSelectedCell();
            boardManager.SubmitManager();
        }
    }
}
