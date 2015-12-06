using UnityEngine;
using System.Collections;

public class ProblemSolving : Score {

    void Awake() {
        MyScoreType = ScoreType.ProblemSolving;
    }

    protected override int CalculateScore() {
        return 1;
    }
}
