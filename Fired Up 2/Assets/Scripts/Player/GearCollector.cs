using UnityEngine;
using System.Collections;
using FU;
public class GearCollector : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allCollectables)) {
            Collectable itemToCollect = col.GetComponent<Collectable>();
            itemToCollect.GetCollected();
        }
    }
}
