using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDecks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandeClick);
    }

    void HandeClick()
    {
        Loader.Load(Loader.Scene.DeckView);
    }
}
