using UnityEngine;
using System.Collections;

public abstract class ScoreType : MonoBehaviour{

    protected Score MyScoreEnum;
    protected int maxPoints = 50000;

    public void SendToScoreBoard() {
        ScoreBoard.Instance.SetScore(MyScoreEnum, CalculateScore());
    }

    protected abstract int CalculateScore();

}
