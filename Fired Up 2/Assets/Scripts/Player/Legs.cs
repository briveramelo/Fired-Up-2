using UnityEngine;
using System.Collections;
using FU;
using System.Diagnostics;

public class Legs : MonoBehaviour {

	[SerializeField]	private	Rigidbody playerBody;
	[SerializeField]	private Transform feetTran;
	private float minAxisInput;
	private float speed;
	private float maxSpeed;
	private float jumpForce;
	private float moveForce;
    private bool canMove = true;
    private float pickUpTime = 2f;

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

	// Use this for initialization
	void Awake () {
		minAxisInput = 0.15f;
		maxSpeed = 5f;
		moveForce = .15f;
		jumpForce = 500f;
        FireFighter.playerTransform = transform.root;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            Move ();
		    Jump();
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

	void Jump(){
		if (Input.GetButtonDown (Controls.Jump)){
			if (Physics.CheckSphere(feetTran.position,.5f,Layers.LayerMasks.ground.value)){
				playerBody.velocity = new Vector3 (playerBody.velocity.x,0f,playerBody.velocity.z);
                playerBody.AddForce (jumpForce * Vector3.up);
			}
		}
	}


}
