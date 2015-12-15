using UnityEngine;
using System.Collections;

public class DistortionShrink : MonoBehaviour {

    private float fadeTime = 2f;
    void Awake() {
        StartCoroutine(ShrinkMaterial());
    }

	IEnumerator ShrinkMaterial() {
        Vector3 startingScale = transform.localScale;
        Vector3 currentScale = startingScale;
        while (startingScale.x > 0.01f) {
            currentScale -= Vector3.one * (startingScale.x / fadeTime) * Time.deltaTime;
            transform.localScale = currentScale;
            yield return null;
        }
    }
}
