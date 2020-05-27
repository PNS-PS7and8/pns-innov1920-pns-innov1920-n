using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpell : BoardBehaviour
{
    [SerializeField] private SelectCell selectCell = null;
    [SerializeField] private SelectCard selectCard = null;

    // Update is called once per frame
    void Update()
    {
        Cell selectedCell = selectCell.SelectedCell;
        GameCard selectedCard = selectCard.SelectedCard;
        if (selectedCell != null && selectedCard != null && boardManager.Manager.MyTurn() && boardManager.Manager.CurrentPlayer.CurrentGold >= selectedCard.card.Cost) {
            if (!(selectedCard.card.GetType().ToString() == "UnitCard")){
                boardManager.Manager.CurrentPlayer.SetCurrentGold(boardManager.Manager.CurrentPlayer.CurrentGold - selectedCard.card.Cost);
                selectedCard.Use(selectedCell);
                selectedCard = null;
                selectedCell = null;
            }
        }
    }
}
