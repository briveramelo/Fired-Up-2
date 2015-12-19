using UnityEngine;
using System.Collections;
using FU;

public abstract class DangerZone : MonoBehaviour {

    public bool hasZoneBeenTriggered;
    public bool HasZoneBeenTriggered { get { return hasZoneBeenTriggered; } }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == Layers.Rooms.roomLocator && !hasZoneBeenTriggered) {
            Debug.Log("dangerZOOONE");
            hasZoneBeenTriggered = true;
            TriggerZone();
        }
    }

    protected abstract void TriggerZone();

}
