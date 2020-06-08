using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Units/Aoe deploy")]
public class AoeDeployCard : UnitCard {
    [SerializeField] protected int aoeDmg;
    [SerializeField] protected int aoeRange;

    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        
        List<Cell> cells = board.Disc(target, aoeRange).ToList();
        foreach (Cell cell in cells){
            if (board.GetUnit(cell) != null){
                board.GetUnit(cell).TakeDamage(aoeDmg);           
            }
        }
        board.AddUnit(this, target, player);
    }
}