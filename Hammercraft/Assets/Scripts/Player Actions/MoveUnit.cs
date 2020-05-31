using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (this.unit != null && cell.Distance(origin) <= this.unit.RangeAtq && this.unit != unit){ 
            this.unit.DealDamage(unit);
            Deselect();
        } else if(!unit.Dead && CanMove(unit) && manager.MyTurn() && unit.Player == manager.PlayerTurn) {
            this.unit = unit;
            this.origin = cell;
            
            hoverCell.ShowCells(color, UnitMovement.AvailableCells(unit.MovementMask, board, cell, unit.Movement));
            hoverCell.ShowCells(Color.yellow, UnitMovement.AvailableCells(unit.MovementMask, board, cell, unit.RangeAtq/2));
        }
    }

    private void OnSelectCell(Cell cell) {
        if (origin != null && unit != null && cell.position != unit.position) {
            if (UnitMovement.CanMove(unit.MovementMask, board, origin, cell, unit.Movement, out var path)) {
                
                origin.cellState = Cell.CellState.Free;
                cell.cellState = Cell.CellState.Occupied;
                
                unit.position = cell.position;

                manager.History.Add(new MovementAction(manager.CurrentPlayer.Role, manager.Turn, unit.Id, path.Select(c => c.position).ToArray()));
                boardManager.SubmitManager();

                Deselect();
            } else {
                Deselect();
            }
        }
    }

    private void OnHoverCell(Cell cell) {
        hoverCell.HideCells(pathColor);
        if (unit == null || origin == null) return;
        if (UnitMovement.CanMove(unit.MovementMask, board, origin, cell, unit.Movement, out var path)) {
            hoverCell.ShowCells(pathColor, path);
        }
    }

    private void Deselect() {
        unit = null;
        origin = null;
        
        hoverCell.HideCells(pathColor);
        hoverCell.HideCells(color);
        hoverCell.HideCells(Color.yellow);
    }
}
