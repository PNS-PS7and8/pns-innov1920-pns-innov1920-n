using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Spells/Terrain")]
public class ChangeTerrainSpell : SpellCard
{
    [SerializeField] private Cell.CellType terrain;
    [SerializeField] private int range;

    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        List<Cell> cells = board.Disc(target, range).ToList();
        foreach(Cell cell in cells){
            cell.cellType = terrain;
        }
    }
}