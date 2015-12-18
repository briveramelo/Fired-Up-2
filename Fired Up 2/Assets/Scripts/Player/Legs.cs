using UnityEngine;
using System.Collections;
using FU;
using System.Diagnostics;

public class Legs : MonoBehaviour {

	[SerializeField]	private	Rigidbody playerBody;
	[SerializeField]	private Transform feetTran;
    [SerializeField]    private Transform cameraTran;
	private float minAxisInput;
	private float speed;
	private float maxSpeed;
	private float moveForce;
    private bool canMove = true;
    private float pickUpTime = 2f;
    private bool isSquatting = false;
    private bool isMidSquat = false;
    [SerializeField, Range (0,0.5f)]  private float squatSpeed = 0.03f;
    private float standingMaxSpeed = 5f;
    private float squattingMaxSpeed = 3f;

    void Awake () {
		maxSpeed = standingMaxSpeed;
		minAxisInput = 0.15f;
		moveForce = .25f;
        FireFighter.playerTransform = transform.root;
	}

    public void ImmobilizeLegs(NPC_Legs npcLegs) {
        StopAllCoroutines();
        StartCoroutine(Immobilize(npcLegs));
    }

    private IEnumerator Immobilize(NPC_Legs npcLegs) {
        canMove = false;
        Stopwatch timer = new Stopwatch();
        timer.Start();
        while (true) {
            if (timer.Elapsed.Seconds > pickUpTime || !npcLegs.IsBeingPickedUp)
                break; 
            yield return null;
        }
        canMove = true;
    }
	
	void Update () {
        if (canMove) {
            Move ();
        }
        if (Input.GetButtonDown(Controls.Squat) && !isMidSquat) {
            StartCoroutine (Squat());
        }
	}

	void Move(){
		speed = playerBody.velocity.magnitude;
		float forwardAxis = Input.GetAxis(Controls.Forward);
		float sidewaysAxis = Input.GetAxis(Controls.Sideways);

		if (Mathf.Abs (forwardAxis)>=minAxisInput){
			playerBody.AddForce( moveForce * transform.forward * -Mathf.Sign( forwardAxis ), ForceMode.VelocityChange );
		}
		if (Mathf.Abs (sidewaysAxis)>=minAxisInput){
			playerBody.AddForce( moveForce * transform.right * Mathf.Sign( sidewaysAxis ), ForceMode.VelocityChange );
		}
		if (speed >= maxSpeed){
			playerBody.velocity = playerBody.velocity.normalized * maxSpeed;
		}
	}

    IEnumerator Squat() {
        isSquatting = !isSquatting;
        maxSpeed = isSquatting ? squattingMaxSpeed : standingMaxSpeed;
        isMidSquat = true;
        Vector3 targetPosition = isSquatting ? Vector3.down * .5f : Vector3.up * 0.5f;
        while (Vector3.Distance(cameraTran.localPosition, targetPosition)>0.01f) {
            cameraTran.localPosition = Vector3.MoveTowards(cameraTran.localPosition, targetPosition, squatSpeed);
            yield return null;
        }
        cameraTran.localPosition = targetPosition;
        isMidSquat = false;
    }
}
