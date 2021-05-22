using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerBehaviour : MonoBehaviour
{
    public GameHandler gh;
    public GameObject CardHolder;
    Vector3 dist = new Vector3();
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    bool mouseClicked;
    int siblingIndex;
    CardBehaviour clickedCard;
    GameObject CardGO;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        Controls();
    }

    void Controls()
    {
        if (gh.turn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CardGO = SelectedCard();
                
                if (CardGO != null)
                {
                    clickedCard = CardGO.GetComponent<CardBehaviour>();
                    mouseClicked = true;
                    dist = Input.mousePosition - CardGO.transform.position;

                    if (clickedCard.isMovable)
                    {
                        CardHolder.GetComponent<GridLayoutGroup>().enabled = false;
                        siblingIndex = CardGO.transform.GetSiblingIndex();
                        CardGO.transform.SetAsLastSibling();
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                CardGO = SelectedCard();
                if (mouseClicked & CardGO != null)
                {
                    mouseClicked = false;
                    clickedCard = CardGO.GetComponent<CardBehaviour>();
                    GameObject rank = AboveRank();
                    if (rank != null)
                    {
                        if (clickedCard.card.rank == Rank.Decoy && clickedCard.isMovable && !rank.name.Contains("En"))
                        {
                            GameObject decoyableCard = DecoyableCard();
                            if (decoyableCard != null)
                            {
                                CardHolder.GetComponent<CardHolder>().cards.RemoveAll(x => x.ID == clickedCard.card.ID);
                                gh.DecoyCard(CardGO, decoyableCard, rank);
                                CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                            }
                        }
                        else
                        {
                            if (clickedCard.isMovable && isCardPlaceable(clickedCard.card, rank))
                            {
                                gh.PlaceCard(rank, CardGO);
                                CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                            }


                        }
                        if (clickedCard.isMovable)
                        {
                            CardGO.transform.SetSiblingIndex(siblingIndex);
                            CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                        }
                        
                    }
                    else if (clickedCard.isMovable)
                    {
                        CardGO.transform.SetSiblingIndex(siblingIndex);
                        CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                    }
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (CardGO != null && CardGO.GetComponent<CardBehaviour>().isMovable)
                {
                    CardGO.transform.position = Input.mousePosition - dist;
                }
            }
        }
        
    }

    GameObject SelectedCard()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject hitGO = result.gameObject.transform.parent.gameObject;

            if (hitGO.TryGetComponent(out CardBehaviour cardBehaviour))
            {
                return hitGO;
            }
        }
        return null;
    }

    GameObject DecoyableCard()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject hitGO = result.gameObject.transform.parent.gameObject;

            if (hitGO.TryGetComponent(out CardBehaviour cardBehaviour))
            {
                if (hitGO.GetComponent<CardBehaviour>().card.rank != Rank.Decoy)
                {
                    return hitGO;
                }
            }
        }
        return null;  
    }

    GameObject AboveRank()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "Rank")
            {
                return result.gameObject;
            }
        }
        return null;
    }

    bool isCardPlaceable(Card _card, GameObject _rank)
    {
        string rankName = _rank.name.ToString();
        string cardRankName = _card.rank.ToString();

        if (_card.rank == Rank.Close | _card.rank == Rank.Ranged | _card.rank == Rank.Siege)
        {
            if (rankName == "Rank" + cardRankName + " P")
            {
                return true;
            }
        }
        else if(_card.ability == Ability.Spy)
        {
            if (rankName.Contains(cardRankName) && rankName.Contains("En"))
            {
                return true;
            }
        }
        else if (_card.rank == Rank.Agile)
        {
            if ((rankName.Contains("Ranged") || rankName.Contains("Close")) && rankName.Contains("P"))
            {
                return true;
            }
        }
        else if (_card.rank == Rank.Weather)
        {
            if (rankName.Contains("Weather"))
            {
                return true;
            }
        }
        else if (_card.rank == Rank.Horn)
        {
            if (rankName.Contains("Horn") & _rank.transform.childCount == 0)
            {
                return true;
            }
        }
        return false;
    }       
}
