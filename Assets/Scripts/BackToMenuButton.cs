using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoBack);
    }

    void GoBack()
    {
        Loader.Load(Loader.Scene.Menu);
    }
}

