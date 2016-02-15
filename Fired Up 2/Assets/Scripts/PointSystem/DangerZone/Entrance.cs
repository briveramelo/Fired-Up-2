using UnityEngine;
using System.Collections;
using System.Diagnostics;
using FU;
public class Entrance : DangerZone {

    private Stopwatch timer;
    private float secondsPassed;
    [SerializeField] private GameObject pointDisplayGameObject;
    [SerializeField] private GameObject exit;

    [SerializeField] private int maxPoints = 10000;
    [SerializeField] private int maxSeconds;
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
        secondsPassed = timer.ElapsedMilliseconds / 1000f;

        int points = CalculateConfidence();
        Confidence.Instance.ConfidencePoints += points;
        PointDisplay pointsToDisplay = (Instantiate(pointDisplayGameObject, exit.transform.position, exit.transform.LookAtPlayer(exit.transform.position)) as GameObject).GetComponent<PointDisplay>();
        pointsToDisplay.DisplayPoints(points, Score.Confidence, true);
    }

    private int CalculateConfidence() {
        return Mathf.Clamp(maxPoints - Mathf.RoundToInt(pointDropRate * secondsPassed), 0, maxPoints);
    }
}
