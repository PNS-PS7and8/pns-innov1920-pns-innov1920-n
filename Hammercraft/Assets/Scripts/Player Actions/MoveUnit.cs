using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class MoveUnit : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Color pathColor = Color.white;

    private Cell origin;
    private Unit unit;



    private bool CanMove(Unit unit) {
        return manager.History.Find<MovementAction>(action => {
            return
                action.Turn == manager.Turn &&
                action.UnitId == unit.Id;
        }).Count == 0;
    }
    
    private void OnEnable() {
        boardClicker.OnClickUnit += OnSelectUnit;
        boardClicker.OnClickCell += OnSelectCell;
        boardClicker.OnHoverCell += OnHoverCell;
        boardClicker.OnClickCard += _ => Deselect();
    }

    private void OnDisable() {
        boardClicker.OnClickUnit -= OnSelectUnit;
        boardClicker.OnClickCell -= OnSelectCell;
        boardClicker.OnHoverCell += OnHoverCell;
        boardClicker.OnClickCard -= _ => Deselect();
    }

    void OnSelectUnit(Cell cell, Unit unit)
    {
        if(!unit.Dead && CanMove(unit) && manager.MyTurn() && unit.Player-1 == manager.CurrentPlayer.Id) {
            this.unit = unit;
            this.origin = cell;
            hoverCell.ShowCells(color, PathFinding.CellsInReach(board, cell, unit.Deplacement));
        }
    }

    private void OnSelectCell(Cell cell) {
        if (origin != null && unit != null && cell.position != unit.position) {
            if (PathFinding.ComputePath(board, origin, cell, unit.Deplacement, out var path)) {
                
                origin.cellState = Cell.CellState.Free;
                cell.cellState = Cell.CellState.Occupied;
                
                unit.position = cell.position;

                manager.History.Add(new MovementAction(manager.CurrentPlayer.Id, manager.Turn, unit.Id, path.Select(c => c.position).ToArray()));
                boardManager.SubmitManager();

                Deselect();
            }
        }
    }

    private void OnHoverCell(Cell cell) {
        if (unit == null || origin == null) return;
        if (PathFinding.ComputePath(board, origin, cell, unit.Deplacement, out var path)) {
            hoverCell.HideCells(pathColor);
            hoverCell.ShowCells(pathColor, path);
        } else {
            Deselect();
        }
    }

    private void Deselect() {
        unit = null;
        origin = null;
        
        hoverCell.HideCells(pathColor);
        hoverCell.HideCells(color);
    }
}
