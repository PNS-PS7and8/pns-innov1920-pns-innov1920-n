using UnityEngine;

[CreateAssetMenu]
public class SpellCard : CardBase
{
    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null)
            unit.TakeDamage(6);
    }
}