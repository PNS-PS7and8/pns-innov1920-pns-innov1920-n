using UnityEngine;

[CreateAssetMenu]
public class SpellCard : CardBase
{
    protected override void CardEffect(Cell target)
    {
        target.unit.TakeDamage(6);
    }
}