using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
    public List<Room> nextRooms = new List<Room>();
    List<FireSpread> RoomFires = new List<FireSpread>();
    List<FireSpread> RoomRandomFires = new List<FireSpread>();
    public int RoomFireNumber = 2;
    bool hasBeenSetOnFire;

    void Start()
    {
        hasBeenSetOnFire = false;
    }

    void pickRandomFiresByRoom()
    {
        for (int i= 0; i < RoomFireNumber; i++)
        {
            int temp = Random.Range(0, RoomFires.Count - 1);
                RoomRandomFires.Add(RoomFires[temp]);
        }
    }

    public void LightThisRoomOnFire()
    {
        if (!hasBeenSetOnFire)
        {
            RoomFires = RoomController.GetRoomFires(this.tag);
            pickRandomFiresByRoom();
            setFires(RoomRandomFires);
        }
    }

    void OnTriggerEnter(Collider col){
        //Logic for collision checks is in the Physics Layer collision pyramid
        //if (col.gameObject.layer == 26)
        col.GetComponent<RoomLocator>().ChangeTag(this.tag);
        if (col.name == "RoomLocator")
        {
            RoomController.SelectRoomsToSetOnFire(this);
        }
    }

    void OnTriggerExit(){
        //Enable Occulsion
        //Stop Particles
    }

    void setFires(List<FireSpread> fires)
    {
        for(int i = 0; i < fires.Count; i++)
        {
            fires[i].CatchFire();
        }
       hasBeenSetOnFire = true;
    }
}
