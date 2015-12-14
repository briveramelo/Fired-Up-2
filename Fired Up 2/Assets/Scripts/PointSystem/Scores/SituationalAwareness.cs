using UnityEngine;
using System.Collections;

public class SituationalAwareness : ScoreType {

    //set for each level
    //Damage beyond this returns 0 points;
    [SerializeField] private int damageBeforeZeroPoints =1000;
    private int pointDropRate;

    void Awake() {
        pointDropRate = maxPoints / damageBeforeZeroPoints;
        MyScoreEnum = Score.SituationalAwareness;
    }

    protected override int CalculateScore() {
        int points = maxPoints - (DamageTracker.DamageTaken * pointDropRate);
        return (Mathf.Clamp(points, 0, maxPoints)); 
    }
}
