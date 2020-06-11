using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName="Cards/Units/Summoner")]
public class SummonerCard : UnitCard {
    [SerializeField] protected CardBase summon;
    [SerializeField] protected int number;

    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        board.AddUnit(this, target, player);
        for (int i = 0; i < number; i++){
            if(objPlayer.Hand.Count()<10) objPlayer.addCard(summon);
        }
        return true;
    }
}