using UnityEngine;
using System.Collections;
using FU;

public class NPC_Legs : MonoBehaviour {


    [SerializeField] Rigidbody myBody;
    private bool isFollowingPlayer;
    public Animator animFollow;

    private bool isBeingCarried;
    public bool IsBeingCarried { get { return isBeingCarried; } }
    private bool isBeingPickedUp;
    public bool IsBeingPickedUp { get { return isBeingPickedUp; } }
    private bool isSelectable;
    public bool IsSelectable { get { return isSelectable; } set { isSelectable = value; } }
    private bool isHomeSafe;

    private float maxSpeed = 5f;
    private float moveForce = 2f;
    private float distanceAway = 3f;
    private float pickUpSpeed = 0.04f;
    private float followSpeed = 0.025f;
    private float brakeFactor = 0.75f;
    private float maxBrakeMultiplier = 2f;
    [SerializeField] private NavMeshAgent myAgent;

    private enum AnimState
    {
        Coughing = 0,
        Following = 1,


    }


    public void ToggleFollow() {
        if (!isHomeSafe) {
            StopAllCoroutines();
            myAgent.enabled = true;
            isFollowingPlayer = !isFollowingPlayer;
            StartCoroutine(FollowPlayer());
        }
    }

    public void ToggleFollow(bool follow) {
        if (!isHomeSafe) {
            StopAllCoroutines();
            myAgent.enabled = true;
            isFollowingPlayer = follow;
            StartCoroutine(FollowPlayer());
        }
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
        if (!isHomeSafe) {
            StopAllCoroutines();
            myAgent.enabled = false;
            isBeingCarried = true;
            isBeingPickedUp = true;
            StartCoroutine(GetPickedUp());
            transform.parent = FireFighter.playerShoulderTransform;
        }
    }

    private IEnumerator GetPickedUp() {
        float distanceAway = 10;
        
        myBody.isKinematic = true;
        animFollow.SetInteger("AnimState", (int)AnimState.Following);
        while (NPCCollector.Instance.IsCarryingPlayer && distanceAway > 0.1f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, pickUpSpeed);
            distanceAway = Vector3.Distance(transform.localPosition, Vector3.zero);
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        isBeingPickedUp = false;
    }

    public void DropOff(float targetDistanceAway) {
        if (!isHomeSafe) {
            StopAllCoroutines();
            transform.parent = null;
            myBody.isKinematic = false;
            myAgent.enabled = true;
            myAgent.destination = FireFighter.playerTransform.position + FireFighter.playerTransform.forward * targetDistanceAway;
            isBeingCarried = false;
        }
    }

    public void EnterSafeZone(Vector3 zoneCenter) {
        StopAllCoroutines();
        if (isBeingCarried) {
            DropOff(.3f);
        }
        myAgent.destination = zoneCenter;
        isHomeSafe = true;
    }

}
