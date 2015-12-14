using UnityEngine;
using System.Collections;
using System;
public class ScoreBoard : MonoBehaviour {

    public static ScoreBoard Instance;
    private LevelSaveData thisLevelSaveData; public LevelSaveData ThisLevelSaveData { get { return thisLevelSaveData; } }

    void Start() {
        Instance = this;
        thisLevelSaveData = new LevelSaveData();
        thisLevelSaveData.thisLevel = GameManager.Instance.CurrentLevel;
        thisLevelSaveData.levelDifficulty = GameManager.Instance.LevelDifficulty;
    }

    public void SetScore(Score ScoreToSet, int newSetScore) {
        switch (ScoreToSet) {
            case Score.Combos:                  thisLevelSaveData.combos =                newSetScore;  break;
            case Score.Confidence:              thisLevelSaveData.confidence =            newSetScore;  break;
            case Score.Resilience:              thisLevelSaveData.resilience =            newSetScore;  break;
            case Score.TimeBonus:               thisLevelSaveData.timeBonus =             newSetScore;  break;
            case Score.SituationalAwareness:    thisLevelSaveData.situationalAwareness =  newSetScore;  break;
            case Score.ProblemSolving:          thisLevelSaveData.problemSolving =        newSetScore;  break;
        }
    }

    public void CalculateTotal(){
        thisLevelSaveData.pointTotal = 0;
        foreach (Score score in Enum.GetValues(typeof(Score))){
            thisLevelSaveData.pointTotal += GetScore(score);
        }
    }

    public int GetScore(Score ScoreToReturn) {
        switch (ScoreToReturn) {
            case Score.Combos:                  return thisLevelSaveData.combos;
            case Score.Confidence:              return thisLevelSaveData.confidence;
            case Score.Resilience:              return thisLevelSaveData.resilience;
            case Score.TimeBonus:               return thisLevelSaveData.timeBonus;
            case Score.SituationalAwareness:    return thisLevelSaveData.situationalAwareness;
            case Score.ProblemSolving:          return thisLevelSaveData.problemSolving;
            case Score.Total:                   return thisLevelSaveData.pointTotal;
        }
        int whyMakeMeDoThisUnity = 0;
        return whyMakeMeDoThisUnity;
    }


    public void DocumentTime() {
        thisLevelSaveData.nowTime = DateTime.UtcNow;
    }

}
