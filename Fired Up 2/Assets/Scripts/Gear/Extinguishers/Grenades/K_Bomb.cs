using UnityEngine;
using System.Collections;
using FU;
using System.Linq;

public class K_Bomb : MonoBehaviour {

    #region Init
    [SerializeField]	private GameObject iceExplosion;
	private float timeToExplode;
	private float explosionRadius;
    #endregion

    void Awake(){
		timeToExplode= 2f;
		explosionRadius = 5f;
		StartCoroutine (Explode());
	}

	IEnumerator Explode(){
		yield return new WaitForSeconds(timeToExplode);
		iceExplosion.transform.rotation = Quaternion.Euler(-90f,0f,0f);
		iceExplosion.transform.position = transform.position;
		iceExplosion.SetActive(true);

        Collider[] extingishableColliders = Physics.OverlapSphere(transform.position, explosionRadius, Layers.LayerMasks.allFires.value).Where(col => CompareTag(col.tag)).ToArray();
        Collider[] playerOrAI = Physics.OverlapSphere(transform.position, explosionRadius, Layers.LayerMasks.allPeople.value).ToArray();
        Debug.Log(playerOrAI.Length);
        for (int i = 0; i < playerOrAI.Length; i++)
        {
            playerOrAI[i].GetComponentInChildren<Health>().DamagePlayer((1/(playerOrAI[i].transform.position - this.transform.position).magnitude)*100);//1/(((this.transform.position - firesInRadius[i].transform.position).magnitude)
            Debug.Log("I dealt " + (1 / ((playerOrAI[i].transform.position - this.transform.position).magnitude) * 100) + "to " + playerOrAI[i].name);
        }
        foreach (Collider col in extingishableColliders){
            FireSpread firespread = col.GetComponent<FireSpread>();
            firespread.ExtinguishFire();
            firespread.SupplyOxygen(false, 4f);
            
        }
		Destroy(gameObject);
	}
	
}
