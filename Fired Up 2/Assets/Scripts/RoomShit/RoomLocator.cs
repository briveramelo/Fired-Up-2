using UnityEngine;
using System.Collections;
using FU;
using System.Linq;
public class RoomLocator : MonoBehaviour {
    [SerializeField] GameObject gameObjectToTag;
    public static RoomLocator PlayerRoomLocator;
	// Use this for initialization
	void Start () {
        Collider[] rooms = Physics.OverlapSphere(transform.position, 0.1f);
        Collider room = rooms.Where(col => col.GetComponent<Room>()).ToArray()[0];
        if (room!=null)
            ChangeTag(room.tag);

        if (transform.root.gameObject.layer == Layers.People.you)
            PlayerRoomLocator = this;

    }
	
    public void ChangeTag(string newTag){
        if (gameObjectToTag != null)
            gameObjectToTag.tag = newTag;
        else
            gameObject.tag = newTag;
        
        //(gameObjectToTag ?? gameObject).tag = newTag;
    }
}
