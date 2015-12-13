 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
public class Health : MonoBehaviour {
    public float health = 100f;
    List<FireSpread> firesInRadius = new List<FireSpread>();
	// Use this for initialization
	void Awake () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            if(gameObject.layer == 20)
            Player.player.KillPlayer();
            else
            Debug.Log("The NPC is dead"); // Kill AI animator
        }
	    else if(firesInRadius.Count > 0)
        {
            CalculateHealth();
        }
	}
    void CalculateHealth()
    {
        for(int i = 0; i < firesInRadius.Count; i++)
        {
            if(firesInRadius[i].isOnFire && firesInRadius[i].tag == RoomLocator.PlayerRoomLocator.tag){
                health -= 1/(((this.transform.position - firesInRadius[i].transform.position).magnitude));//fix mag inverse
                //Debug.Log(health);
            }
            
        }
    }
    public void DamagePlayer(float amount)
    {
        health -= amount;
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.tag);
         
          if (LayerMaskExtensions.IsInLayerMask(col.gameObject,Layers.LayerMasks.allFires))
            firesInRadius.Add(col.GetComponent<FireSpread>());
    }
    void OnTriggerExit(Collider col)
    {
        // Debug.Log(col.name);
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allFires))
            firesInRadius.Remove(col.GetComponent<FireSpread>());
    }
}
