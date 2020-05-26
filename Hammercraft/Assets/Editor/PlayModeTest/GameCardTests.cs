using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;

[TestFixture]
public class GameCardTests : MonoBehaviourBaseTest
{
    [UnityTest]
    public bool GameCardUpdate_Test(){
        Board board = new Board();
        Cell cell = board.GetCell(1,1);
        GameCard gc = subject.AddComponent<GameCard>();
        CardBase a = gc.card;
        return true;
    }

}
