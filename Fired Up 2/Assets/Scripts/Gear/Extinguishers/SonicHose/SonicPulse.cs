using UnityEngine;
using System.Collections;
using FU;

public class SonicPulse : MonoBehaviour {
    [SerializeField] Rigidbody pulseBody;
    private float pulseSpeed =20f;

    public void Launch(Vector3 launchDirection) {
        pulseBody.velocity = launchDirection * pulseSpeed;
        StartCoroutine(Pulse());
    }

    void OnTriggerEnter(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allFires))
            col.GetComponent<FireSpread>().ExtinguishFire();
    }

    IEnumerator Pulse() {
        while (transform.localScale.x<1.15f) {
            transform.localScale += Vector3.one * 0.01f;
            yield return null;
        }
        while (transform.localScale.x > 0f)
        {
            transform.localScale -= Vector3.one * 0.04f;
            yield return null;
        }
    }
}
