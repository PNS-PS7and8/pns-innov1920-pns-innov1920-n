using System.Collections;
using UnityEngine;

public class UseCard : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;

    [SerializeField] private float growDuration = 0.2f;
    [SerializeField] private float growAmount = 1.5f;

    private GameCard card;

    private void OnEnable() {
        boardClicker.OnClickCard += OnClickCard;
        boardClicker.OnClickCell += OnClickCell;
        boardClicker.OnClickUnit += OnClickUnit;
    }

    private void OnDisable() {
        boardClicker.OnClickCard -= OnClickCard;
        boardClicker.OnClickUnit += OnClickUnit;
        boardClicker.OnClickUnit += OnClickUnit;
    }

    void OnClickCard(GameCard card)
    {
        if (this.card == card){} else {
            this.card = card;
            StartCoroutine(GrowCard(card));
        }
    }

    void OnClickCell(Cell cell) {
        if (card != null && manager.MyTurn()) {
            if (manager.CurrentPlayer.CurrentGold >= card.card.Cost) {
                if (cell.cellState == Cell.CellState.Free && cell.cellType == Cell.CellType.Field && card.card.GetType().ToString() == "UnitCard"){
                    manager.CurrentPlayer.SetCurrentGold(manager.CurrentPlayer.CurrentGold - card.card.Cost);
                    card.Use(cell);
                } else if (card.card.GetType().ToString() != "UnitCard"){
                    manager.CurrentPlayer.SetCurrentGold(manager.CurrentPlayer.CurrentGold - card.card.Cost);
                    card.Use(cell);
                }
            }
        }
        card = null;
    }

    void OnClickUnit(Cell cell, Unit unit) {
        if (card != null)
            if (card.card.GetType().IsAssignableFrom(typeof(SpellCard)))
                OnClickCell(cell);
    }

    IEnumerator GrowCard(GameCard card) {
        Vector3 originalScale = card.transform.localScale;
        for(float t = 0; t < growDuration && card != null; t += Time.deltaTime) {
            card.transform.localScale = originalScale * Mathf.Lerp(1, growAmount, t / growDuration);
            yield return new WaitForEndOfFrame();
        }
        while (card == this.card)
            yield return new WaitForFixedUpdate();
        for(float t = 0; t < growDuration && card != null; t += Time.deltaTime) {
            card.transform.localScale = originalScale * Mathf.Lerp(1, growAmount, 1 - t / growDuration);
            yield return new WaitForEndOfFrame();
        }
        if (card != null)
            card.transform.localScale = originalScale;
    }
}
