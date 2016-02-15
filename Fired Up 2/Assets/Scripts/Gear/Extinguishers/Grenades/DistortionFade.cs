using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Renderer))]
public class DistortionFade : MonoBehaviour {

    private float fadeTime = 2f;
    private Material distortionMaterial;
    void Awake() {
        distortionMaterial = GetComponent<MeshRenderer>().material;
        StartCoroutine(FadeMaterial());
    }

    IEnumerator FadeMaterial() {
        Color startColor = distortionMaterial.GetColor("_TintColor");
        Color newColor = startColor;
        while (newColor.a>0.01f) {
            newColor = new Color(newColor.r, newColor.g, newColor.b, Mathf.Clamp01(newColor.a - (startColor.a / fadeTime)) * Time.deltaTime);
            distortionMaterial.SetColor("_TintColor", newColor);
            yield return null;
        }
    }

    
}
