using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainCardPick : MonoBehaviour
{
    [HideInInspector]
    public CaptainCard captainCard;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        transform.GetComponentInParent<CaptainCardsView>().pickedCaptainCardID = captainCard.ID;
    }
}
