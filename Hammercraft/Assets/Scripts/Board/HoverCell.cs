using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCell : BoardBehaviour
{
    [SerializeField]
    private GameObject hexagone;
    private Dictionary<Cell, GameObject> dictCells;
    private List<Cell> cells;

    void Start()
    {
        dictCells = new Dictionary<Cell, GameObject>();
        cells = new List<Cell>();

        foreach(Cell cell in board.Cells()) {
            GameObject goCell = Instantiate(hexagone, cell.LocalPosition+Vector3.up*0.01f, Quaternion.Euler(90, 0, 0), transform);
            goCell.SetActive(false);
            dictCells[cell] = goCell;
        }
    }

    public void ShowCells(params Cell[] cellsToShow) {
        cells = new List<Cell>(cellsToShow);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Cell cell in dictCells.Keys) {
            dictCells[cell].SetActive(cells.Contains(cell));
        }
    }
}
