using UnityEngine;
using System.Collections;
using FU;
using System.Linq;
public class RoomLocator : MonoBehaviour {

    [SerializeField] GameObject gameObjectToTag = null;
    public static RoomLocator PlayerRoomLocator;
    bool hasBeenLocated;
	// Use this for initialization
	void Start () {
        if (transform.root.gameObject.layer == Layers.People.you)
            PlayerRoomLocator = this;
    }
	
    public void ChangeTag(string newTag){
        if (gameObjectToTag != null)
            gameObjectToTag.tag = newTag;
        else
            gameObject.tag = newTag;
        if (gameObject.layer == 26 && gameObject.name == "FireParts")
        {
            if(!gameObject.GetComponentInParent<FireSpread>().isOnFire)
gameObject.SetActive(false);
            gameObject.layer = 11;
            Destroy(this);
        }
            
        
    }
}
