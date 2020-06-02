using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckSpell : MonoBehaviour
{
    [SerializeField] private Hand hand;
    [SerializeField] private GameObject _spellCard;
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Cost;
    [SerializeField] private TMP_Text Description;
   

    public void DrawSpell(SpellCard card = null)
    {
        _spellCard.transform.SetParent(transform);
        _spellCard.transform.localScale = Vector3.one;
        _spellCard.transform.localPosition = new Vector3(0, 0, 0);
        _spellCard.transform.eulerAngles = new Vector3(180, 90, 0);
        _spellCard.SetActive(true);
        Sequence DrawSequence = DOTween.Sequence();
        _spellCard.transform.SetParent(hand.gameObject.transform);
        DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(0, 0.2f, 0), 0.4f));
        DrawSequence.Join(_spellCard.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f));
        DrawSequence.Join(_spellCard.transform.DOScale(Vector3.one*2, 0.4f));
        if (card)
        {
            Name.text = card.Name;
            Cost.text = card.Cost.ToString();
            Description.text = card.Description;
        }
        StartCoroutine(wait());
        DrawSequence.AppendInterval(1);
        float x = 0;
        if (hand.player != null)
        {
            x = (hand.player.Hand.Count - 0.5f - hand.player.Hand.Count / 2f) * hand.Spacing;
        }
        DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(x, 0.2f, 0f), 0.2f));
        DrawSequence.Append(_spellCard.transform.DOLocalMove(new Vector3(x, 0f, 0f), 0.2f));
        DrawSequence.Join(_spellCard.transform.DOLocalRotate(new Vector3(-90f, 0, 0), 0.4f));
        DrawSequence.Join(_spellCard.transform.DOScale(Vector3.one, 0.2f));
        DOTween.Play(DrawSequence);
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1.9f);
        hand.UpdateHand();
        _spellCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("down"))
        {

            DrawSpell();
        }
    }
}
