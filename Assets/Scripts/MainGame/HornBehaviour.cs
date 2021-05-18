using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornBehaviour : MonoBehaviour
{
    public RankBehaviour rank;

    public void Horn()
    {
        rank.globalHorned = true;
        rank.RankSum();
    }
}
