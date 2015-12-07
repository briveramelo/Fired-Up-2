using UnityEngine;
using System.Collections;

public class ProblemSolving : Score {

    public static ProblemSolving Instance;
    public int ProblemSolvingPoints { get; set; }

    void Awake() {
        Instance = this;
        MyScoreType = ScoreType.ProblemSolving;
    }

    

    protected override int CalculateScore() {
        return 1;
    }
}
