using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardButton : MonoBehaviour, IPointerClickHandler
{
    public Button buttonComponent;
    public Card card;
    public GameObject cardInfoPrefab;
    CardScrollList scrollList;

    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShowCardDets();
        }
    }

    void ShowCardDets()
    {
        Debug.Log(card.name);
        Transform parent = GameObject.Find("Canvas").transform;
        GameObject CardDetailsObject = Instantiate(cardInfoPrefab, parent);
        CardDetailsObject.GetComponentInChildren<Text>().text = CardDetails.GetDetails(card);
        CardDetailsObject.transform.GetChild(2).GetComponent<Image>().sprite = card.largeArtwork;
    }
}
