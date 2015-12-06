using UnityEngine;
using System.Collections;

public class Confidence : Score {

    public static Confidence Instance;
    [HideInInspector] public int ConfidencePoints; 

    void Awake() {
        Instance = this;
        MyScoreType = ScoreType.Confidence;
    }

    protected override int CalculateScore(){
        return ConfidencePoints;
    }
}
