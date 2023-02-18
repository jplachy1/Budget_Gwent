using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowCardDetails : MonoBehaviour, IPointerClickHandler
{
    public GameObject cardInfoPrefab;
    private Card card = null;
    private CaptainCard captainCard = null;

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
        else if (gameObject.GetComponent<CaptainCardPreview>() != null)
        {
            captainCard = gameObject.GetComponent<CaptainCardPreview>().captainCard;
        }
        else if (gameObject.GetComponent<CaptainCardBehaviour>() != null)
        {
            captainCard = gameObject.GetComponent<CaptainCardBehaviour>().captainCard;
        }
        else if (gameObject.GetComponent<CaptainCardPick>() != null)
        {
            captainCard = gameObject.GetComponent<CaptainCardPick>().captainCard;
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
        if (card != null)
        {
            CardDetailsObject.GetComponentInChildren<Text>().text = CardDetails.GetDetails(card);
            CardDetailsObject.transform.GetChild(2).GetComponent<Image>().sprite = card.LargeArtwork;
        }
        else
        {
            CardDetailsObject.GetComponentInChildren<Text>().text = captainCard.description;
            CardDetailsObject.transform.GetChild(2).GetComponent<Image>().sprite = captainCard.largeArtwork;
        }
    }


}
