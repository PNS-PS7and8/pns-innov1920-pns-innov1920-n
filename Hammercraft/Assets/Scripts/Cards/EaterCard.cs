using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Units/Eater")]
public class EaterCard : UnitCard {
    [SerializeField] protected int killRange;

    protected override void CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        EaterCard me = this;
        int cptAtq = 0;
        int cptHp = 0;
        List<Cell> cells = board.Disc(target, killRange).ToList();
        
        foreach (Cell cell in cells){
            Unit currentUnit = board.GetUnit(cell);
            if (currentUnit != null && currentUnit.Player == player){
                cptAtq += currentUnit.Attack;
                cptHp += currentUnit.Health;        
            }
        }
        me.attack += cptAtq;
        me.health += cptHp;
        board.AddUnit(me, target, player); 

        foreach (Cell cell in cells){
            Unit currentUnit = board.GetUnit(cell);
            if (currentUnit != null && currentUnit.Player == player && cell != target){
                board.GetUnit(cell).TakeDamage(999);           
            }
        }
        
    }
}