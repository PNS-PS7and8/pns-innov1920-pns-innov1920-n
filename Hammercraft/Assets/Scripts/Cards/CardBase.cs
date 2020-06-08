using UnityEngine;

[System.Serializable]
public abstract class CardBase : ScriptableObject{
#if UNITY_EDITOR
    [SerializeField] public string resourcePath;    
#else
    [SerializeField] protected string resourcePath;    
#endif
    protected Sprite image = null;
    [SerializeField] protected new string name = "";
    [SerializeField, TextArea] protected string description = "";
    [SerializeField] protected int cost = 1;
    [SerializeField] public CastMask castMask = 0;
    
    public string ResourcePath => resourcePath;

    public int Cost => cost;
    public string Description => description;
    public Sprite Image => image;
    public string Name => name;
    

    public bool Use(Board board, Cell target, PlayerRole player, Player objPlayer) {
        if (CardCast.CanCast(castMask, board, target)) {
            CardEffect(board, target, player, objPlayer);
            return true;
        }
        return false;
    }

    protected abstract void CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer);
}

public abstract class SpellCard : CardBase {}