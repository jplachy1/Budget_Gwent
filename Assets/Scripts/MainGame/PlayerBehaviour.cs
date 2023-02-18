using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerBehaviour : MonoBehaviour
{
    public GameHandler gh;
    public GameObject CardHolder;
    public GameObject cardView;

    Vector3 dist = new Vector3();
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    bool mouseClicked;
    int siblingIndex;
    CardBehaviour clickedCard;
    GameObject holdingCardGO;

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
        if (gh.IsPlayersTurn())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                holdingCardGO = SelectedCard();
                
                if (holdingCardGO != null)
                {
                    clickedCard = holdingCardGO.GetComponent<CardBehaviour>();
                    mouseClicked = true;
                    dist = Input.mousePosition - holdingCardGO.transform.position;

                    if (clickedCard.isMovable)
                    {
                        CardHolder.GetComponent<GridLayoutGroup>().enabled = false;
                        siblingIndex = holdingCardGO.transform.GetSiblingIndex();
                        holdingCardGO.transform.SetAsLastSibling();
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (mouseClicked & holdingCardGO != null)
                {
                    mouseClicked = false;
                    clickedCard = holdingCardGO.GetComponent<CardBehaviour>();
                    GameObject rank = AboveRank();
                    if (rank != null)
                    {
                        if (clickedCard.card.Rank == Rank.Decoy && clickedCard.isMovable && !rank.name.Contains("En"))
                        {
                            GameObject decoyableCard = DecoyableCard();
                            if (decoyableCard != null)
                            {
                                CardHolder.GetComponent<CardHolder>().cards.RemoveAll(x => x.Id == clickedCard.card.Id);
                                gh.DecoyCard(holdingCardGO, decoyableCard, rank);
                                CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                            }
                        }
                        else if (clickedCard.isMovable && isCardPlaceable(clickedCard.card, rank))
                        {
                            gh.PlaceCard(rank, holdingCardGO);
                            CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                        }

                        if (clickedCard.isMovable)
                        {
                            holdingCardGO.transform.SetSiblingIndex(siblingIndex);
                            CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                        }
                        
                    }
                    else if (clickedCard.isMovable)
                    {
                        holdingCardGO.transform.SetSiblingIndex(siblingIndex);
                        CardHolder.GetComponent<GridLayoutGroup>().enabled = true;
                    }
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (holdingCardGO != null && holdingCardGO.GetComponent<CardBehaviour>().isMovable)
                {
                    holdingCardGO.transform.position = Input.mousePosition - dist;
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
                if (hitGO.GetComponent<CardBehaviour>().card.Rank != Rank.Decoy)
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
        string cardRankName = _card.Rank.ToString();

        if ((_card.Rank == Rank.Close | _card.Rank == Rank.Ranged | _card.Rank == Rank.Siege) & _card.Ability != Ability.Spy)
        {
            if (rankName == "Rank" + cardRankName + " P")
            {
                return true;
            }
        }
        else if(_card.Ability == Ability.Spy)
        {
            if (rankName.Contains(cardRankName) && rankName.Contains("En"))
            {
                return true;
            }
        }
        else if (_card.Rank == Rank.Agile)
        {
            if ((rankName.Contains("Ranged") || rankName.Contains("Close")) && rankName.Contains("P"))
            {
                return true;
            }
        }
        else if (_card.Rank == Rank.Weather)
        {
            if (rankName.Contains("Weather"))
            {
                return true;
            }
        }
        else if (_card.Rank == Rank.Horn)
        {
            if (rankName.Contains("Horn") & _rank.transform.childCount == 0)
            {
                return true;
            }
        }
        return false;
    }       
}
