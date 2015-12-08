using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {
    BoxCollider trigger;
    float timeUnitlTrapFalls = 2f;
   public bool active = true;
	// Use this for initialization
	void Start () {
        trigger = this.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.name.Equals("FireFighter") && active)
        {
            Debug.Log("Trap Triggered");
            
                StartCoroutine(TrapCountdown());
        }
            
    }
    IEnumerator TrapCountdown()
    {
        Destroy(trigger);
        active = false;
        yield return new WaitForSeconds(timeUnitlTrapFalls);
        ActivateTrap();
        yield return null;
    }
    void ActivateTrap()
    {
        this.gameObject.AddComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("FireFighter"))
        {
            Debug.Log("Player Died");
        }
    }
}
