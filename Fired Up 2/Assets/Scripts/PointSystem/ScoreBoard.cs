using UnityEngine;
using System.Collections;
using System;
public class ScoreBoard : MonoBehaviour {

    public static ScoreBoard Instance;

    private int combos;                 public int Combos { get { return combos; } }
    private int confidence;             public int Confidence { get { return confidence; } }
    private int problemSolving;         public int ProblemSolving { get { return problemSolving; } }
    private int resilience;             public int Resilience { get { return resilience; } }
    private int situationalAwareness;   public int SituationalAwareness { get { return situationalAwareness; } }
    private int timeBonus;              public int TimeBonus { get { return timeBonus; } }
    private int pointTotal;             public int PointTotal { get { return pointTotal; } }
    private DateTime nowTime;           
    [HideInInspector] public LevelEnum ThisLevel = LevelEnum.One;

    void Start() {
        Instance = this;
        ThisLevel = GameManager.Instance.CurrentLevel;
    }

    public void SetScore(ScoreType ScoreTypeToSet, int Score) {
        switch (ScoreTypeToSet) {
            case ScoreType.Combos:                  combos =                Score;  break;
            case ScoreType.Confidence:              confidence =            Score;  break;
            case ScoreType.Resilience:              resilience =            Score;  break;
            case ScoreType.TimeBonus:               timeBonus =             Score;  break;
            case ScoreType.SituationalAwareness:    situationalAwareness =  Score;  break;
            case ScoreType.ProblemSolving:          problemSolving =        Score;  break;
        }
    }

    public int GetScore(ScoreType ScoreToReturn) {
        switch (ScoreToReturn) {
            case ScoreType.Combos:                  return combos;
            case ScoreType.Confidence:              return confidence;
            case ScoreType.Resilience:              return resilience;
            case ScoreType.TimeBonus:               return timeBonus;
            case ScoreType.SituationalAwareness:    return situationalAwareness;
            case ScoreType.ProblemSolving:          return problemSolving;
        }
        int whyMakeMeDoThisUnity = 0;
        return whyMakeMeDoThisUnity;
    }

    public void CalculateTotal(){
        pointTotal = 0;
        foreach (ScoreType scoreType in Enum.GetValues(typeof(ScoreType))){
            pointTotal += GetScore(scoreType);
        }
    }

    public void DocumentTime() {
        nowTime = DateTime.UtcNow;
    }

}
