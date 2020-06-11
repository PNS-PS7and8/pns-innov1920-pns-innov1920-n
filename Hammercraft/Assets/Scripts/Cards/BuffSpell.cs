using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Buff")]
public class BuffSpell : SpellCard
{
    [SerializeField] private int atq = 0;
    [SerializeField] private int movement = 0;
    [SerializeField] private int range = 0;

    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null){
            if (unit.Card.ResourcePath.Contains("Specials/"))
                return false;
            unit.attack = unit.attack + atq;
            unit.movement = unit.movement + movement;
            unit.rangeAtq = unit.rangeAtq + range;
            return true;
        }
        return false;
    }
}