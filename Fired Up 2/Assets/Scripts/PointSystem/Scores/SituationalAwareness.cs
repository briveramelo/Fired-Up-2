using UnityEngine;
using System.Collections;

public class SituationalAwareness : Score {

    //set for each level
    //Damage beyond this returns 0 points;
    [SerializeField] private int damageBeforeZeroPoints =100;
    private int pointDropRate;

    void Awake() {
        pointDropRate = maxPoints / damageBeforeZeroPoints;
        MyScoreType = ScoreType.SituationalAwareness;
    }

    protected override int CalculateScore() {
        int points = maxPoints - (DamageTracker.DamageTaken * pointDropRate);
        return (Mathf.Clamp(points, 0, maxPoints)); 
    }
}
