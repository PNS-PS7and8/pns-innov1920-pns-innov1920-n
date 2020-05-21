using TMPro;
using UnityEngine;

public class GameCard : MonoBehaviour
{
    public CardBase card = null;
    
    [SerializeField] private TextMeshPro costText = null;
    [SerializeField] private TextMeshPro nameText = null;
    [SerializeField] private TextMeshPro descriptionText = null;
    [SerializeField] private SpriteRenderer image = null;
    
    [SerializeField] private TextMeshPro attackText = null;
    [SerializeField] private TextMeshPro healthText = null;

    public BoardPlayer player { get; set; }

    private void Update() {
        costText.text = card.Cost.ToString();
        nameText.text = card.Description;
        descriptionText.text = card.Description;
        image.sprite = card.Image;
        
        if (card.GetType().IsAssignableFrom(typeof(UnitCard))) {
            attackText.text = ((UnitCard) card).Attack.ToString();
            healthText.text = ((UnitCard) card).Health.ToString();
        } else {
            attackText.text = "";
            healthText.text = "";
        }
    }

    public void Use(Cell cell) {
        card.Use(cell);
        Destroy(gameObject);
        player.RemoveCard(this);
    }
}
