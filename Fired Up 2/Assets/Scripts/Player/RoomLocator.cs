using UnityEngine;
using System.Collections;

public class RoomLocator : MonoBehaviour {
    public static RoomLocator roomLocator;
	// Use this for initialization
	void Start () {
        roomLocator = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangeTag(string newTag)
    {
        this.tag = newTag;
    }
}
