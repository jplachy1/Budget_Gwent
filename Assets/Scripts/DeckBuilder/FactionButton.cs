using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionButton : MonoBehaviour
{
    public GameObject captainCardsView;
    public GameObject cardScrollList;
    public GameObject CardView;

    public GameObject[] otherCaptainCardViews;
    public GameObject[] otherScrollLists;
    public GameObject[] otherCardViews;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        foreach (GameObject GO in otherCaptainCardViews)
        {
            GO.SetActive(false);
        }

        foreach (GameObject GO in otherScrollLists)
        {
            GO.SetActive(false);
        }

        foreach (GameObject GO in otherCardViews)
        {
            GO.SetActive(false);
        }

        CardView.SetActive(true);
        captainCardsView.SetActive(true);
        cardScrollList.SetActive(true);

    }
}
