using UnityEngine;
using System.Collections;
using FU;
public class RoomLocator : MonoBehaviour {
    public static RoomLocator roomLocator;
	// Use this for initialization
	void Start () {
        if (transform.root.gameObject.layer == Layers.People.you)
            roomLocator = this;
	}
	
    public void ChangeTag(string newTag){
        this.tag = newTag;
    }
}
