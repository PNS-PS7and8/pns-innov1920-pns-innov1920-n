using UnityEngine;

[CreateAssetMenu(menuName="Cards/Spells/Damage")]

public class DamageSpell : SpellCard
{
    [SerializeField] private int damage = 0;
    [SerializeField] private bool canHitSpecials = true;
    public int Damage => damage;
    public bool CanHitSpecials => canHitSpecials;

    //Carte de sort réduisant les points de vie de l'unité ciblée
    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        Unit unit = board.GetUnit(target);
        if (unit != null && (!unit.Card.ResourcePath.Contains("Specials/") || unit.Card.ResourcePath.Contains("Specials/") && CanHitSpecials))
        {
            unit.TakeDamage(damage);
            return true;
        }
        return false;
    }
}