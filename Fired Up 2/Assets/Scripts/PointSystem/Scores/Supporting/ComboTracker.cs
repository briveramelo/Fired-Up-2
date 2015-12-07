using UnityEngine;
using System.Collections;
using FU;
public class ComboTracker : MonoBehaviour {

    [SerializeField] private GameObject pointDisplayGameObject;
    [HideInInspector] public static ComboTracker Instance;

    private float maxComboTime;
    public float MaxComboTime { get { return maxComboTime; } set { maxComboTime = value; } }

    private bool isComboing;
    private Coroutine Timer;
    private int firesInCurrentCombo;
    private int biggestCombo;
    private int currentPointPool;

	void Start () {
        Instance = this;
        maxComboTime = 1.5f;
    }

    public void AddToFireCombo(Vector3 displayPosition, int pointsToAdd) {
        if (isComboing) {
            firesInCurrentCombo++;
            currentPointPool += pointsToAdd;
        }
        else {
            if (firesInCurrentCombo > biggestCombo)
                biggestCombo = firesInCurrentCombo;
            firesInCurrentCombo = 1;
            currentPointPool = pointsToAdd;
        }
        Quaternion displayRotation = Quaternion.LookRotation(displayPosition - FireFighter.playerTransform.position);
        PointDisplay comboDisplay = (Instantiate(pointDisplayGameObject, displayPosition, displayRotation) as GameObject).GetComponent<PointDisplay>();
        comboDisplay.DisplayCombo("x " + firesInCurrentCombo);

        if (Timer!= null)
            StopCoroutine(Timer);
        Timer = StartCoroutine(ComboTimer(displayPosition));
    }

    IEnumerator ComboTimer(Vector3 displayPosition) {
        isComboing = true;
        yield return new WaitForSeconds(maxComboTime);

        Quaternion displayRotation = Quaternion.LookRotation(displayPosition - FireFighter.playerTransform.position);
        PointDisplay pointDisplay = (Instantiate(pointDisplayGameObject, displayPosition, displayRotation) as GameObject).GetComponent<PointDisplay>();
        int pointPoolCombo = currentPointPool * firesInCurrentCombo;
        pointDisplay.DisplayPoints(pointPoolCombo, ScoreType.ProblemSolving);

        ProblemSolving.Instance.ProblemSolvingPoints += pointPoolCombo;
        isComboing = false;
        yield return null;
    }
}
