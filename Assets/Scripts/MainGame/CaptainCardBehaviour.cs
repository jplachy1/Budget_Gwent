using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainCardBehaviour : MonoBehaviour
{
    public bool player;
    public CaptainCard captainCard;
    public GameHandler gh;
    public GameObject CardViewerPrefab;
    public GameObject graveyardCardPrefab;

    private bool hasBeenUsed;
    
    private const string HornCardId = "NE09";

    void Start()
    {
        if (player)
        {
            captainCard = PickDecksPlayButton.playerCaptainCard;
            gameObject.GetComponent<Button>().onClick.AddListener(Play);
        }
        else
        {
            captainCard = PickDecksPlayButton.enemyCaptainCard;
        }

        hasBeenUsed = false;

        gameObject.GetComponent<Image>().sprite = captainCard.artwork;
    }

    void Play()
    {
        if (hasBeenUsed)
        {
            Debug.Log("Captain card has already been used");
            return;
        }

        if (captainCard.ID == "CM1")
        {
            Monsters1();
        }
        else if (captainCard.ID == "CM2")
        {
            Monsters2();
        }
        else if (captainCard.ID == "CM3")
        {
            Monsters3();
        }
        else if (captainCard.ID == "CM4")
        {
            Monsters4();
        }
        else if (captainCard.ID == "CNI1")
        {
            CNI1();
        }
        else if (captainCard.ID == "CNI2")
        {
            CNI2();
        }
        else if (captainCard.ID == "CNI3")
        {
            CNI3();
        }
        else if (captainCard.ID == "CNI4")
        {
            CNI4();
        }
        else if (captainCard.ID == "CNR1")
        {
            CNR1();
        }
        else if (captainCard.ID == "CNR2")
        {
            CNR2();
        }
        else if (captainCard.ID == "CNR3")
        {
            CNR3();
        }
        else if (captainCard.ID == "CNR4")
        {
            CNR4();
        }
        else if (captainCard.ID == "CS1")
        {
            CS1();
        }
        else if (captainCard.ID == "CS2")
        {
            CS2();
        }
        else if (captainCard.ID == "CS3")
        {
            CS3();
        }
        else if (captainCard.ID == "CS4")
        {
            CS4();
        }

        MarkCardAsUsed();

        gh.SwitchTurns();  
    }

    void Monsters1()
    {
        
    }

    void Monsters2()
    {

    }

    void Monsters3()
    {
        Card hornCard = DeckLoader.cardList.Find(x => x.Id == HornCardId);
        GameObject closeRank = gh.GetCurrentHornRank(Rank.Close);
        gh.SpawnCard(hornCard, closeRank, false);
    }

    void Monsters4()
    {

    }

    void CNI1()
    {
        CardHolder cardHolder = !gh.IsPlayersTurn() ? gh.playerHolder : gh.enemyHolder;
        List<int> cardIndexes = new List<int>();
        List<GameObject> cardsToShow = new List<GameObject>();
        int cardIndex;

        for (int i = 0; i < 3; i++)
        {
            do
            {
                cardIndex = Random.Range(0, cardHolder.cards.Count - 1);
            } while (cardIndexes.Contains(cardIndex));
            cardIndexes.Add(cardIndex);
        }

        GameObject cardViewer = Instantiate(CardViewerPrefab, GameObject.Find("Canvas").transform);
        foreach (int index in cardIndexes)
        {
            Card foundCard = cardHolder.cards[index];
            GameObject content = cardViewer.transform.GetChild(1).GetChild(0).gameObject;
            gh.SpawnCard(gh.SetCard(foundCard), content, false, graveyardCardPrefab);
        }
    }

    void CNI2()
    {

    }

    void CNI3()
    {

    }

    void CNI4()
    {

    }

    void CNR1()
    {

    }

    void CNR2()
    {

    }

    void CNR3()
    {

    }

    void CNR4()
    {

    }

    void CS1()
    {

    }

    void CS2()
    {

    }

    void CS3()
    {

    }

    void CS4()
    {

    }

    private void MarkCardAsUsed()
    {
        hasBeenUsed = true;
        
        var image = gameObject.GetComponent<Image>();
        image.color = Color.gray;

        var button = gameObject.GetComponent<Button>();
        button.enabled = false;
    }
}
