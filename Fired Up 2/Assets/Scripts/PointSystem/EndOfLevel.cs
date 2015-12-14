using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EndOfLevel : MonoBehaviour {

    public static EndOfLevel Instance;
    void Awake() {
        Instance = this;
    }

    public void SaveScores() {
        foreach (ScoreType scoreType in FindObjectsOfType<ScoreType>()) {
            scoreType.SendToScoreBoard();
        }
        ScoreBoard.Instance.DocumentTime();
        ScoreBoard.Instance.CalculateTotal();
        DataSaver.Instance.PromptSave(ScoreBoard.Instance.ThisLevelSaveData);
        
    }

    public void EndLevel() {
        SceneManager.LoadScene((int)Level.PointScreen);
    }
}
