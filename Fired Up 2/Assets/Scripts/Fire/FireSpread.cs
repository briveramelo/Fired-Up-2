using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
using System.Linq;


public class FireSpread : MonoBehaviour {

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
    public float extingishTime = 0;
    int counterforDebug = 0;

    void Update()
    {
       // if (isOnFire)
      //  {
        //    Debug.Log(this.transform.position + "  " + this.name);
         //   printLocalFires();
      //  }

    }
    void Start() {
        //throwFire = new Task(SetFires(), false);
        thisCollider = GetComponent<Collider>();
        fireRadius = 1f;
        Collider[] nearbyFireColliders = Physics.OverlapSphere(transform.position, fireRadius, Layers.LayerMasks.allFires.value);
        nearbyFireColliders = nearbyFireColliders.Where(col => col != thisCollider).ToArray();
        //nearbyFireSpreadScripts = new FireSpread[nearbyFireColliders.Length];
        for (int i = 0; i < nearbyFireColliders.Length; i++) {
            
                if (this.tag.Equals(nearbyFireColliders[i].tag))
                {
                    nearbyFireSpreadScripts.Add(nearbyFireColliders[i].GetComponent<FireSpread>());
                   
                }
            
        }
        
        if (isOnFire)
        {
           // printLocalFires();
CatchFire();
        }
        
    }

    public void CatchFire() {
        myFireParts.SetActive(true);
        fireEffectSettings.IsVisible = true;
        isOnFire = true;
        throwFire = new Task(SetFires(), true);
        
       // Debug.Log(this.transform.position + "  " + this.name + "Calling CoRoutine + catch fire");
        //StartCoroutine(SetFires());
    }

    IEnumerator SetFires() {
        float fireRandomTime = Random.Range(timeItTakesToCatchFire -2,timeItTakesToCatchFire + 2);
            yield return new WaitForSeconds(fireRandomTime);
           // Debug.Log(this.transform.position + "  " + this.name + "Begining of CoRoutine");
            if (nearbyFireSpreadScripts.Count > 0 && isOnFire)
            {
                for (int i = 0; i < nearbyFireSpreadScripts.Count; i++)
                {
                    if ((!nearbyFireSpreadScripts[i].isOnFire) && 
                        (Random.Range(0, 1) < .6) && 
                        ((Time.time - nearbyFireSpreadScripts[i].extingishTime) > waitTimeAfterExtingishing))
                    {
                        nearbyFireSpreadScripts[i].CatchFire();
                    }
                }
            }
        throwFire = new Task(SetFires(), true);
       // Debug.Log(this.transform.position + "  " + this.name + "About to restart CoRoutine" + counterforDebug++);
            //StartCoroutine(SetFires());
           
        }
        
    
        
        
    public void ExtinguishFire(){
       // myFireParts.SetActive(false);
        extingishTime = Time.time;
       if(isOnFire)
        {
            
            fireEffectSettings.IsVisible = false;
        isOnFire = false;
        
         throwFire.Stop();
       // Debug.Log(this.transform.position + "  " + this.name + "Put out");
       // Debug.Log(this.transform.position + "  " + this.name + "I was put out" + isOnFire);
        }
        
    }

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + Vector3.up*.5f,fireRadius);
	}




	void printLocalFires(){
		if(nearbyFireSpreadScripts.Count > 0){
			for(int i = 0; i < nearbyFireSpreadScripts.Count-1; i++)   //foreach (FireSpread fire in fireList){
			Debug.Log(this.name +"With tag "+ tag + "Can spawn" + nearbyFireSpreadScripts[i].name + "with tag" + nearbyFireSpreadScripts[i].tag);
		}
	}
}
