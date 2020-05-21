using UnityEngine;

public abstract class CardBase : ScriptableObject {
    [SerializeField] protected Sprite image = null;
    [SerializeField] protected new string name = "";
    [SerializeField, TextArea] protected string description = "";
    [SerializeField] protected int cost = 1;

    public int Cost => cost;
    public string Description => description;
    public Sprite Image => image;
    public string Name => name;

    public void Use(Cell target) {
        CardEffect(target);
    }

    protected abstract void CardEffect(Cell target);
}