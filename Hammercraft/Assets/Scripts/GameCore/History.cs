using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class GameAction {
    [SerializeField] protected int player;
    [SerializeField] protected int turn;

    public int Player => player;
    public int Turn => turn;

    public GameAction(int player, int turn) {
        this.player = player;
        this.turn = turn;
    }
}

public class MovementAction : GameAction {
    [SerializeField] private int unitId;
    [SerializeField] private Vector2Int[] path;

    public int UnitId => unitId;
    public Vector2Int[] Path => path;
    public Vector2Int Origin => path[0];
    public Vector2Int Destination => path[path.Length-1];

    public MovementAction(int player, int turn, int unitId, params Vector2Int[] path) : base(player, turn) {
        this.unitId = unitId;
        this.path = path;
    }
}

public class CardPlayAction : GameAction {
    [SerializeField] protected Vector2Int[] targetPositions;
    [SerializeField] protected string cardResourcePath;

    public Vector2Int[] TargetPositions => targetPositions;
    public string CardResourcePath => cardResourcePath;
    public CardBase Card => Resources.Load<CardBase>(cardResourcePath);

    public CardPlayAction(int player, int turn, string cardResourcePath, params Vector2Int[] targetPositions) : base(player, turn) {
        this.cardResourcePath = cardResourcePath;
        this.targetPositions = targetPositions;
    }

}

public class SpellCastAction : CardPlayAction {
    public SpellCard SpellCard => Resources.Load<SpellCard>(cardResourcePath);
    
    public SpellCastAction(int player, int turn, string cardResourcePath, params Vector2Int[] targetPositions) : base(player, turn, cardResourcePath, targetPositions) {}
}

public class UnitDeployAction : CardPlayAction {
    public UnitCard UnitCard => Resources.Load<UnitCard>(cardResourcePath);

    public UnitDeployAction(int player, int turn, string cardResourcePath, params Vector2Int[] targetPositions) : base(player, turn, cardResourcePath, targetPositions) {}
}

[System.Serializable]
public class GameHistory
{
    public delegate bool GameHistoryFilter(GameAction action);
    public delegate bool GameHistoryFilter<T>(T action) where T : GameAction;
    public delegate int GameHistoryOrdering(GameAction action);
    public delegate int GameHistoryOrdering<T>(T action) where T : GameAction;
    
    private List<GameAction> history;

    public GameHistory() {
        history = new List<GameAction>();
    }

    public List<GameAction> Find(GameHistoryFilter filter){
        return history.Where(action => filter(action)).ToList();
    }

    public List<GameAction> Find(GameHistoryFilter filter, GameHistoryOrdering ordering, bool reverseOrder = false) {
        return Find(filter).OrderBy(action => (reverseOrder ? -1 : 1) * ordering(action)).ToList();
    }

    public List<T> Find<T>(GameHistoryFilter<T> filter) where T : GameAction {
        return history.OfType<T>().Where(action => filter(action)).ToList();
    }

    public List<T> Find<T>(GameHistoryFilter<T> filter, GameHistoryOrdering<T> ordering, bool reverseOrder = false) where T : GameAction {
        return Find<T>(filter).OrderBy(action => (reverseOrder ? -1 : 1) * ordering(action)).ToList(); 
    }

    public void Add(GameAction action){
        history.Add(action);
    }
}
