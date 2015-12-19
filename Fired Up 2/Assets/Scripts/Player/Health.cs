 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class Health : MonoBehaviour {

    public float health = 100f;
    List<FireSpread> firesInRadius = new List<FireSpread>();
	
	void Update () {
        if(health <= 0){
            if(gameObject.layer == 20)
                Player.player.KillPlayer();
        }
	    else if(firesInRadius.Count > 0){
            CalculateHealth();
        }
	}

    void CalculateHealth(){
        for(int i = 0; i < firesInRadius.Count; i++){
            if (firesInRadius[i] != null && firesInRadius[i].isOnFire && firesInRadius[i].tag == RoomLocator.PlayerRoomLocator.tag){
                health -= 1/(((transform.position - firesInRadius[i].transform.position).magnitude));
            }
        }
    }

    public void DamagePlayer(float amount){
        health -= amount;
    }

    void OnTriggerEnter(Collider col){
          if (LayerMaskExtensions.IsInLayerMask(col.gameObject,Layers.LayerMasks.allFires))
            firesInRadius.Add(col.GetComponent<FireSpread>());
    }

    void OnTriggerExit(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allFires))
            firesInRadius.Remove(col.GetComponent<FireSpread>());
    }
}
