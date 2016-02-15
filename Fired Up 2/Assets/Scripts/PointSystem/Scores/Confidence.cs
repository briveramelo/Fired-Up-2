using UnityEngine;
using System.Collections;

public class Confidence : ScoreType {

    public static Confidence Instance;
    [HideInInspector] public int ConfidencePoints; 

    void Awake() {
        Instance = this;
        MyScoreEnum = Score.Confidence;
    }

    protected override int CalculateScore(){
        return ConfidencePoints;
    }
}
