using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInfoBehaviour : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private bool mouseOver;

    void Start()
    {
        mouseOver = false;
    }

    void Update()
    {
        ExitCardInfoView();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    void ExitCardInfoView()
    {
        if (!mouseOver && Input.GetMouseButtonDown(0))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
