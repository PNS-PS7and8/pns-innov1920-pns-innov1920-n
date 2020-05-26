using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCell : BoardBehaviour
{
    [SerializeField] private GameObject hoverCellPrefab;
    [SerializeField] private Sprite spriteFullCell;
    [SerializeField] private Sprite spriteContourCell;

    private Dictionary<Cell, GameObject> dictCells;
    private List<Cell> cells;

    void Start()
    {
        dictCells = new Dictionary<Cell, GameObject>();
        cells = new List<Cell>();

        foreach(Cell cell in board.Cells()) {
            GameObject goCell = Instantiate(hoverCellPrefab, cell.LocalPosition+Vector3.up*0.01f, Quaternion.Euler(90, 0, 0), transform);
            goCell.SetActive(false);
            dictCells[cell] = goCell;
        }
    }

    public void ShowCells(List<Cell> cellsToShow) {
        cells = cellsToShow;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Cell cell in dictCells.Keys) {
            if (cell.position == cells[0].position) {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteContourCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = Color.white;
                dictCells[cell].SetActive(true);
            } else if(cells.Contains(cell)) {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteFullCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = Color.green;
                dictCells[cell].SetActive(true);
            } else {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteContourCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = Color.white;
                dictCells[cell].SetActive(false);
            }
        }
    }
}
