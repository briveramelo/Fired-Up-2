using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class TimeBonus : Score {

    //Set these for each level in the inspector
    //In Descending order
    [SerializeField] private int timeToZeroPoints = 100; 
    [SerializeField] private int timeToD = 90;
    [SerializeField] private int timeToC = 80;
    [SerializeField] private int timeToB = 70; 
    [SerializeField] private int timeToA = 60;

    private Stopwatch timer;
    private float pointDropRate;
    private int secondsPassed;

    void Awake() {
        timer = new Stopwatch();
        timer.Start();
        pointDropRate = (1 / (timeToZeroPoints - timeToA));
        MyScoreType = ScoreType.TimeBonus;
    }

    protected override int CalculateScore() {
        secondsPassed = timer.Elapsed.Seconds;
        timer.Stop();

        if (secondsPassed <= timeToA)
            return maxPoints;
        else if (secondsPassed > timeToA && secondsPassed < timeToZeroPoints)
            return maxPoints - (int)(pointDropRate * secondsPassed);
        else
            return 0;
    }

}
