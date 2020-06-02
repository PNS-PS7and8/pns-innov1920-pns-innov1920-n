using TMPro;
using UnityEngine;
using DG.Tweening;

public class CollectionCard : BoardBehaviour
{
    [SerializeField, Range(1f, 2f)] private float scaleUp = 1.5f;
    [SerializeField, Range(0f, 1f)] private float offsetY = 0.2f;
    [SerializeField, Range(0f, 1f)] private float offsetZ = 0.2f;
    [SerializeField] private Transform OverCard = null;
    public CardBase card = null;
    
    [SerializeField] private Transform unitModel = null;
    [SerializeField] private Transform spellModel = null;
    [SerializeField] private TMP_Text costText = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text descriptionText = null;
    [SerializeField] private TMP_Text RANGEorCACText = null;
  //  [SerializeField] private SpriteRenderer image = null;
    
    [SerializeField] private TMP_Text attackText = null;
    [SerializeField] private TMP_Text healthText = null;

    public BoardPlayer player { get; set; }

    private void Update() {
        if (card != null) {
            costText.text = card.Cost.ToString();
            nameText.text = card.Name;
            descriptionText.text = card.Description;
            //image.sprite = card.Image;
        
            if (card.GetType().IsAssignableFrom(typeof(UnitCard))) {
                unitModel.gameObject.SetActive(true);
                spellModel.gameObject.SetActive(false);
                RANGEorCACText.text = (((UnitCard) card).Range) ? "Range" : "Melee";
                attackText.text = ((UnitCard) card).Attack.ToString();
                healthText.text = ((UnitCard) card).Health.ToString();
            } else {
                unitModel.gameObject.SetActive(false);
                spellModel.gameObject.SetActive(true);
                RANGEorCACText.text = "";
                attackText.text = "";
                healthText.text = "";
            }
        } else {
            unitModel.gameObject.SetActive(false);
            spellModel.gameObject.SetActive(false);
            costText.text = "";
            nameText.text = "";
            descriptionText.text = "";
            //image.sprite = null;
            RANGEorCACText.text = "";
            attackText.text = "";
            healthText.text = "";
        }
    }

    private void OnMouseEnter() {
        //transform.DOScale(Vector3.one * scaleUp, 0.1f);
        //OverCard.gameObject.SetActive(true);
        //DOTween.Play(transform);
    }

    private void OnMouseExit() {
        //transform.DOScale(Vector3.one,0.2f);
        //OverCard.gameObject.SetActive(false);
        //DOTween.Play(transform);
    }

    private void OnMouseDown() {
        
    }
    
}
