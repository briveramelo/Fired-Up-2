using UnityEngine;
using System.Collections;
using FU;
using System.Linq;

public class K_Bomb : MonoBehaviour {

    [SerializeField]	private GameObject iceExplosion;
	private float timeToExplode;
    private float extinguishedTime;
	private float explosionRadius;

    void Awake(){
        extinguishedTime = 9f;
        timeToExplode= 2f;
		explosionRadius = 5f;
		StartCoroutine (Explode());
	}

	IEnumerator Explode(){
		yield return new WaitForSeconds(timeToExplode);
		iceExplosion.transform.rotation = Quaternion.Euler(-90f,0f,0f);
		iceExplosion.transform.position = transform.position;
		iceExplosion.SetActive(true);

        Collider[] extingishableColliders = Physics.OverlapSphere(transform.position, explosionRadius, Layers.LayerMasks.allFires.value).Where(col => CompareTag(col.tag) && col.GetComponent<FireSpread>()).ToArray();
        Collider[] playerOrAI = Physics.OverlapSphere(transform.position, explosionRadius, Layers.LayerMasks.allPeople.value).ToArray();
        for (int i = 0; i < playerOrAI.Length; i++){
            playerOrAI[i].GetComponentInChildren<Health>().DamagePlayer((1/(playerOrAI[i].transform.position - this.transform.position).magnitude)*100);
        }
        foreach (Collider col in extingishableColliders){
            FireSpread firespread = col.GetComponent<FireSpread>();
            firespread.ExtinguishFire(extinguishedTime);
        }
		Destroy(gameObject);
	}
	
}
