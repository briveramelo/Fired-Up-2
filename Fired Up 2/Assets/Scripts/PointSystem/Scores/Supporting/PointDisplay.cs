﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
public class PointDisplay : MonoBehaviour {

    [SerializeField] private TextMesh pointValue;
    [SerializeField] private TextMesh scoreType;
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] AudioClip pointClip;
    [SerializeField] AudioClip comboClip;
    protected static GameObject lastCombo;
    private float lerpSpeed = 0.075f;

    public void DisplayPoints(int points, Score scoreEnum, bool followPlayer = false) {
        soundPlayer.clip = pointClip;
        soundPlayer.Play();
        pointValue.text = "+" + points.ToString();
        scoreType.text = scoreEnum.ToString();
        Destroy(gameObject, ComboTracker.Instance.MaxComboTime);
        if (followPlayer)
            StartCoroutine(EnsurePlayerCanSeePoints());
    }

    IEnumerator EnsurePlayerCanSeePoints() {
        transform.parent = FireFighter.pointsSpot;
        while (true) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, lerpSpeed);
            transform.rotation = transform.LookAtPlayer(transform.position);
            yield return null;
        }
    }

    public void DisplayCombo(string combo, bool followPlayer = false) {
        soundPlayer.clip = comboClip;
        soundPlayer.Play();

        if (lastCombo != null)
            Destroy(lastCombo);
        if (followPlayer)
            StartCoroutine(EnsurePlayerCanSeePoints());

        lastCombo = gameObject;
        pointValue.text = combo;
        scoreType.text = "";
        Destroy(gameObject, ComboTracker.Instance.MaxComboTime);
    }

}
