using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCell : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    private Cell _selectedCell = null;
    public Cell SelectedCell { get {return _selectedCell;} }

    // Update is called once per frame
    void Update()
    {
        if (boardClicker.ClickCell(out var cell)) {
            _selectedCell = cell;
        }
    }

    public void ResetSelectedCell() {
        _selectedCell = null;
    }
}
