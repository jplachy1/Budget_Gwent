using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeckCards : MonoBehaviour
{
    public SimpleObjectPool cardObjectPool;
    List<Card> cards;

    void AddCards()
    {
        foreach (Card card in cards)
        {
            GameObject cardGO = cardObjectPool.GetObject();
            cardGO.transform.SetParent(gameObject.transform);
            cardGO.GetComponent<Image>().sprite = card.artwork;
            cardGO.GetComponent<CardPreview>().card = card;
            cardGO.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void RemoveCards()
    {
        while (transform.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            cardObjectPool.ReturnObject(toRemove);
        }
    }

    public void RefreshDisplay(List<Card> _cards)
    {
        cards = _cards;
        SetupHeight();
        RemoveCards();
        AddCards();
    }

    void SetupHeight()
    {
        int cardCount = cards.Count;
        float height = 180f;
        float contentHeight = (cardCount / 3) * height;
        Vector2 currentHeight = gameObject.GetComponent<RectTransform>().sizeDelta;
        Vector2 vectorHeight = new Vector2(currentHeight.x, contentHeight);
        gameObject.GetComponent<RectTransform>().sizeDelta = vectorHeight;
    }
}
