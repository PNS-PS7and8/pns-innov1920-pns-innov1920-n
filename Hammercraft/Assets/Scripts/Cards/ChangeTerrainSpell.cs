using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Terrain")]

public class ChangeTerrainSpell : SpellCard
{
    [SerializeField] private Cell.CellType terrain;

    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        target.cellType = terrain;
    }
}