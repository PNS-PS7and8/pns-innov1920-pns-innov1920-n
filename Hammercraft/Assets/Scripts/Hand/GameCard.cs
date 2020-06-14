using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

//Classe représentant l'objet carte dans la main du joueur lors d'une partie
public class GameCard : BoardBehaviour
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
            if (card.GetType().IsAssignableFrom(typeof(UnitCard)) || card.GetType().IsSubclassOf(typeof(UnitCard))) {
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

    //Utilise la carte sur la cellule donnée, et la retire de la main du joueur
    public bool Use(Cell cell) {
        ParticleSystem partic = card.particleSystem;
        if (card.Use(board, cell, manager.PlayerTurn, manager.LocalPlayer)) {
            PlayEffect(partic, cell);
            manager.LocalPlayer.UseCard(card);
            boardManager.Hand.UpdateHand();
            boardManager.SubmitManager();
            return true;
        }
        return false;
    }

    //Joue l'effet de particule sur la cellule donnée de la carte
    private void PlayEffect(ParticleSystem particle, Cell cell){
        ParticleSystem particleEff = GameObject.Instantiate(particle);
        particleEff.transform.parent = boardManager.transform;
        particleEff.transform.localPosition = board.CellToLocal(cell.position);
        particleEff.transform.localScale = new Vector3(0.005f,0.005f,0.005f);
    }

    //Grossis la carte lorsque l'on passe la souris dessus
    private void OnMouseEnter() {
        transform.DOScale(Vector3.one * scaleUp, 0.1f);
        transform.DOLocalMoveY(offsetY, 0.1f);
        transform.DOLocalMoveZ(-offsetZ, 0.1f);
        if(Int16.Parse(costText.text) <= manager.LocalPlayer.CurrentGold)
            OverCard.gameObject.SetActive(true);
        DOTween.Play(transform);
    }

    //Remet la carte à sa taille normale lorsque la souris quitte celle-ci
    private void OnMouseExit() {
        transform.DOScale(Vector3.one,0.2f);
        transform.DOLocalMoveY(0, 0.1f);
        transform.DOLocalMoveZ(0, 0.1f);
        OverCard.gameObject.SetActive(false);
        DOTween.Play(transform);
    }
}
