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
        public void BoardCreationParam_Test() {
        Board board = new Board(new Vector2Int(20,20));
        Assert.That(board.size, Is.EqualTo(new Vector2Int(20,20)));
    }
}
