using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Unspawn Unit")]

public class UnspawnUnitSpell : SpellCard
{
    [SerializeField] private bool destroyUnit = true;
    [SerializeField] private int amount = 1;

    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null && !unit.Card.ResourcePath.Contains("Specials/"))
        {
            if (destroyUnit) {
                unit.TakeDamage(unit.Health);
            }
            for(int i=0; i<amount; i++) {
                objPlayer.addCard(unit.Card);
            }
            return true;
        }
        return false;
    }
}