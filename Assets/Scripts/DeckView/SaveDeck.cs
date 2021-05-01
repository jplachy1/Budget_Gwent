using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDeck : MonoBehaviour
{
    public CardScrollList cardScrollList;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        
    }
}
