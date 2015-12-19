using UnityEngine;
using System.Collections;
using FU;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class PointCategoryRevealer : MonoBehaviour {

    [SerializeField] private Text levelName;
	[SerializeField] private GameObject[] pointGameObjects;
    private Animator[] pointAnimators;
    private PointsDance[] pointDancers;
    private Text[] pointTextScripts;

    [SerializeField] private Text totalText;
    [SerializeField] private GameObject totalGameObject;
    [SerializeField] private Animator totalAnimator;

    public static PointCategoryRevealer Instance;
    private float timeToFlipPoints = 1.5f; public float TimeToFlipPoints { get { return timeToFlipPoints; } }

    void Awake() {
        Controls.SetControls();

        Instance = this;
        int arraySize = pointGameObjects.Length;
        pointTextScripts = new Text[arraySize + 1];
        pointDancers = new PointsDance[arraySize];
        pointAnimators = new Animator[arraySize];
        for (int i = 0; i < pointGameObjects.Length; i++) {
            pointTextScripts[i] = pointGameObjects[i].GetComponent<Text>();
            pointDancers[i] = pointGameObjects[i].GetComponent<PointsDance>();
            pointAnimators[i] = pointGameObjects[i].GetComponent<Animator>();
        }
    }

    IEnumerator Start() {
        yield return null;
            levelName.text = GameManager.Instance.CurrentLevel.ToString();
        yield return StartCoroutine(DisplayAllSubtotals());
        yield return StartCoroutine(WaitForInput());
            HidePoints();
        yield return null;
        yield return StartCoroutine(DisplayPoints(totalGameObject, totalText, Score.Total, totalAnimator));
        yield return StartCoroutine(WaitForInput());
            SceneManager.LoadScene((int)Level.LevelSelect);
    }

    IEnumerator DisplayAllSubtotals() {
        for (int i=0; i<Enum.GetValues(typeof(Score)).Length-1; i++){
            yield return StartCoroutine(DisplayPoints(pointGameObjects[i], pointTextScripts[i], (Score)i, pointAnimators[i]));
        }
    }
    
    IEnumerator DisplayPoints(GameObject textToActivate, Text textScript, Score ScoreToReturn, Animator pointAnimator) {
        textToActivate.transform.parent.gameObject.SetActive(true);
        textToActivate.SetActive(true);

        int pointDisplay = 0;
        int score = ScoreBoard.Instance.GetScore(ScoreToReturn) * 10000 + 1337;
        int flipRate = Mathf.CeilToInt(score / (timeToFlipPoints * 60));
        while (!Input.GetButtonDown(Controls.Jump) && pointDisplay < score){
            pointDisplay += flipRate;
            textScript.text = pointDisplay.ToString();
            yield return null;
        }
        pointAnimator.SetInteger("AnimState", 1);
        textScript.text = score.ToString();

        Stopwatch timer = new Stopwatch();
        timer.Start();
        float elapsedTime = 0f;
        while (!Input.GetButtonDown(Controls.Jump) && elapsedTime < timeToFlipPoints) {
            elapsedTime = timer.ElapsedMilliseconds / 1000f;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator WaitForInput() {
        while (!Input.GetButtonDown(Controls.Jump)) {
            yield return null;
        }
    }

    void HidePoints() {
        foreach (GameObject game in pointGameObjects) {
            game.transform.parent.gameObject.SetActive(false);
        }
    }
}
