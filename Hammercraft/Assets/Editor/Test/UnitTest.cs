using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class UnitTest : MonoBehaviour
{
    [Test]
    public void UnitCellOccupied_Test(){
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        UnitCard unit = ScriptableObject.CreateInstance<UnitCard>();

        //Abandonné pour le moment !

        //Le problème vient de unit.Model
        //GameObject gameObject = Object.Instantiate(unit.Model);

        //BoardUnit boardUnit = gameObject.AddComponent<BoardUnit>();
        //Unit u = new Unit(unit, cell);
        //Assert.That(cell.cellState.ToString(), Is.EqualTo("Occupied"));
    }
}
