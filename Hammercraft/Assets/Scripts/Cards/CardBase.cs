using UnityEngine;

[System.Serializable]
public abstract class CardBase : ScriptableObject{
    [SerializeField] protected string resourcePath;
    protected Sprite image = null;
    [SerializeField] protected new string name = "";
    [SerializeField, TextArea] protected string description = "";
    [SerializeField] protected int cost = 1;
    
    public string ResourcePath => resourcePath;

    public int Cost => cost;
    public string Description => description;
    public Sprite Image => image;
    public string Name => name;
    

    public void Use(Board board, Cell target, PlayerRole player) {
        CardEffect(board, target, player);
    }

    protected abstract void CardEffect(Board board, Cell target, PlayerRole player);
}