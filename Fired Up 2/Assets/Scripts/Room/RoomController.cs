using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {
    public static Room[] rooms = new Room[20];
    static FireSpread[] allFires = new FireSpread[400];
    public static difficulty LevelDifficulty;
    public enum difficulty { Easy, Normal, Hard };
    // Use this for initialization
    void Start () {
        rooms = FindObjectsOfType<Room>();
        allFires = FindObjectsOfType<FireSpread>();
        LevelDifficulty = difficulty.Hard;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    static List<FireSpread> SortListsByTag(List<FireSpread> RoomFires, string tag)
    {
        for (int i = 0; i < allFires.Length; i++)
        {
            if (allFires[i] != null)
            {
                if (allFires[i].gameObject.tag == tag)
                    RoomFires.Add(allFires[i]);
            }

            else
                break;
        }
        return RoomFires;
    }
    public static List<FireSpread> GetRoomFires(string tag)
    {
        //Debug.Log(allFires.Length + "=" + (Room1.Count + Room2.Count + Room3.Count));
        //IENumorator for waiting until all fires are sorted.
        List<FireSpread> RoomFires = new List<FireSpread>();
        RoomFires = SortListsByTag(RoomFires, tag);
        return RoomFires;
    }
    public static void SelectRoomsToSetOnFire(Room currentRoom)
    {
        List<Room> nextRooms = currentRoom.nextRooms;
        if (LevelDifficulty == difficulty.Easy)
        {
            currentRoom.LightThisRoomOnFire();
        }
        if (LevelDifficulty == difficulty.Normal)
        {
            currentRoom.LightThisRoomOnFire();
            for (int i = 0; i < nextRooms.Count; i++)
            {
                nextRooms[i].LightThisRoomOnFire();
            }
        }
        if (LevelDifficulty == difficulty.Hard)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i] != null)
                {
                    rooms[i].LightThisRoomOnFire();
                }
            }
        }
    }
}
