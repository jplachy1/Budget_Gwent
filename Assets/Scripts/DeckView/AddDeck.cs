using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDeck : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }
    
    public void HandleClick()
    {
        Loader.Load(Loader.Scene.DeckBuilder);
    }

}
