using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour {

    [SerializeField] Text Name;
    [SerializeField] Text Rank;
    [SerializeField] Text TotalBest;
    public Button button;
    private PlayerInfo myPlayerInfo; public PlayerInfo MyPlayerInfo { get { return myPlayerInfo; } }
    public bool IsSelected { get; set; }
    private Color selectedColor = Color.red;
    private Color deselectedColor = Color.white;

    public void SetPlayerInfo(PlayerInfo incomingPlayerInfo) {
        Name.text = incomingPlayerInfo.playerName;
        Rank.text = incomingPlayerInfo.myRank.ToString();
        TotalBest.text = incomingPlayerInfo.totalBest.ToString();
        myPlayerInfo = incomingPlayerInfo;
    }

    public void HighlightMe() {
        IsSelected = true;
        button.image.color = selectedColor;
    }

    public void UnHighlightMe() {
        IsSelected = false;
        button.image.color = deselectedColor;
    }
}
