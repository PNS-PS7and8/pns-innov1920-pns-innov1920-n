using TMPro;
using UnityEngine;

public class GameCard : BoardBehaviour
{
    public CardBase card = null;
    
    [SerializeField] private TMP_Text costText = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text descriptionText = null;
    [SerializeField] private TMP_Text RANGEorCACText = null;
  //  [SerializeField] private SpriteRenderer image = null;
    
    [SerializeField] private TMP_Text attackText = null;
    [SerializeField] private TMP_Text healthText = null;

    public BoardPlayer player { get; set; }

    private void Update() {
        costText.text = card.Cost.ToString();
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        //image.sprite = card.Image;
        
        if (card.GetType().IsAssignableFrom(typeof(UnitCard))) {
            RANGEorCACText.text = (((UnitCard) card).Range) ? "Range" : "Melee";
            attackText.text = ((UnitCard) card).Attack.ToString();
            healthText.text = ((UnitCard) card).Health.ToString();
        } 
    }

    public void Use(Cell cell) {
        card.Use(board, cell, manager.PlayerTurn);
        Destroy(gameObject);
        player.RemoveCard(this);
        boardManager.SubmitManager();
    }
}
