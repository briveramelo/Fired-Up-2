using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;

public class PointsDance : MonoBehaviour {


    private bool timerIsDone;
    private float period = 3f;
    private float maxTilt = 10f;
    private float pi = Mathf.PI;
    private float pulseTime = 2f;
    private float lerpRate = 0.03f;
    [SerializeField] private Text myText;

    void OnEnable() {
        StartCoroutine ( BustAMove());
    }

    IEnumerator BustAMove() {
        yield return StartCoroutine(Pulse());
        StartCoroutine(GetDirty());
    }


    IEnumerator Pulse() {
        int startSize = myText.fontSize;
        Stopwatch timer = new Stopwatch();
        timer.Start();
        float elapsedTime = 0f;
        while (elapsedTime < PointCategoryRevealer.Instance.TimeToFlipPoints) {
            elapsedTime = timer.ElapsedMilliseconds / 1000f;
            float fontCosCurvPoint = startSize * Mathf.Cos(elapsedTime * (2f * pi / PointCategoryRevealer.Instance.TimeToFlipPoints)) + startSize/2f + 1f;
            myText.fontSize = Mathf.RoundToInt(fontCosCurvPoint);
            yield return null;
        }
    }

    IEnumerator GetDirty() {
        float rotationAway = 10f;
        while (rotationAway > 0.01f) {
            float targetZRotation = maxTilt * Mathf.Sin(Time.realtimeSinceStartup * 2f * pi / period);
            rotationAway = transform.rotation.eulerAngles.z - targetZRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,0f,targetZRotation), lerpRate);
            yield return null;
        }
        while (true) {
            transform.rotation = Quaternion.Euler(0f, 0f, maxTilt * Mathf.Sin(Time.realtimeSinceStartup * 2f * pi / period));
            yield return null;
        }
    }
}
