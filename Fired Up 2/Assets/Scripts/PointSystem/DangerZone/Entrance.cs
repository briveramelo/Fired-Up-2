using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Entrance : DangerZone {

    private Stopwatch timer;
    private int secondsPassed;
    [SerializeField] private int maxPoints = 10000;
    [SerializeField] private int maxSeconds = 5;
    private int pointDropRate;

    void Awake(){
        timer = new Stopwatch();
        pointDropRate = maxPoints / maxSeconds;
    }

    protected override void TriggerZone(){
        timer.Start();
    }

    public void StopTimer() {
        timer.Stop();
        secondsPassed = timer.Elapsed.Seconds;

        Confidence.Instance.ConfidencePoints += CalculateConfidence();
    }

    private int CalculateConfidence() {
        return Mathf.Clamp(maxPoints - pointDropRate * secondsPassed, 0, maxPoints);
    }
}
