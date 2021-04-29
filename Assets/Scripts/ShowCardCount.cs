using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class ShowCardCount : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void DisplayCardCount(int n)
    {
        text.text = n.ToString();
    }
}
