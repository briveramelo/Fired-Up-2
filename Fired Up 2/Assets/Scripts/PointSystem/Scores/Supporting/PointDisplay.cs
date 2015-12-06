using UnityEngine;
using System.Collections;

public class PointDisplay : MonoBehaviour {

    [SerializeField] private TextMesh pointValue;
    [SerializeField] private TextMesh scoreType;

    public void DisplayPoints(int points, ScoreType scoreEnum) {
        pointValue.text = points.ToString();
        scoreType.text = scoreEnum.ToString();
    }

}
