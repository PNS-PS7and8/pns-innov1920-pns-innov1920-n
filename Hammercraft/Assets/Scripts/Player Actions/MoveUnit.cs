using System.Linq;
using UnityEngine;
using Photon.Pun;

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
        bool canMove = boardManager.Manager.History.Find<MovementAction>(action => boardManager.Manager.Turn == action.Turn && action.UnitId == selectedUnit.Id).Count() == 0;
        if(unitMoves.cellInCellsWalkable(selectedCell) && selectedUnit.Health > 0 && canMove && boardManager.Manager.MyTurn() 
        && selectedUnit.Id == PhotonNetwork.LocalPlayer.ActorNumber && selectCell.SelectedCell.position != selectedUnit.position) {
            board.GetCell(selectedUnit).cellState = Cell.CellState.Free;
            selectedUnit.position = selectedCell.position;
            selectedCell.cellState = Cell.CellState.Occupied;
            boardManager.Manager.History.Add(new MovementAction(boardManager.Manager.CurrentPlayer.Id, boardManager.Manager.Turn, selectedUnit.Id, selectedUnit.position, selectedCell.position));
            selectUnit.SetSelectedUnit(board.GetUnit(selectedCell), selectedCell);
            selectCell.ResetSelectedCell();
            boardManager.SubmitManager();
        }
    }
}
