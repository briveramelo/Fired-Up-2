using UnityEngine;
using System.Collections;
using FU;

public class GrenadeTosser : Inventory {
	
	[SerializeField]	private GameObject k_Bomb;
	[SerializeField]	private GameObject blackDeath;

	private float throwForce;
	private float timeToWaitToThrow;
	private bool canThrowGrenade;
    private float deadZone=.3f;
    private float lastAxis;

	// Use this for initialization
	void Awake () {
		throwForce = 500f;
		timeToWaitToThrow = 2f;
		canThrowGrenade = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw (Controls.ThrowGrenade)>=deadZone && GearInventory[CurrentGrenade] >0 &&
		    (CurrentGrenade == Gear.K_Bomb || CurrentGrenade == Gear.BlackDeath) &&
		    canThrowGrenade && lastAxis<deadZone){

			StartCoroutine (ThrowThing());
		}
        lastAxis = Input.GetAxisRaw(Controls.ThrowGrenade);
    }

	IEnumerator ThrowThing(){
		GameObject objectToSpawn = CurrentGrenade == Gear.K_Bomb ? k_Bomb : blackDeath;
        UpdateAmmo(CurrentGrenade, -1);
        canThrowGrenade = false;
		GameObject grenade = Instantiate(objectToSpawn, transform.position, Quaternion.identity) as GameObject;
		grenade.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * throwForce);

		yield return new WaitForSeconds(timeToWaitToThrow);

		canThrowGrenade = true;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * 3f);
	}
}
