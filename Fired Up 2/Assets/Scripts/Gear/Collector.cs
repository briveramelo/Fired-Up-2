using UnityEngine;
using System.Collections;
using FU;
public class Collector : MonoBehaviour {

    LayerMask collectableMask;

    void Awake() {
        collectableMask = Layers.LayerMasks.allCollectables;
    }

    void OnTriggerEnter(Collider col) {
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, collectableMask)) {
            Collectable itemToCollect = col.GetComponent<Collectable>();
            itemToCollect.GetCollected();
        }
    }
}
