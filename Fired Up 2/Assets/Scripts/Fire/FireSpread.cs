using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
using System.Linq;


public class FireSpread : MonoBehaviour {

    //[HideInInspector]
    public bool isOnFire;
    private List<FireSpread> nearbyFireSpreadScripts = new List<FireSpread>();
    [SerializeField] private EffectSettings fireEffectSettings;
    [SerializeField] private GameObject myFireParts;
    private float fireRadius;
    private Collider thisCollider;
    private float timeItTakesToCatchFire = 7f;
    float waitTimeAfterExtingishing = 5f;
    //TaskManager task = new TaskManager();
    Task throwFire;
    [HideInInspector] public float timeOfLastExtinguish = 0;
    int counterforDebug = 0;
    int currentPointValue = 100;
    private bool isOxygen = true;
    bool FiresOnlyLightFiresInTheirRoom = true;
    Task wait;

    void Start() {
        //throwFire = new Task(SetFires(), false);
        thisCollider = GetComponent<Collider>();
        fireRadius = 1f;

        Collider[] nearbyFireColliders = Physics.OverlapSphere(transform.position, fireRadius, Layers.LayerMasks.allFires.value);
        nearbyFireColliders = nearbyFireColliders.Where(col => col != thisCollider).ToArray();
        
        //nearbyFireSpreadScripts = new FireSpread[nearbyFireColliders.Length];
        for (int i = 0; i < nearbyFireColliders.Length; i++) {
           // if (this.tag.Equals(nearbyFireColliders[i].tag)){ //Removed do to dynamic tagging of fires
                nearbyFireSpreadScripts.Add(nearbyFireColliders[i].GetComponent<FireSpread>());
          //  }
        }
        
        if (isOnFire){
           // printLocalFires();
            CatchFire();
        }
    }
    void FindFiresInMyRoom()
    {
        for (int i = 0; i < nearbyFireSpreadScripts.Count; i++)
        {
            if (!this.tag.Equals(nearbyFireSpreadScripts[i].tag))
            {
                Debug.Log(this.tag + "removed" + (nearbyFireSpreadScripts[i].tag));
                nearbyFireSpreadScripts.RemoveAt(i);
            }
        }
    }
    void Update(){
        // if (isOnFire){
        //      Debug.Log(this.transform.position + "  " + this.name);
        //      printLocalFires();
        // }
    }


    public void CatchFire() {
        if (isOxygen) {
            myFireParts.SetActive(true);
            fireEffectSettings.IsVisible = true;
            isOnFire = true;
            throwFire = new Task(SetFires(), true);
        }
        
        //Debug.Log(this.transform.position + "  " + this.name + "Calling CoRoutine + catch fire");
        //StartCoroutine(SetFires());
    }

    IEnumerator SetFires() {
        float fireRandomTime = Random.Range(timeItTakesToCatchFire -2,timeItTakesToCatchFire + 2);
        yield return new WaitForSeconds(fireRandomTime);
        //Debug.Log(this.transform.position + "  " + this.name + "Begining of CoRoutine");
        if (nearbyFireSpreadScripts.Count > 0 && isOnFire)
        {
            if(FiresOnlyLightFiresInTheirRoom)
            FindFiresInMyRoom();
            for (int i = 0; i < nearbyFireSpreadScripts.Count; i++)
            {
                if ((!nearbyFireSpreadScripts[i].isOnFire) && 
                    (Random.Range(0, 1) < .6) && 
                    ((Time.time - nearbyFireSpreadScripts[i].timeOfLastExtinguish) > waitTimeAfterExtingishing))
                {
                    //Debug.Log(this.tag + "Spread fire to" + nearbyFireSpreadScripts[i].tag);
                    if (this.tag.Equals(nearbyFireSpreadScripts[i].tag)) // added because of dynamic fire tagging
                        nearbyFireSpreadScripts[i].CatchFire();
                }
            }
        }
        throwFire = new Task(SetFires(), true);
        //Debug.Log(this.transform.position + "  " + this.name + "About to restart CoRoutine" + counterforDebug++);
        //StartCoroutine(SetFires());
    }
    
    public void ExtinguishFire(){
        // myFireParts.SetActive(false);
        timeOfLastExtinguish = Time.time;
        if(isOnFire) {
            fireEffectSettings.IsVisible = false;
            isOnFire = false;
            ComboTracker.Instance.AddToFireCombo(transform.position, currentPointValue);
        
            throwFire.Stop();
            // Debug.Log(this.transform.position + "  " + this.name + "Put out");
            // Debug.Log(this.transform.position + "  " + this.name + "I was put out" + isOnFire);
        }
        
    }

	void printLocalFires(){
		if(nearbyFireSpreadScripts.Count > 0){
			for(int i = 0; i < nearbyFireSpreadScripts.Count-1; i++)    //foreach (FireSpread fire in fireList){
			Debug.Log(this.name +"With tag "+ tag + "Can spawn" + nearbyFireSpreadScripts[i].name + "with tag" + nearbyFireSpreadScripts[i].tag);
		}
	}

    public void SupplyOxygen(bool oxygen, float timeToWait) {
        isOxygen = oxygen;
        if (!oxygen) {
            if (wait!=null)
                wait.Stop();
            wait = new Task(WaitForOxygen(timeToWait), true);
        }
    }

    IEnumerator WaitForOxygen(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        isOxygen = true;
    }


}
