using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

    public static EndOfLevel Instance;
    void Awake() {
        Instance = this;
    }

    public void SaveScores() {
        foreach (Score score in FindObjectsOfType<Score>()) {
            score.SendToScoreBoard();
        }
        ScoreBoard.Instance.DocumentTime();
        ScoreBoard.Instance.CalculateTotal();
        ScoreSaver.Instance.PromptSave(ScoreBoard.Instance);
        
    }

    public void EndLevel() {
        Application.LoadLevel(1); //pointscreen
    }
}
