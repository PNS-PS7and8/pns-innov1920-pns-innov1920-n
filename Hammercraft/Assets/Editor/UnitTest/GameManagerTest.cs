using UnityEngine;
using NUnit.Framework;

public class GameManagerTest {
        [Test]
    public void Serialize() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );

        GameManager manager = new GameManager(setup, deck, deck);

        byte[] serialized = GameManager.Serialize(manager);
        
        //Debug.Log(System.Text.Encoding.UTF8.GetString(serialized));
        //Assert.That(manager, Is.EqualTo(GameManager.Deserialize(serialized)));
    }

    [Test]
    public void CanPlay() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        byte[] serialized = GameManager.Serialize(manager);
        Assert.True(manager.CanPlay(0));
    }
        [Test]
    public void CantPlay() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        byte[] serialized = GameManager.Serialize(manager);
        Assert.False(manager.CanPlay((PlayerRole)1));
    }

    [Test]
    public void NextTurn() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        byte[] serialized = GameManager.Serialize(manager);
        manager.NextTurn();
        Assert.False(manager.CanPlay(0));
    }


    [Test]
    public void GetPlayer() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        byte[] serialized = GameManager.Serialize(manager);
        Player p = new Player(deck, 0);
        Assert.That(manager.GetPlayer(0).Deck.Name, Is.EqualTo(p.Deck.Name));
    }

    [Test]
    public void MyTurn() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        byte[] serialized = GameManager.Serialize(manager);
        Assert.That(manager.MyTurn());
    }

    [Test]
    public void IncreaseTurn() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        manager.IncreaseTurn();
        Assert.That(manager.GetPlayer(0).Gold == 1);
    }

    [Test]
    public void IncreaseGold() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        manager.IncreaseTurn();
        manager.IncreaseTurn();
        manager.IncreaseGold();
        Assert.That(manager.GetPlayer(0).Gold > 1);
    }

    [Test]
    public void CurrentGold() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        manager.IncreaseTurn();
        manager.IncreaseTurn();
        manager.IncreaseGold();
        manager.GetPlayer(0).SetCurrentGold(1);
        Assert.That(manager.GetPlayer(0).CurrentGold == 1);
    }

    [Test]
    public void ResetGold() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = new Vector2Int(10,10);
        setup.gameMode = GameModes.Point;
        Deck deck = new Deck( "coucou",
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<DamageSpell>(), SpellCard.CreateInstance<DamageSpell>()
            }
        );
        GameManager manager = new GameManager(setup, deck, deck);
        manager.IncreaseTurn();
        manager.IncreaseTurn();
        manager.IncreaseGold();
        manager.GetPlayer(0).SetCurrentGold(1);
        manager.ResetGold();
        Assert.That(manager.GetPlayer(0).CurrentGold > 1);
    }
    
}