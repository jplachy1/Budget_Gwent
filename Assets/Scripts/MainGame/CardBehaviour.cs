using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public Card card;
    public bool isMovable = true;

    void Start()
    {
        SetCardArtwork();
    }

    void Update()
    {
        ShowDamage();
    }

    void SetCardArtwork()
    {
        GameObject childImage = gameObject.transform.GetChild(0).gameObject;
        childImage.GetComponent<Image>().sprite = card.artwork;
    }

    void ShowDamage()
    {
        GameObject childText = gameObject.transform.GetChild(2).gameObject;
        childText.GetComponent<Text>().text = card.rankDmg.ToString();
    }
}