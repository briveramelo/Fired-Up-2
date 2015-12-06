using UnityEngine;
using System.Collections;

public static class ScoreBoard {

    static int Combos;
    static int Confidence;
    static int ProblemSolving;
    static int Resilience;
    static int SituationalAwareness;
    static int TimeBonus;

    public static void SetScore(ScoreType ScoreTypeToSet, int Score) {
        switch (ScoreTypeToSet) {
            case ScoreType.Combos:                  Combos = Score;             break;
            case ScoreType.Confidence:              Confidence = Score;         break;
            case ScoreType.Resilience:              Resilience = Score;         break;
            case ScoreType.TimeBonus:               TimeBonus = Score;          break;
            case ScoreType.SituationalAwareness:    SituationalAwareness = Score;break;
            case ScoreType.ProblemSolving:          ProblemSolving = Score;     break;
        }
    }

}
