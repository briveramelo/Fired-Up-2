using UnityEngine;
using System.Collections;

public abstract class Score : MonoBehaviour{

    protected ScoreType MyScoreType;
    protected int maxPoints = 50000;

    protected void SendToScoreBoard() {
        ScoreBoard.SetScore(MyScoreType, CalculateScore());
    }

    protected abstract int CalculateScore();

}
