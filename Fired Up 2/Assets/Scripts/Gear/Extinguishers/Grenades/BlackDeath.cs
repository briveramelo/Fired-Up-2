using UnityEngine;
using System.Collections;
using FU;
using System.Linq;
using System.Diagnostics;
public class BlackDeath : MonoBehaviour {
	
	[SerializeField]	private GameObject grenade;
	[SerializeField]	private GameObject blackHoleEffect;
	[SerializeField]	private GameObject blackHoleSound;
	[SerializeField]	private EffectSettings effectSettings;
	[SerializeField]	private ParticleSystem rockParticles;
	[SerializeField]	private LayerMask layersToPull;

	private float holeRadius;
	private float pullForce;
	private bool singularizing;
    private bool willKillOnCollision;
	private float timeToBeginPulling;
	private float timeToAppear;
	private float timeToSpendPulling;
    private float heightToRise;
    private float riseSpeed;
    private float lightKillTime = 1f;
    private Stopwatch timer;
    private float timeBendTime = 4f;
    private float minimumTimeScale = 0.4f;
    private float maximumTimeScale = 1.5f;
    private float minTimeMult;
    private float maxTimeMult;
    private float distanceToTimeFactor = 7f;

    // Use this for initialization
    void Awake () {
		holeRadius = 6f;
		pullForce = 20f;
		timeToAppear = 3f;
		timeToBeginPulling = 3.5f;
		timeToSpendPulling = 6f;
        heightToRise = .2f;
        riseSpeed = .005f;
        timer = new Stopwatch();

        minTimeMult = maximumTimeScale - 1f;
        maxTimeMult = 1f - minimumTimeScale;

        StartCoroutine (RipThroughSpaceTime());
	}
	
	IEnumerator RipThroughSpaceTime(){
		yield return new WaitForSeconds(timeToAppear);
		blackHoleEffect.transform.rotation = Quaternion.identity;
		blackHoleEffect.SetActive(true);
        Destroy(grenade);
        Destroy(GetComponent<Rigidbody>());

		yield return new WaitForSeconds(timeToBeginPulling-timeToAppear);
        blackHoleSound.SetActive(true);
		singularizing = true;
        StartCoroutine (MoveUp());
        StartCoroutine (PulseLights());
        StartCoroutine (BendTime());
		StartCoroutine (PullInObjects());

		yield return new WaitForSeconds (timeToSpendPulling);
		effectSettings.IsVisible = false;
		singularizing = false;
	}

    IEnumerator BendTime() {
        Stopwatch objectiveWatch = new Stopwatch();
        objectiveWatch.Start();
        float timeElapsed = 0f;
        Vector3 myPosition = transform.position;
        Transform playerTransform = FindObjectOfType<Legs>().transform;
        while ( timeElapsed < timeBendTime) {
            timeElapsed = objectiveWatch.Elapsed.Milliseconds / 1000f;
            Time.timeScale = 1 + Mathf.Cos(timeElapsed * 4f) * DistanceToTimeFactor(myPosition, playerTransform);
            yield return null; 
        }
        yield return null;
    }

    float DistanceToTimeFactor(Vector3 myPosition, Transform playerTransform) {
        float distance = Vector3.Distance(myPosition, playerTransform.position);
        float factor = maxTimeMult - distance / distanceToTimeFactor;
        
        return Mathf.Clamp(factor, minTimeMult, maxTimeMult);
    }

    IEnumerator PulseLights() {
        Vector3 myPosition = transform.position;
        Light[] nearbyLights = FindObjectsOfType<Light>().Where(theLight => Vector3.Distance(theLight.transform.position, myPosition) < holeRadius).ToArray();

        float[] startingIntensities = new float[nearbyLights.Length];
        if (nearbyLights.Length > 0) {
            for (int i = 0; i < nearbyLights.Length; i++) {
                startingIntensities[i] = nearbyLights[i].intensity;
            }
        }

        float ambientIntensityStart = RenderSettings.ambientIntensity;

        timer.Start();
        while( timer.Elapsed.Milliseconds/1000f < lightKillTime) {
            if (nearbyLights.Length > 0){
                for (int i = 0; i < nearbyLights.Length; i++) {
                    nearbyLights[i].intensity -= startingIntensities[i] / lightKillTime * Time.deltaTime;
                }
            }
            RenderSettings.ambientIntensity -= ambientIntensityStart / Time.deltaTime;
            yield return null;
        }

        if (nearbyLights.Length > 0){
            for (int i = 0; i < nearbyLights.Length; i++) {
                nearbyLights[i].intensity =0;
            }
        }
        RenderSettings.ambientIntensity = 0;

        timer.Stop();
        timer.Reset();
        timer.Start();

        while (timer.Elapsed.Milliseconds / 1000f < lightKillTime){
            if (nearbyLights.Length > 0){
                for (int i = 0; i < nearbyLights.Length; i++){
                    nearbyLights[i].intensity += startingIntensities[i] / lightKillTime * Time.deltaTime;
                }
            }
            RenderSettings.ambientIntensity += ambientIntensityStart / Time.deltaTime;
            yield return null;
        }

        if (nearbyLights.Length > 0){
            for (int i = 0; i < nearbyLights.Length; i++) {
                nearbyLights[i].intensity = startingIntensities[i];
            }
        }
        RenderSettings.ambientIntensity = ambientIntensityStart;

        yield return null;
    }

    IEnumerator MoveUp() {
        float distanceAwayFromEnd = 0f;
        while (distanceAwayFromEnd < heightToRise) {
            transform.position += Vector3.up * riseSpeed;
            distanceAwayFromEnd += riseSpeed;
            yield return null;
        }
        yield return null;
    }

	IEnumerator PullInObjects(){
        foreach (Collider col in Physics.OverlapSphere(transform.position, holeRadius, Layers.LayerMasks.allFires.value)){
            FireSpread fireSpread = col.GetComponent<FireSpread>();
            fireSpread.ExtinguishFire();
        }

        while (singularizing){
			foreach (Collider col in Physics.OverlapSphere(transform.position,holeRadius,layersToPull)){
				Vector3 pullDir = (transform.position - col.transform.position).normalized;
				float pullFactor = Vector3.Distance(transform.position,col.transform.position)/holeRadius;
				col.attachedRigidbody.AddForce(pullDir * pullForce * pullFactor);
			}
            
            yield return null;
		}
		yield return null;
        willKillOnCollision = true;
	}
	
	void OnTriggerEnter(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, layersToPull) && willKillOnCollision)
            Destroy(col.gameObject);
	}
}