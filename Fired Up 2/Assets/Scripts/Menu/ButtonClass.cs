using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour {

    [SerializeField] Text Name;
    [SerializeField] Text Rank;
    [SerializeField] Text TotalBest;

    public void SetPlayerInfo(PlayerInfo incomingPlayerInfo) {
        Name.text = incomingPlayerInfo._playerName;
        Rank.text = incomingPlayerInfo._myRank.ToString();
        TotalBest.text = incomingPlayerInfo._totalBest.ToString();
    }
}
