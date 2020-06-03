using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DeckUnit : MonoBehaviour
{
    
    [SerializeField] private Hand hand;
    [SerializeField] private GameObject _unitCard;
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Cost;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Attack;
    [SerializeField] private TMP_Text Health;
    [SerializeField] private TMP_Text RangeOrCac;

    public void DrawUnit(UnitCard card = null)
    {
        
        _unitCard.transform.SetParent(transform);
        _unitCard.transform.localScale = Vector3.one;
        _unitCard.transform.localPosition = new Vector3(0, 0, 0);
        _unitCard.transform.eulerAngles = new Vector3(180, 90, 0);
        _unitCard.SetActive(true);
        Sequence DrawSequence = DOTween.Sequence();
        _unitCard.transform.SetParent(hand.gameObject.transform);
        DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(0, 0.2f, 0), 0.4f));
        DrawSequence.Join(_unitCard.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
        DrawSequence.Join(_unitCard.transform.DOScale(Vector3.one*2, 0.4f));
        
        if (card)
        {
            Name.text = card.Name;
            Cost.text = card.Cost.ToString();
            Description.text = card.Description;
            Attack.text = card.Attack.ToString();
            Health.text = card.Health.ToString();
            RangeOrCac.text = (card.Range) ? "Range" : "Melee";
        }
        StartCoroutine(wait());
        DrawSequence.AppendInterval(1);
        float x = 0;
        if(hand.player != null)
        {
           x = (hand.player.Hand.Count-hand.player.Hand.Count / 2f)*hand.Spacing;

        }
        DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(x,0.2f, 0f), 0.2f));
        DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(x, 0f, 0f), 0.2f));
        DrawSequence.Join(_unitCard.transform.DOLocalRotate(new Vector3(-90f, 0, 0), 0.2f));
        DrawSequence.Join(_unitCard.transform.DOScale(Vector3.one, 0.2f));

        DOTween.Play(DrawSequence);

    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1.9f);
        hand.UpdateHand();
        _unitCard.SetActive(false);
    }

    public void FixedUpdate()
    {
        if(Input.GetKey("up"))
        {
            
            DrawUnit();
        }
    }
}
