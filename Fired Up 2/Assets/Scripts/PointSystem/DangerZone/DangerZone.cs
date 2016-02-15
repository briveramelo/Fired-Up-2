using UnityEngine;
using System.Collections;
using FU;

public abstract class DangerZone : MonoBehaviour {

    private bool hasZoneBeenTriggered;
    public bool HasZoneBeenTriggered { get { return hasZoneBeenTriggered; } }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == Layers.Rooms.roomLocator && !hasZoneBeenTriggered) {
            hasZoneBeenTriggered = true;
            TriggerZone();
        }
    }

    protected abstract void TriggerZone();

}
