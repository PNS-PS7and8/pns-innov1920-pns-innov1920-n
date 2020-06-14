using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Spells/Terrain")]
public class ChangeTerrainSpell : SpellCard
{
    [SerializeField] private Cell.CellType terrain = Cell.CellType.None;
    [SerializeField] private int range = 0;

    //Carte de sort modifiant une ou plusieurs cellule du terrain de jeu
    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        List<Cell> cells = board.Disc(target, range).ToList();
        foreach(Cell cell in cells){
            if(board.GetUnit(cell) == null)
            cell.cellType = terrain;
        }
        return true;
    }
}