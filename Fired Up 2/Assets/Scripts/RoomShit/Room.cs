using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
    public List<GameObject> startFires = new List<GameObject>();
	
    void OnTriggerEnter(Collider col){
        //Logic for collision checks is in the Physics Layer collision pyramid
        col.GetComponent<RoomLocator>().ChangeTag(this.tag);
    }

    void OnTriggerExit(){
        //Enable Occulsion
        //Stop Particles
    }

    void setFires(){

    }
}
