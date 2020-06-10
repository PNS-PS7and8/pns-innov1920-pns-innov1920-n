using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class CellTest : MonoBehaviour
{
    public enum CellType {Water, Field, Mountain, None};

    [Test]
    public void Neighbors_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(4,4);
        Cell cellN = board.GetCell(4,3);
        List<Cell> got = board.Neighbors(cell).ToList();
        Assert.True(got.Contains(cellN));
    }

    [Test]
    public void HeightWater_Test() {
        Cell cell = new Cell();
        cell.cellType = (Cell.CellType)CellType.Water;
        float res = cell.Height;
        Assert.True(res == 1); 
    }

    [Test]
    public void HeightMountain_Test() {
        Cell cell = new Cell();
        cell.cellType = (Cell.CellType)CellType.Mountain;
        float res = cell.Height;
        Assert.True(res == 3); 
    }

    [Test]
    public void LocalPosition_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        Assert.That(board.LocalPosition(cell), Is.EqualTo(new Vector3(0,2,0)));
    }

    [Test]
    public void Distance_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        Cell cellN = board.GetCell(5,4);
        Assert.That(cell.Distance(cellN), Is.EqualTo(2f));
    }

    [Test]
    public void Ring_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        Cell cellN = board.GetCell(5,4);
        Assert.True(board.Ring(cell, 1).Contains(cellN));
    }
    
    [Test]
    public void NotRing_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        Cell cellN = board.GetCell(5,3);
        Assert.False(board.Ring(cell, 1).Contains(cellN));
    }

    [Test]
    public void Disc_Test() {
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        Cell cellN = board.GetCell(5,4);
        Assert.True(board.Disc(cell, 3).Contains(cellN));
    }

}
