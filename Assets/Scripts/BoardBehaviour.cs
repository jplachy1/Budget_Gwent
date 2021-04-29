using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardBehaviour : MonoBehaviour
{
    public RankBehaviour [] ranks = new RankBehaviour[3];
    public Text boardDamageText;
    int boardDamage;
    
    void Start()
    {

    }

    void Update()
    {
        UpdateBoardScore();
    }

    int GetRankScore(RankBehaviour _rank)
    {
        return _rank.RankScore();
    }

    public void UpdateBoardScore()
    {
        boardDamage = 0;
        foreach (RankBehaviour rank in ranks)
        {
            boardDamage += GetRankScore(rank);
        }

        boardDamageText.text = boardDamage.ToString();   
    }
}
