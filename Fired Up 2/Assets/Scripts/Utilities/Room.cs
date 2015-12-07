using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
    public List<GameObject> startFires = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter()
    {
        //Undo Occulsion
        //Set Fires
        RoomLocator.roomLocator.ChangeTag(this.tag);

    }
    void OnTriggerExit()
    {
        //Enable Occulsion
        //Stop Particles
    }
    void setFires()
    {

    }
}
