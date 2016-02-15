using UnityEngine;
using System.Collections;

public class ProblemSolving : ScoreType {

    public static ProblemSolving Instance;
    public int ProblemSolvingPoints { get; set; }

    void Awake() {
        Instance = this;
        MyScoreEnum = Score.ProblemSolving;
    }

    

    protected override int CalculateScore() {
        return ProblemSolvingPoints;
    }
}
