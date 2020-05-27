using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeploy : BoardBehaviour
{
    [SerializeField] private SelectCell selectCell = null;
    [SerializeField] private SelectCard selectCard = null;

    // Update is called once per frame
    void Update()
    {
        Cell selectedCell = selectCell.SelectedCell;
        GameCard selectedCard = selectCard.SelectedCard;
        if (selectedCell != null && selectedCard != null && boardManager.Manager.MyTurn() && boardManager.Manager.CurrentPlayer.CurrentGold >= selectedCard.card.Cost) {
            if (selectedCard.card.GetType().IsAssignableFrom(typeof(UnitCard))){ 
                if (selectedCell.cellType == Cell.CellType.Field &&
                    selectedCell.cellState == Cell.CellState.Free){
                    
                    selectedCard.Use(selectedCell);
                    selectedCard = null;
                    selectedCell.cellState = Cell.CellState.Occupied;
                    selectedCell = null;
                }
            }
        }
    }
}
