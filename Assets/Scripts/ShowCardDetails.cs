using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowCardDetails : MonoBehaviour, IPointerClickHandler
{
    public GameObject cardInfoPrefab;
    private Card card;

    void Start()
    {
        if (gameObject.GetComponent<CardButton>() != null)
        {
            card = gameObject.GetComponent<CardButton>().card;
        }
        else if (gameObject.GetComponent<CardBehaviour>() != null)
        {
            card = gameObject.GetComponent<CardBehaviour>().card;
        }
        else if (gameObject.name == "Card(Clone)")
        {
            card = gameObject.GetComponent<CardPreview>().card;
        }
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
        GameHandler.showingCardDetails = true;
        Transform parent = GameObject.Find("Canvas").transform;
        GameObject CardDetailsObject = Instantiate(cardInfoPrefab, parent);
        CardDetailsObject.GetComponentInChildren<Text>().text = CardDetails.GetDetails(card);
        CardDetailsObject.transform.GetChild(2).GetComponent<Image>().sprite = card.largeArtwork;
    }


}
