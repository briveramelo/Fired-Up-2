using UnityEngine;
using System.Collections;

public class RenderQueueSetter : MonoBehaviour {

	void Start () {
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        for (int i=0; i<renders.Length; i++){
            for (int j = 0; j < renders[i].materials.Length; j++) {
                renders[i].materials[j].renderQueue = 2002;
            }
        }
    }
}
