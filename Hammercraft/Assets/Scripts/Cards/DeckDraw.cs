using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class DeckDraw : MonoBehaviour
{
    
    [SerializeField] private Hand hand;
    [SerializeField] private GameObject _unitCard;
    [SerializeField] private GameObject _spellCard;
    [SerializeField] private GameObject UnitDeck;
    [SerializeField] private GameObject SpellDeck;


    public void Mulligan(Tuple<List<UnitCard>, List<SpellCard>, Player> tuple)
    {
       // for
    }

    public void Draw(CardBase card = null)
    {
        Sequence DrawSequence = DOTween.Sequence();
        float x = 0;
        print(card.GetType());
        if (card.GetType() == typeof(UnitCard))
        {
            _unitCard.transform.localScale = Vector3.one;
            _unitCard.transform.localPosition = new Vector3(0, 0, 0);
            _unitCard.transform.eulerAngles = new Vector3(180, 90, 0);
            _unitCard.SetActive(true);
            DrawSequence = DOTween.Sequence();
            _unitCard.transform.SetParent(hand.gameObject.transform);
            DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(0, 0.2f, 0), 0.4f));
            DrawSequence.Join(_unitCard.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
            DrawSequence.Join(_unitCard.transform.DOScale(Vector3.one*2, 0.4f));
            if (card)
            {
                TMP_Text[] texts = _unitCard.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
                print(texts.Length);
                texts[0].text = card.Name;
                texts[1].text = card.Description;
                texts[2].text = card.Cost.ToString();
                texts[3].text = ((UnitCard) card).Attack.ToString();
                texts[4].text = ((UnitCard) card).Health.ToString();
                texts[5].text = (((UnitCard) card).Range) ? "Range" : "Melee";
            }
            StartCoroutine(wait());
            DrawSequence.AppendInterval(1);
            if(hand.player != null)
            {
               x = (hand.player.Hand.Count-hand.player.Hand.Count / 2f)*hand.Spacing;
            }
            DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(x,0.2f, 0f), 0.2f));
            DrawSequence.Append(_unitCard.transform.DOLocalMove(new Vector3(x, 0f, 0f), 0.2f));
            DrawSequence.Join(_unitCard.transform.DOLocalRotate(new Vector3(-90f, 0, 0), 0.2f));
            DrawSequence.Join(_unitCard.transform.DOScale(Vector3.one, 0.2f));

            DOTween.Play(DrawSequence);
            
           

        } else
        {

            _spellCard.transform.SetParent(SpellDeck.transform);
            _spellCard.transform.localScale = Vector3.one;
            _spellCard.transform.localPosition = new Vector3(0, 0, 0);
            _spellCard.transform.eulerAngles = new Vector3(180, 90, 0);
            _spellCard.SetActive(true);
            DrawSequence = DOTween.Sequence();
            _spellCard.transform.SetParent(hand.gameObject.transform);
            DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(0, 0.2f, 0), 0.4f));
            DrawSequence.Join(_spellCard.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
            DrawSequence.Join(_spellCard.transform.DOScale(Vector3.one * 2, 0.4f));
            if (card)
            {
                TMP_Text[] texts = _spellCard.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
                texts[0].text = card.Name;
                texts[1].text = card.Description;
                texts[2].text = card.Cost.ToString();
            }
            StartCoroutine(wait());
            DrawSequence.AppendInterval(1);
            if (hand.player != null)
            {
                x = (hand.player.Hand.Count - hand.player.Hand.Count / 2f) * hand.Spacing;
            }
            DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(x, 0.2f, 0f), 0.2f));
            DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(x, 0f, 0f), 0.2f));
            DrawSequence.Join(_spellCard.transform.DOLocalRotate(new Vector3(-90f, 0, 0), 0.2f));
            DrawSequence.Join(_spellCard.transform.DOScale(Vector3.one, 0.2f));

            DOTween.Play(DrawSequence);
        }
        

        

    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1.9f);
        hand.UpdateHand();
        _unitCard.SetActive(false);
        _spellCard.SetActive(false);
    }

}
