using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Button button;
    public CardScrollList cardScrollList;
    [HideInInspector] public static List<Card> deck;
    SaveData save = new SaveData();
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        deck = cardScrollList.cardList;
        // Loader.Load(Loader.Scene.MainGame);
        save.Load("save.bin");
        //save.Save(deck);
    }

}
