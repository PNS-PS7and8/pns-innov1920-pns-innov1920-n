using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class BoardTest
{
    [Test]
    public void BoardCreation_Test() {
        Board board = new Board();
        Assert.That(board.size, Is.EqualTo(new Vector2Int(50,50)));
    }

    [Test]
        public void BoardCreationParam1_Test() {
        Board board = new Board(new Vector2Int(20,20));
        Assert.That(board.size, Is.EqualTo(new Vector2Int(20,20)));
    }

    [Test]
        public void BoardCreationParam2_Test() {
        Board board = new Board(10,10);
        Assert.That(board.size, Is.EqualTo(new Vector2Int(10,10)));
    }

    [Test]
        public void HasCell1True_Test() {
        Board board = new Board(10,10);
        Assert.True(board.HasCell(new Vector2Int(1,1)));
    }

    [Test]
        public void HasCell1False_Test() {
        Board board = new Board(10,10);
        Assert.False(board.HasCell(new Vector2Int(11,13)));
    }

    [Test]
        public void HasCell2True_Test() {
        Board board = new Board(10,10);
        Assert.True(board.HasCell(1,1));
    }

    [Test]
        public void HasCell2False_Test() {
        Board board = new Board(10,10);
        Assert.False(board.HasCell(11,13));
    }

    [Test]
        public void GetCell1_Test() {
        Board board = new Board(10,10);
        Assert.True((board.GetCell(1,1)).GetType() == typeof(Cell));
    }

    [Test]
        public void GetCell2_Test() {
        Board board = new Board(10,10);
        Assert.True((board.GetCell(new Vector2Int(2,2))).GetType() == typeof(Cell));
    }

    [Test]
        public void CellToLocal1_Test() {
        Board board = new Board(10,10);
        Vector3 v = new Vector3 (0,2,0);
        Assert.That(board.CellToLocal(new Vector2Int(5,5)), Is.EqualTo(v));
    }

    [Test]
        public void CellToLocal2_Test() {
        Board board = new Board(10,10);
        Vector3 v = new Vector3 (0,2,0);
        Assert.That(board.CellToLocal(5,5), Is.EqualTo(v));
    }

    [Test]
        public void LocalToCell_Test() {
        Board board = new Board(10,10);
        Vector2Int v = new Vector2Int (5,5);
        Vector3 vEx = new Vector3 (0,2,0);
        Assert.That(board.LocalToCell(vEx), Is.EqualTo(v));
    }
}
