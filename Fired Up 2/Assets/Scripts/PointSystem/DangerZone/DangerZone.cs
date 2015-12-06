using UnityEngine;
using System.Collections;
using FU;

public abstract class DangerZone : MonoBehaviour {

    bool hasZoneBeenTriggered;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == Layers.People.you && !hasZoneBeenTriggered) {
            hasZoneBeenTriggered = true;
            TriggerZone();
        }
    }

    protected abstract void TriggerZone();

}
