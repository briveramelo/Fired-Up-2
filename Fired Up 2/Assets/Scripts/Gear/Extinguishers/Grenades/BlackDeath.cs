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
    private float maxDistanceBeforeZeroTimeScale = 7f;
    private float distanceToTimeFactor;
    private float maxTimeMult;
    private float timeFactor;
    private float extinguishedTime;

    void Awake () {
		holeRadius = 6f;
		pullForce = 20f;
		timeToAppear = 3f;
		timeToBeginPulling = 3.5f;
		timeToSpendPulling = 6f;
        heightToRise = .2f;
        riseSpeed = .005f;
        timer = new Stopwatch();
        extinguishedTime = 12f;
        maxTimeMult = 1f - minimumTimeScale;
        distanceToTimeFactor = maxTimeMult / maxDistanceBeforeZeroTimeScale;

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
            timeFactor = DistanceToTimeFactor(myPosition, playerTransform);
            Time.timeScale = 1 + Mathf.Cos(timeElapsed * timeFactor) * timeFactor;
            yield return null; 
        }
        Time.timeScale = 1;
        yield return null;
    }

    float DistanceToTimeFactor(Vector3 myPosition, Transform playerTransform) {
        float distance = Vector3.Distance(myPosition, playerTransform.position);
        float factor = maxTimeMult - distance * distanceToTimeFactor;
        
        return Mathf.Clamp(factor, 0, maxTimeMult);
    }

    #region PulseLights
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
    #endregion

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
        Collider[] extingishableColliders = Physics.OverlapSphere(transform.position, holeRadius, Layers.LayerMasks.allFires.value).Where(col => CompareTag(col.tag)).ToArray();
        foreach (Collider col in extingishableColliders) {
            FireSpread fireSpread = col.GetComponent<FireSpread>();
            fireSpread.ExtinguishFire(extinguishedTime);
        }

        willKillOnCollision = true;
        while (singularizing){
			foreach (Collider col in Physics.OverlapSphere(transform.position,holeRadius,layersToPull)){
				Vector3 pullDir = (transform.position - col.transform.position).normalized;
				float pullFactor = Vector3.Distance(transform.position,col.transform.position)/holeRadius;
				col.attachedRigidbody.AddForce(pullDir * pullForce * pullFactor);
			}
            
            yield return null;
		}
		yield return null;
	}
	
	void OnTriggerEnter(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, layersToPull) && willKillOnCollision) {
            if (col.gameObject.layer == Layers.People.you)
                Player.player.KillPlayer();
        }
            
	}
}