using UnityEngine;
using System.Collections;
using FU;
using System;
using UnityEngine.UI;

public class PointCategoryRevealer : MonoBehaviour {

    [SerializeField] Text levelName;
	[SerializeField] GameObject[] categories;
    [SerializeField] Text[] pointTextScripts;
    [SerializeField] GameObject total;
    [SerializeField] Text totalTextScript;

    private float timeToFlipPoints = 1.5f;

    IEnumerator Start() {
        yield return null;
        Controls.SetControls();
        levelName.text = "Level1";
        foreach (ScoreType scoreType in Enum.GetValues(typeof(ScoreType))){
            categories[(int)scoreType].SetActive(true);
            int score = ScoreBoard.Instance.GetScore(scoreType);
            int currentPointDisplay = 0;
            int flipRate = (int)(score / timeToFlipPoints);
            while (!Input.GetButtonDown(Controls.Jump) && currentPointDisplay < score) {
                currentPointDisplay += flipRate;
                pointTextScripts[(int)scoreType].text = currentPointDisplay.ToString();
                yield return null;
            }
            pointTextScripts[(int)scoreType].text = score.ToString();
            yield return new WaitForSeconds(.5f);
        }
        while (!Input.GetButtonDown(Controls.Jump)) {
            yield return null;
        }

        foreach (GameObject game in categories) {
            game.SetActive(false);
        }

        total.SetActive(true);
        int scoreTotal = ScoreBoard.Instance.PointTotal;
        int pointDisplay = 0;
        int jumpRate = (int)(scoreTotal / (timeToFlipPoints + 1.5f));
        while (!Input.GetButtonDown(Controls.Jump) && pointDisplay < scoreTotal) {
            pointDisplay += jumpRate;
            totalTextScript.text = pointDisplay.ToString();
            yield return null;
        }
        totalTextScript.text = scoreTotal.ToString();

        while (!Input.GetButtonDown(Controls.Jump)) {
            Application.LoadLevel(0);
            yield return null;
        }

    }

    IEnumerator PumpThePoints() {
        yield return null;

    }
}
