using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Damage")]

public class DamageSpell : SpellCard
{
    [SerializeField] private int damage = 0;

    protected override void CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null)
        {
            unit.TakeDamage(damage);
              
        }
    }
}