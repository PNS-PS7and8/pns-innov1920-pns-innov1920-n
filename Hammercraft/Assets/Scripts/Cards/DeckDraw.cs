using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class DeckDraw : MonoBehaviour
{
    
    [SerializeField] private Hand hand;
    [SerializeField] private GameObject _unitCard;
    [SerializeField] private GameObject _spellCard;
    [SerializeField] private GameObject UnitDeck;
    [SerializeField] private GameObject SpellDeck;
    [SerializeField] private GameObject validate;
    [SerializeField] private BoardPlayer BoardPlayer;
    private Button ValidateButton;
    private List<CardBase> listCardMulligan = new List<CardBase>();
    private List<GameObject> cardsMulligan = new List<GameObject>();
    private List<int> toReplace = new List<int>();
    private class CardEvent : UnityEvent<GameObject> { }
    public static UnityEvent<GameObject> eventReplaceCards;


    public void Start()
    {
        if (eventReplaceCards == null)
            eventReplaceCards = new CardEvent();

        eventReplaceCards.AddListener(UpdateReplace);
        
    }

    public void UpdateReplace(GameObject card)
    {
        int cardNb = 0;
        if (cardsMulligan.Count == 3)
            cardNb = Mathf.RoundToInt(card.transform.localPosition.x * 50f / 9 + 2);
        else
            cardNb = Mathf.RoundToInt(card.transform.localPosition.x * 50f / 9 + 5f/2);
        if(toReplace.Contains(cardNb))
        {
            toReplace.Remove(cardNb);
        } else
        {
            toReplace.Add(cardNb);
        }
    }

    public void Mulligan(Tuple<List<UnitCard>, List<SpellCard>> tuple)
    {
        
        StartCoroutine(MulliganCoroutine(tuple));
    }

    private IEnumerator MulliganCoroutine(Tuple<List<UnitCard>, List<SpellCard>> v)
    {
       ValidateButton = validate.GetComponentInChildren<Button>(true);
       validate.SetActive(true);
       float x = (v.Item2.Count == 1) ? 0 : -0.5f ;
       Sequence DrawSequence = DOTween.Sequence();
       foreach(UnitCard unit in v.Item1)
       {
            yield return new WaitForSecondsRealtime(1f);
            GameObject U = Instantiate(_unitCard, UnitDeck.transform);
            cardsMulligan.Add(U);
            listCardMulligan.Add(unit);
            U.transform.localScale = Vector3.one;         
            U.transform.localPosition = new Vector3(0, 0, 0);
            U.transform.eulerAngles = new Vector3(180, 90, 0);
            U.SetActive(true);
            DrawSequence = DOTween.Sequence();
            U.transform.SetParent(hand.gameObject.transform);
            float posX = x * 0.18f - 0.18f;
            DrawSequence.Append(U.transform.DOLocalMove(new Vector3(posX, 0.3f, 0), 0.4f));
            DrawSequence.Join(U.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
            DrawSequence.Join(U.transform.DOScale(Vector3.one * 2, 0.4f));
            TMP_Text[] texts = U.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
            texts[0].text = unit.Name;
            texts[1].text = unit.Description;
            texts[2].text = unit.Cost.ToString();
            texts[3].text = ((UnitCard)unit).Attack.ToString();
            texts[4].text = ((UnitCard)unit).Health.ToString();
            texts[5].text = (((UnitCard)unit).Range) ? "Range" : "Melee";
            U.GetComponentInChildren<Canvas>().GetComponentInChildren<Collider>(true).gameObject.SetActive(true);

            x++;

       }
        foreach (SpellCard spell in v.Item2)
        {
            yield return new WaitForSecondsRealtime(1f);
            GameObject S = Instantiate(_spellCard, SpellDeck.transform);
            cardsMulligan.Add(S);
            listCardMulligan.Add(spell);
            S.transform.localScale = Vector3.one;
            S.transform.localPosition = new Vector3(0, 0, 0);
            S.transform.eulerAngles = new Vector3(180, 90, 0);
            S.SetActive(true);
            DrawSequence = DOTween.Sequence();
            S.transform.SetParent(hand.gameObject.transform);
            float posX = x * 0.18f - 0.18f;
            DrawSequence.Append(S.transform.DOLocalMove(new Vector3(posX, 0.3f, 0), 0.4f));
            DrawSequence.Join(S.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
            DrawSequence.Join(S.transform.DOScale(Vector3.one * 2, 0.4f));
            TMP_Text[] texts = S.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
            
            texts[0].text = spell.Name;
            texts[1].text = spell.Description;
            texts[2].text = spell.Cost.ToString();
            S.GetComponentInChildren<Canvas>().GetComponentInChildren<Collider>(true).gameObject.SetActive(true);
            x++;
        }
        ValidateButton.gameObject.SetActive(true);


    }

    public void Draw(CardBase card = null)
    {
        Sequence DrawSequence = DOTween.Sequence();
        float x = 0;
        
        if (card.GetType() == typeof(UnitCard))
        {
            _unitCard.transform.SetParent(UnitDeck.transform);
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

    public void Validate()
    {
        
        StartCoroutine(UpdateTextReplace());
        

    }

    private IEnumerator UpdateTextReplace()
    {
        float x = (listCardMulligan.Count == 3) ? 0 : -0.9f;
        Sequence ReplaceSequence = DOTween.Sequence();
        ValidateButton.gameObject.SetActive(false);
        List<CardBase> toAddCard = new List<CardBase>();
        List<CardBase> toRemoveCard = new List<CardBase>();
        foreach(GameObject c in cardsMulligan)
        {
            c.GetComponentInChildren<Canvas>().GetComponentInChildren<Collider>(true).gameObject.SetActive(false);
        }
        toReplace.Sort();
        foreach (int i in toReplace)
        {
            ReplaceSequence = DOTween.Sequence();
            float posX = i * 0.18f - 0.18f;
            if (listCardMulligan[i - 1].GetType() == typeof(UnitCard))
            {
                UnitCard unitReplace = BoardPlayer.manager.CurrentPlayer.Deck.DrawUnit();
       
                GameObject U = cardsMulligan[i - 1];
                toAddCard.Add(unitReplace);
                Vector3 pos = U.transform.localPosition;
                ReplaceSequence.Append(U.transform.DOLocalMove(new Vector3(0.439f, 0.2018478f, 0.1763173f), 0.4f));
                ReplaceSequence.Join(U.transform.DOLocalRotate(new Vector3(209.874f, 92.881f, 84.231f), 0.4f));
                ReplaceSequence.Join(U.transform.DOScale(Vector3.one, 0.4f));
                ReplaceSequence.Append(U.transform.DOLocalMove(pos, 0.4f));
                ReplaceSequence.Join(U.transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 0.4f));
                ReplaceSequence.Join(U.transform.DOScale(Vector3.one * 2, 0.4f));
                
                yield return new WaitForSecondsRealtime(0.5f);
                TMP_Text[] texts = U.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
                texts[0].text = unitReplace.Name;
                texts[1].text = unitReplace.Description;
                texts[2].text = unitReplace.Cost.ToString();
                texts[3].text = unitReplace.Attack.ToString();
                texts[4].text = unitReplace.Health.ToString();
                texts[5].text = (unitReplace.Range) ? "Range" : "Melee";
                DOTween.Play(ReplaceSequence);
            }
            else
            {               
                SpellCard spellReplace = BoardPlayer.manager.CurrentPlayer.Deck.DrawSpell();
                toAddCard.Add(spellReplace);
                GameObject S = cardsMulligan[i - 1];
                Vector3 pos = S.transform.localPosition;
                ReplaceSequence.Append(S.transform.DOLocalMove(new Vector3(0.393f, 0.1185548f, 0.1763173f), 0.4f));
                ReplaceSequence.Join(S.transform.DOLocalRotate(new Vector3(-29.874f, -87.119f, 264.231f), 0.4f));
                ReplaceSequence.Join(S.transform.DOScale(Vector3.one, 0.4f));
                ReplaceSequence.Append(S.transform.DOLocalMove(pos, 0.4f));
                ReplaceSequence.Join(S.transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 0.4f));
                ReplaceSequence.Join(S.transform.DOScale(Vector3.one * 2, 0.4f));
                yield return new WaitForSecondsRealtime(0.5f);
                TMP_Text[] texts = S.GetComponentInChildren<Canvas>().GetComponentsInChildren<TMP_Text>();
                texts[0].text = spellReplace.Name;
                texts[1].text = spellReplace.Description;
                texts[2].text = spellReplace.Cost.ToString();
                
                DOTween.Play(ReplaceSequence);


            }
            BoardPlayer.manager.CurrentPlayer.Deck.AddCard(listCardMulligan[i - 1]);
            toRemoveCard.Add(listCardMulligan[i - 1]);
        }
        foreach(CardBase r in toRemoveCard)
        {
            listCardMulligan.Remove(r);
        }
        foreach(CardBase a in toAddCard)
        {
            listCardMulligan.Add(a);
        }
        yield return new WaitForSecondsRealtime(2f);
        foreach (CardBase card in listCardMulligan)
        {
            BoardPlayer.manager.CurrentPlayer.addThisCard(card);
        }
        validate.gameObject.SetActive(false);
        foreach (GameObject c in cardsMulligan)
        {
            Destroy(c);
        }
        hand.UpdateHand();
    }
}
