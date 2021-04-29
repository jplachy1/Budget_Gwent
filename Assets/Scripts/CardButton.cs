using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public Button buttonComponent;
    Card card;
    CardScrollList scrollList;

    void Start()
    {
        buttonComponent.onClick.AddListener (HandleClick);
    }

    public void Setup(Card currentCard, CardScrollList currentScrollList)
    {
        card = currentCard;
        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        scrollList.TransferCard(card);
    }
}
