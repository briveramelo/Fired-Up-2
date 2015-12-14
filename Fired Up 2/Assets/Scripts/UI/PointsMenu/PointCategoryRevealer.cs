using UnityEngine;
using System.Collections;
using FU;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointCategoryRevealer : MonoBehaviour {

    [SerializeField] Text levelName;
	[SerializeField] GameObject[] pointGameObjects;
    [SerializeField] Text[] pointTextScripts;

    private float timeToFlipPoints = 1.5f;

    IEnumerator Start() {
        yield return null;
        levelName.text = ScoreBoard.Instance.ThisLevelSaveData.thisLevel.ToString();
        yield return StartCoroutine(DisplayAllSubtotals());
        yield return StartCoroutine(WaitForInput());
        HidePoints();
        yield return StartCoroutine(DisplayPoints(pointGameObjects[pointGameObjects.Length-1], Score.Total));
        yield return StartCoroutine(WaitForInput());
        SceneManager.LoadScene((int)Level.LevelSelect);
    }

    void Elsewhere() {
        Controls.SetControls();
    }

    IEnumerator DisplayAllSubtotals() {
        for (int i=0; i<Enum.GetValues(typeof(ScoreType)).Length-1; i++){
            yield return StartCoroutine(DisplayPoints(pointGameObjects[i], (Score)i));
        }
    }
    
    IEnumerator DisplayPoints(GameObject textToActivate, Score ScoreToReturn) {
        textToActivate.SetActive(true);
        int score = ScoreBoard.Instance.GetScore(ScoreToReturn);
        int pointDisplay = 0;
        int flipRate = (int)(score / timeToFlipPoints);
        while (!Input.GetButtonDown(Controls.Jump) && pointDisplay < score) {
            pointDisplay += flipRate;
            pointTextScripts[(int)ScoreToReturn].text = pointDisplay.ToString();
            yield return null;
        }
        pointTextScripts[(int)ScoreToReturn].text = score.ToString();
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator WaitForInput() {
        while (!Input.GetButtonDown(Controls.Jump)) {
            yield return null;
        }
    }

    void HidePoints() {
        foreach (GameObject game in pointGameObjects) {
            game.SetActive(false);
        }
    }
}
