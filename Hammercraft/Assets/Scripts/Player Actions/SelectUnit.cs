using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    private Unit _selectedUnit = null;
    private Cell _cellUnit = null;
    public Unit SelectedUnit { get {return _selectedUnit;}}
    public Cell CellUnit { get {return _cellUnit;} }

    // Update is called once per frame
    void Update()
    {
        if (boardClicker.ClickCell(out var cell)) {
            if(_selectedUnit == null || (_selectedUnit != null && board.GetUnit(cell) != null)) {
                _selectedUnit = board.GetUnit(cell);
                _cellUnit = cell;
            }
        }
    }

    public void SetSelectedUnit(Unit unit, Cell cell) {
        _selectedUnit = unit;
        _cellUnit = cell;
    }
}
