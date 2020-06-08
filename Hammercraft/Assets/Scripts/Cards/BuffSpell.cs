using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Buff")]
public class BuffSpell : SpellCard
{
    [SerializeField] private int atq;
    [SerializeField] private int movement;
    [SerializeField] private int range;

    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null){
            unit.attack = unit.attack + atq;
            unit.movement = unit.movement + movement;
            unit.rangeAtq = unit.rangeAtq + range;
        }
    }
}