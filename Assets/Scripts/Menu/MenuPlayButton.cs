using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Play);
    }

    void Play()
    {
        Loader.Load(Loader.Scene.PickDecks);
    }
}
