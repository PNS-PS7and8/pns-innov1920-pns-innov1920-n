using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Units/Aoe deploy")]
public class AoeDeployCard : UnitCard {
    [SerializeField] protected int aoeDmg;
    [SerializeField] protected int aoeRange;

    //Carte effectuant des dommage en zone lorsqu'elle est déployée
    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        List<Cell> cells = board.Disc(target, aoeRange).ToList();
        foreach (Cell cell in cells){
            if (board.GetUnit(cell) != null){
                board.GetUnit(cell).TakeDamage(aoeDmg);           
            }
        }
        board.AddUnit(this, target, player);
        return true;
    }
}