using UnityEngine;
using System.Collections;

public abstract class Score : MonoBehaviour{

    protected ScoreType MyScoreType;
    protected int maxPoints = 50000;

    public void SendToScoreBoard() {
        ScoreBoard.Instance.SetScore(MyScoreType, CalculateScore());
    }

    protected abstract int CalculateScore();

}
