using UnityEngine;
using System.Collections;
public class Resilience : ScoreType {

    //(damageTaken / lives Lost)

    public static Resilience Instance;
    private int damageDeathRatio;
    private int maxDamageDeathRatio = 200;
    private int pointRiseRate;

    void Awake() {
        Instance = this;
        pointRiseRate = maxPoints / maxDamageDeathRatio;
        MyScoreEnum = Score.Resilience;
    }

   
    protected override int CalculateScore() {
        if (DamageTracker.LivesLost == 0)
            return maxPoints;
        else {
            damageDeathRatio = DamageTracker.DamageTaken / DamageTracker.LivesLost;
            return Mathf.Clamp(pointRiseRate * damageDeathRatio, 0, maxPoints);
        }
    }
}
