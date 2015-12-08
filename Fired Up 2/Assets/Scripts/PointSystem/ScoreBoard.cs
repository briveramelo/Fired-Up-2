using UnityEngine;
using System.Collections;
using System;
public class ScoreBoard : MonoBehaviour {

    public static ScoreBoard Instance;

    private int Combos;
    private int Confidence;
    private int ProblemSolving;
    private int Resilience;
    private int SituationalAwareness;
    private int TimeBonus;
    private DateTime nowTime;
    [HideInInspector] public LevelEnum ThisLevel = LevelEnum.One;

    void Start() {
        Instance = this;
        ThisLevel = GameManager.Instance.CurrentLevel;
    }

    public void SetScore(ScoreType ScoreTypeToSet, int Score) {
        switch (ScoreTypeToSet) {
            case ScoreType.Combos:                  Combos =                Score;  break;
            case ScoreType.Confidence:              Confidence =            Score;  break;
            case ScoreType.Resilience:              Resilience =            Score;  break;
            case ScoreType.TimeBonus:               TimeBonus =             Score;  break;
            case ScoreType.SituationalAwareness:    SituationalAwareness =  Score;  break;
            case ScoreType.ProblemSolving:          ProblemSolving =        Score;  break;
        }
    }

    public void DocumentTime() {
        nowTime = DateTime.UtcNow;
    }

}
