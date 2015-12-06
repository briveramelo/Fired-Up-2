using UnityEngine;
using System.Collections;
using FU;
public class K_Bomb : MonoBehaviour {

    #region Init
    [SerializeField]	private GameObject iceExplosion;
	[SerializeField]	private float timeToExplode;
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
		foreach (Collider col in Physics.OverlapSphere(transform.position,explosionRadius, Layers.LayerMasks.allFires.value)){
            FireSpread firespread = col.GetComponent<FireSpread>();
            firespread.ExtinguishFire();
		}
		Destroy(gameObject);
	}
	
}
