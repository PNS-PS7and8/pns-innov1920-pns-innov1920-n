using UnityEngine;
using NUnit.Framework;

public class GameManagerTest {
        [Test]
    public void Serialize() {
        GameManager manager = new GameManager();
        
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.KillToWin;
        
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<SpellCard>(), SpellCard.CreateInstance<SpellCard>()
            }
        );

        manager.StartGame(setup, deck, deck);

        byte[] serialized = GameManager.Serialize(manager);
        
        Debug.Log(System.Text.Encoding.UTF8.GetString(serialized));
        Assert.That(manager, Is.EqualTo(GameManager.Deserialize(serialized)));
    }
}