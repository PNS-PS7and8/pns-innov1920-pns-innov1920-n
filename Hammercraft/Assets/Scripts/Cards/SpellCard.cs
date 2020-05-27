using UnityEngine;

[CreateAssetMenu]
public class SpellCard : CardBase
{
    protected override void CardEffect(Board board, Cell target)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null)
            unit.TakeDamage(6);
    }
}