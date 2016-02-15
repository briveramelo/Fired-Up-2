using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
using System.Linq;


public class FireSpread : MonoBehaviour {

    public bool isOnFire;
    [SerializeField] private EffectSettings fireEffectSettings;
    [SerializeField] private GameObject myFireParts;
    public FireSpread[] nearbyFireSpreadScripts;
    private float fireRadius;
    private Collider thisCollider;
    private float timeItTakesToCatchFire = 7f;
    float waitTimeAfterExtingishing = 5f;
    [HideInInspector] public float timeOfLastExtinguish = 0;
    int currentPointValue = 100;
    bool FiresOnlyLightFiresInTheirRoom = true;

    void Start() {
        thisCollider = GetComponent<Collider>();
        fireRadius = 1f;

        Invoke ("FindNeighborFires", 1f);

        if (isOnFire) {
            CatchFire();
        }
        StartCoroutine(SetFires());
    }

    void FindNeighborFires() {
        Collider[] nearbyFireColliders = Physics.OverlapSphere(transform.position, fireRadius, Layers.LayerMasks.allFires.value).Where(col => col != thisCollider && col.CompareTag(tag)).ToArray();
        nearbyFireSpreadScripts = new FireSpread[nearbyFireColliders.Length];
        for (int i = 0; i < nearbyFireColliders.Length; i++){
            nearbyFireSpreadScripts[i] = nearbyFireColliders[i].GetComponent<FireSpread>();
        }
    }

    public void CatchFire() {
        myFireParts.SetActive(true);
        fireEffectSettings.IsVisible = true;
        isOnFire = true;
        StopAllCoroutines();
        StartCoroutine(SetFires());
    }

    IEnumerator SetFires() {
        float fireRandomTime = Random.Range(timeItTakesToCatchFire -2,timeItTakesToCatchFire + 2);
        yield return new WaitForSeconds(fireRandomTime);
        if (nearbyFireSpreadScripts.Length > 0 && isOnFire){
            for (int i = 0; i < nearbyFireSpreadScripts.Length; i++){
                if ((!nearbyFireSpreadScripts[i].isOnFire) && (Random.Range(0, 1) < .6) && 
                    ((Time.time - nearbyFireSpreadScripts[i].timeOfLastExtinguish) > waitTimeAfterExtingishing)){
                    nearbyFireSpreadScripts[i].CatchFire();
                }
            }
        }
        StartCoroutine(SetFires());
    }
    
    public void ExtinguishFire(float extinguisherWaitValue){
        waitTimeAfterExtingishing = extinguisherWaitValue;
        timeOfLastExtinguish = Time.time;
        if(isOnFire) {
            fireEffectSettings.IsVisible = false;
            isOnFire = false;
            ComboTracker.Instance.AddToFireCombo(transform.position, currentPointValue);
        }
    }
}
