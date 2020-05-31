using System.Collections;
using System.Linq;
using UnityEngine;

public class UseCard : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;

    [SerializeField] private float growDuration = 0.2f;
    [SerializeField] private float growAmount = 1.5f;
    private GameCard card;

    private void OnEnable() {
        boardClicker.OnClickCard += OnClickCard;
        boardClicker.OnClickCell += OnClickCell;
        boardClicker.OnClickUnit += (c, u) => OnClickCell(c);
    }

    private void OnDisable() {
        boardClicker.OnClickCard -= OnClickCard;
        boardClicker.OnClickCell -= OnClickCell;
        boardClicker.OnClickUnit -= (c, u) => OnClickCell(c);
    }

    void OnClickCard(GameCard card)
    {
        if (manager.MyTurn()) {
            if (manager.CurrentPlayer.CurrentGold >= card.card.Cost) {
                this.card = card;
                hoverCell.HideCells(Color.green);
                hoverCell.ShowCells(Color.green, 
                    board.Cells().Where(cell => CardCast.CanCast(card.card.castMask, board, cell))
                );
            }
        }
    }

    void OnClickCell(Cell cell) {
        if (card != null && manager.MyTurn()) {
            if (manager.CurrentPlayer.CurrentGold >= card.card.Cost) {
                if (card.Use(cell)) {
                    manager.CurrentPlayer.SetCurrentGold(manager.CurrentPlayer.CurrentGold - card.card.Cost);
                }
            }
        }
        hoverCell.HideCells(Color.green);
        card = null;
    }
}
