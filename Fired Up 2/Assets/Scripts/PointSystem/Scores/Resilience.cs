using UnityEngine;
using System.Collections;
public class Resilience : Score {

    //(damageTaken / lives Lost)

    public static Resilience Instance;
    private int damageDeathRatio;
    private int maxDamageDeathRatio = 200;
    private int pointRiseRate;

    void Awake() {
        Instance = this;
        pointRiseRate = maxPoints / maxDamageDeathRatio;
        MyScoreType = ScoreType.Resilience;
    }

   
    protected override int CalculateScore() {
        damageDeathRatio = DamageTracker.DamageTaken / DamageTracker.LivesLost;
        if (DamageTracker.LivesLost == 0)
            return maxPoints;
        else
            return Mathf.Clamp(pointRiseRate * damageDeathRatio, 0, maxPoints);
    }
}
