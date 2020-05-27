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
    private Cell mouseHover = null;

    void Start()
    {
        dictCells = new Dictionary<Cell, GameObject>();
        cells = new List<Cell>();

        foreach(Cell cell in board.Cells()) {
            GameObject goCell = Instantiate(hoverCellPrefab, board.LocalPosition(cell)+Vector3.up*0.01f, Quaternion.Euler(90, 0, 0), transform);
            goCell.SetActive(false);
            dictCells[cell] = goCell;
        }
    }

    public void ShowMouseHover(Cell cell) {
        mouseHover = cell;
    }

    public void ShowCells(List<Cell> cellsToShow) {
        cells = cellsToShow;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Cell cell in dictCells.Keys) {
            if (cell == mouseHover) {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteContourCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = Color.white;
                dictCells[cell].SetActive(true);
            } else if(cells.Contains(cell)) {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteFullCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = new Color(0,1,0.7f,0.2f);
                dictCells[cell].SetActive(true);
            } else {
                dictCells[cell].GetComponent<SpriteRenderer>().sprite = spriteContourCell;
                dictCells[cell].GetComponent<SpriteRenderer>().color = Color.white;
                dictCells[cell].SetActive(false);
            }
        }
    }
}
