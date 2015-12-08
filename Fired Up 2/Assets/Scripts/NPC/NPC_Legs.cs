using UnityEngine;
using System.Collections;
using FU;

public class NPC_Legs : MonoBehaviour {

    [SerializeField] Rigidbody myBody;
    private bool isFollowingPlayer;

    private bool isBeingCarried;
    public bool IsBeingCarried { get { return isBeingCarried; } }
    private bool isBeingPickedUp;
    public bool IsBeingPickedUp { get { return isBeingPickedUp; } }
    private bool isSelectable;
    public bool IsSelectable { get { return isSelectable; } set { isSelectable = value; } }

    private float maxSpeed = 5f;
    private float moveForce = 2f;
    private float distanceAway = 3f;
    private float pickUpSpeed = 0.04f;
    private float followSpeed = 0.025f;
    private float brakeFactor = 0.75f;
    private float maxBrakeMultiplier = 2f;
    [SerializeField] private NavMeshAgent myAgent;


    public void ToggleFollow() {
        StopAllCoroutines();
        myAgent.enabled = true;
        isFollowingPlayer = !isFollowingPlayer;
        StartCoroutine(FollowPlayer());
    }

    public void ToggleFollow(bool follow) {
        StopAllCoroutines();
        myAgent.enabled = true;
        isFollowingPlayer = follow;
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer() {
        while (isFollowingPlayer) {
            myAgent.destination = FireFighter.followSpotTransform.position;
            yield return null;
        }
        myAgent.destination = transform.position;
        yield return null;
    }

    public void PickUp() {
        StopAllCoroutines();
        myAgent.enabled = false;
        isBeingCarried = true;
        isBeingPickedUp = true;
        StartCoroutine(GetPickedUp());
    }

    private IEnumerator GetPickedUp() {
        float distanceAway = 10;
        while (NPCCollector.Instance.IsCarryingPlayer && distanceAway > 0.1f) {
            transform.position = Vector3.Lerp(transform.position, FireFighter.playerShoulderTransform.position, pickUpSpeed);
            distanceAway = Vector3.Distance(transform.position, FireFighter.playerShoulderTransform.position);
            yield return null;
        }
        isBeingPickedUp = false;
        while (NPCCollector.Instance.IsCarryingPlayer) {
            transform.position = FireFighter.playerShoulderTransform.position;
            yield return null;
        }
    }

    public void DropOff(float targetDistanceAway) {
        StopAllCoroutines();
        myAgent.enabled = true;
        myAgent.destination = FireFighter.playerTransform.position + FireFighter.playerTransform.forward * targetDistanceAway;
        isBeingCarried = false;
    }

}
