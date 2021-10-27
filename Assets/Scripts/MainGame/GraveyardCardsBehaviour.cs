using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraveyardCardsBehaviour : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private bool mouseOver;
    public GraveyardBehaviour graveyardBehaviour;

    void Update()
    {
        Exit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    private void Exit()
    {
        if (!mouseOver && Input.GetMouseButtonDown(0) && graveyardBehaviour.swap && !GameHandler.showingCardDetails)
        {
            gameObject.SetActive(false);
        }
    }
}
