using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitMoves : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private SelectUnit selectUnit = null;
    [SerializeField] private HoverCell hoverCell = null;
    private Dictionary<Cell, Cell> _unitCellsWalkable = new Dictionary<Cell, Cell>();

    // Update is called once per frame
    void Update()
    {
        Unit selectedUnit = selectUnit.SelectedUnit;
        if(selectedUnit != null) {
            Show_moves(selectedUnit);
        } else {
            hoverCell.ShowCells(new List<Cell>());
        }
    }

    public List<Cell> getItinerary(Cell departure, Cell arrival) {
        List<Cell> res = new List<Cell>();
        /*if(_unitCellsWalkable.Count != 0) {
            res.Add(arrival);
            while(_unitCellsWalkable[arrival] != departure) {
                arrival = _unitCellsWalkable[arrival];
                res.Add(arrival);
            }
            res.Add(departure);
        }*/
        return res;
    }

    public bool cellInCellsWalkable(Cell cell) {
        return cell != null && _unitCellsWalkable.ContainsKey(cell); 
    }

    /* Algo parcours en profondeur DFS : affichage des cases où l'unité peut se rendre */
    private void Show_moves(Unit unit) {
        Cell selectedCell = selectUnit.CellUnit;
        _unitCellsWalkable = new Dictionary<Cell, Cell>();
        int deplacement_max = unit.Deplacement;

        /* Initialisation du dictionnaire :
        - Cell => cellules où peut aller l'unité
        - int => déplacement pour aller à ladite cellule
        */
        Dictionary<Cell, int> dict_walkable = new Dictionary<Cell, int>();
        dict_walkable.Add(selectedCell, 0);

        List<Cell> walkable = new List<Cell>();
        foreach(Cell cell in board.FreeNeighbors(selectedCell)) {
            _unitCellsWalkable[cell] = selectedCell;
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
                        _unitCellsWalkable[cell] = current_cell;
                        walkable.Add(cell);
                    }
                }
            }

            walkable.RemoveAt(0);
        }
        hoverCell.ShowCells(new List<Cell>(_unitCellsWalkable.Keys));
    }
}
