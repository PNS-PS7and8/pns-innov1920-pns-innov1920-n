using UnityEngine;

[CreateAssetMenu]
public class SpellCard : CardBase
{
    protected override void CardEffect(Board board, Cell target)
    {
        board.GetUnit(target).TakeDamage(6);
    }
}