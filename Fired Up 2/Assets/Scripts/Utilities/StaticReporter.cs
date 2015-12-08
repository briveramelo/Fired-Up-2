using UnityEngine;
using System.Collections;
using FU;
public class StaticReporter : MonoBehaviour {

    [SerializeField] private Transform followSpot;
    [SerializeField] private Transform shoulder;
    [SerializeField] private Transform pointsSpot;

    void Awake() {
        FireFighter.pointsSpot = transform;
        FireFighter.playerShoulderTransform = shoulder;
        FireFighter.followSpotTransform = followSpot;
    }
}
