using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PointDisplay : MonoBehaviour {

    [SerializeField] private TextMesh pointValue;
    [SerializeField] private TextMesh scoreType;
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] AudioClip pointClip;
    [SerializeField] AudioClip comboClip;
    protected static GameObject lastCombo;

    public void DisplayPoints(int points, ScoreType scoreEnum) {
        soundPlayer.clip = pointClip;
        soundPlayer.Play();
        pointValue.text = "+" + points.ToString();
        scoreType.text = scoreEnum.ToString();
        Destroy(gameObject, ComboTracker.Instance.MaxComboTime);
    }

    public void DisplayCombo(string combo) {
        soundPlayer.clip = comboClip;
        soundPlayer.Play();

        if (lastCombo != null)
            Destroy(lastCombo);
        lastCombo = gameObject;
        pointValue.text = combo;
        scoreType.text = "";
        Destroy(gameObject, ComboTracker.Instance.MaxComboTime);
    }

}
