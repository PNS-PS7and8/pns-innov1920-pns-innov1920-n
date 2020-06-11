using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Units/Eater")]
public class EaterCard : UnitCard {
    [SerializeField] protected int killRange;

    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        base.CardEffect(board, target, player, objPlayer);
        Unit spawned = board.GetUnit(target);
        int cptAtq = 0;
        int cptHp = 0;
        List<Cell> cells = board.Disc(target, killRange).ToList();
        
        var mask = CastMask.AnyTerrain | CastMask.AnyPosition | CastMask.AnyCellOwner;
        mask &= ~CastMask.SpecialCells;

        foreach (Cell cell in cells){
            Unit currentUnit = board.GetUnit(cell);
            if (currentUnit != null && currentUnit != spawned && currentUnit.Player == player && !currentUnit.Card.ResourcePath.Contains("Specials/")){
                cptAtq += currentUnit.Attack;
                cptHp += currentUnit.Health;
                currentUnit.TakeDamage(999);
            }
        }
        Debug.Log(cptAtq);
        spawned.attack += cptAtq;
        spawned.TakeDamage(-cptHp);
        return true;
    }
}