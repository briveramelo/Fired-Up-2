using UnityEngine;
using System.Collections;

public class OculusReticle : MonoBehaviour {

    [SerializeField] Renderer reticleRenderer;
    bool hasBeenCalled;

    public static float timeToSelect = 2.5f;
    private float targetCutOff = 1f;
    private float selectionRate;

	void Start () {
        reticleRenderer.material.SetFloat("_Cutoff", .01f);
        hasBeenCalled = false;
        selectionRate = targetCutOff / timeToSelect;
    }
	
    void OnTriggerStay(Collider col){
        if (col.gameObject.GetComponent<IRiftSelectable>() != null){
            IRiftSelectable riftInterface = col.gameObject.GetComponent<IRiftSelectable>();
            if (riftInterface.IsSelectable()) {
                reticleRenderer.material.SetFloat("_Cutoff", reticleRenderer.material.GetFloat("_Cutoff") + selectionRate * Time.deltaTime);
                riftInterface.OnHoverOverObject();
                if (reticleRenderer.material.GetFloat("_Cutoff") >= targetCutOff){
                    if (!hasBeenCalled){
                        hasBeenCalled = true;
                        riftInterface.OnHoldForEnoughTime();
                        reticleRenderer.material.SetFloat("_Cutoff", .01f);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.GetComponent<IRiftSelectable>() != null){
            IRiftSelectable riftInterface = col.gameObject.GetComponent<IRiftSelectable>();
            riftInterface.OnHoverExitObject();
        }
        reticleRenderer.material.SetFloat("_Cutoff", .01f);
        hasBeenCalled = false;
    }
}
