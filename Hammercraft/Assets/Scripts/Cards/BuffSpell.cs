using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Buff")]
public class BuffSpell : SpellCard
{
    [SerializeField] private int atq = 0;
    [SerializeField] private int movement = 0;
    [SerializeField] private int range = 0;

    protected override void CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null){
            unit.attack = unit.attack + atq;
            unit.movement = unit.movement + movement;
            unit.rangeAtq = unit.rangeAtq + range;
        }
    }
}