﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class Selectable_Light : MonoBehaviour {

    [SerializeField] private Light myLight;
    [SerializeField] private NPC_Legs myLegs;
    public bool IsSelectable { get; set; }
    private float lightLerp = 0.05f;
    private float baselineIntensity = 1.25f;

    public void HighlightOnHover() {
        StopAllCoroutines();
        StartCoroutine(PulseLight());
    }

    IEnumerator PulseLight() {
        myLight.range = baselineIntensity;
        myLight.intensity = baselineIntensity;
        myLight.enabled = true;
        Stopwatch myTimer = new Stopwatch();
        myTimer.Start();
        while (IsSelectable) {
            float sinCurvePoint = 1.25f + Mathf.Sin(myTimer.ElapsedMilliseconds / 1000f) * .25f;
            myLight.intensity = sinCurvePoint;
            myLight.range = sinCurvePoint;
            yield return null;
        }
        myTimer.Stop();
        myLight.enabled = false;
    }
}
