using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCaptainCard : MonoBehaviour
{
    public GameObject CaptainCardPrefab;

    public void RefreshDisplay(CaptainCard captainCard)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject CaptainCardGO = Instantiate(CaptainCardPrefab, transform);
        CaptainCardGO.GetComponent<Image>().sprite = captainCard.artwork;
        CaptainCardGO.GetComponent<CaptainCardPreview>().captainCard = captainCard;
    }
}
