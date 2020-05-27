using System.Collections.Generic;
using UnityEngine;

public class PlayerClickControls : BoardBehaviour {
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;
    private List<Cell> cellsToHover = new List<Cell>();
    private Cell selectedCell = null;
    private GameCard selectedCard = null;
    private Unit selectedUnit = null;
    private List<GameCard> gameCards;
    private Dictionary<Cell, Cell> unit_cells_walkable = new Dictionary<Cell, Cell>();
    
    [SerializeField] private Transform selectEffect = null;

    private void Start() {
        gameCards = new List<GameCard>();
    }

    private void Hover() {
        cellsToHover = new List<Cell>();
        if (boardClicker.HoverCell(out var hover)) {
            cellsToHover.Add(hover);
        }
    }

    private void Select() {
        if (boardClicker.ClickCell(out var cell)) {
            if (cell == selectedCell){
                selectedCell = null;
                selectEffect.gameObject.SetActive(false);
            } else {
                selectedCell = cell;
                selectEffect.gameObject.SetActive(true);
                selectEffect.position = board.LocalPosition(selectedCell);
            }
            if(board.GetUnit(cell) != null) {
                Show_deplacements(board.GetUnit(cell));
            } else if(unit_cells_walkable.ContainsKey(selectedCell) && selectedUnit.Health > 0 && boardManager.Manager.History.isAvailable("move"+selectedUnit.Id)) {
                board.GetCell(selectedUnit).cellState = Cell.CellState.Free;
                selectedUnit.position = selectedCell.position;
                selectedCell.cellState = Cell.CellState.Occupied;
                boardManager.Manager.History.addHistory("move"+selectedUnit.Id);
                unit_cells_walkable = new Dictionary<Cell, Cell>();
                selectedUnit = board.GetUnit(cell);
                selectedCell = null;
                selectEffect.gameObject.SetActive(false);
                boardManager.SubmitManager();
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitinfo)) {
                    if (hitinfo.transform.TryGetComponent<GameCard>(out var gameCard)) {
                        if (selectedCard != null){
                            if (selectedCard == gameCard){
                            gameCard.transform.localScale = new Vector3(3,3,3);
                            } else {
                            selectedCard.transform.localScale = new Vector3(3,3,3);
                            }
                        }
                        if (selectedCard == gameCard ) {
                            selectedCard = null;
                        } else {
                            selectedCard = gameCard;
                            gameCard.transform.localScale = new Vector3(5,5,5);
                        }
                    }
                }
            }
        }
    }

    /* Algo parcours en profondeur DFS : affichage des cases où l'unité peut se rendre */
    private void Show_deplacements(Unit unit) {
        unit_cells_walkable = new Dictionary<Cell, Cell>();
        selectedUnit = board.GetUnit(selectedCell);
        int deplacement_max = unit.Deplacement;

        /* Initialisation du dictionnaire :
        - Cell => cellules où peut aller l'unité
        - int => déplacement pour aller à ladite cellule
        */
        Dictionary<Cell, int> dict_walkable = new Dictionary<Cell, int>();
        dict_walkable.Add(selectedCell, 0);

        List<Cell> walkable = new List<Cell>();
        foreach(Cell cell in board.FreeNeighbors(selectedCell)) {
            unit_cells_walkable[cell] = selectedCell;
            dict_walkable.Add(cell, 1);
            walkable.Add(cell);
        }
        
        while(walkable.Count > 0) {
            Cell current_cell = walkable[0];
            int current_depl = dict_walkable[current_cell];

            if(current_depl+1 <= deplacement_max) {
                current_depl++;
                foreach(Cell cell in board.FreeNeighbors(current_cell)) {
                    if(!dict_walkable.ContainsKey(cell) || dict_walkable[cell] > current_depl) {
                        dict_walkable[cell] = current_depl;
                        unit_cells_walkable[cell] = current_cell;
                        walkable.Add(cell);
                    }
                }
            }

            walkable.RemoveAt(0);
        }
    }
    
    int radius = 2;
    private void Update() {
        Hover();
        Select();

        //Colorisation des cases où peuvent marcher une unité
        foreach(Cell cell in unit_cells_walkable.Keys) {
            Debug.DrawLine(board.LocalPosition(cell), board.LocalPosition(unit_cells_walkable[cell]), Color.red);
        }
        cellsToHover.AddRange(unit_cells_walkable.Keys);
        hoverCell.ShowCells(cellsToHover);

        if (Input.mouseScrollDelta.y > 0) radius+=2;
        if (Input.mouseScrollDelta.y < 0) radius = Mathf.Max(radius-2, 0);
        if (boardClicker.HoverCell(out var c)) {
            List<Vector3> verts = new List<Vector3>();
            foreach (var cell in board.Disc(c, radius))
            {
                verts.Add(board.LocalPosition(cell));
            }
            for (int i = 0; i < verts.Count-1; i++)
            {
                Debug.DrawLine(verts[i], verts[(i+1)%verts.Count], Color.white);
            }
        }

        if (selectedCell != null && selectedCard != null) {
            
            if (selectedCard.card.GetType().ToString() == "UnitCard"){ 
                if (selectedCell.cellType == Cell.CellType.Field &&
                    selectedCell.cellState == Cell.CellState.Free){
                    
                    selectedCard.Use(selectedCell);
                    selectedCard = null;
                    selectedCell.cellState = Cell.CellState.Occupied;
                    selectedCell = null;
                    selectEffect.gameObject.SetActive(false);
                }
            } else {
                    selectedCard.Use(selectedCell);
                    selectedCard = null;
                    selectedCell = null;
                    selectEffect.gameObject.SetActive(false);
            }
        }
    }
}