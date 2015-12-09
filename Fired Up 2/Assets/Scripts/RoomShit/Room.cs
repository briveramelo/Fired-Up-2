using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
    public bool useInspectorFires = false;
    public List<FireSpread> startFires = new List<FireSpread>();
    FireSpread[] allFires = new FireSpread[400];
    List<FireSpread> Room1 = new List<FireSpread>();
    List<FireSpread> Room2 = new List<FireSpread>();
    List<FireSpread> Room3 = new List<FireSpread>();
    List<FireSpread> Room1Fires = new List<FireSpread>();
    List<FireSpread> Room2Fires = new List<FireSpread>();
    List<FireSpread> Room3Fires = new List<FireSpread>();
    public int Room1FireNumber = 2;
    public int Room2FireNumber = 5;
    public int Room3FireNumber= 1;
    int firesTagged;
    public enum difficulty { Easy, Normal, Hard};
    void Start()
    {
        

    }
    void pickRandomFiresByRoom()
    {
        Debug.Log("Room1 " + Room1FireNumber);
        Debug.Log("Room2 " + Room2FireNumber);
        Debug.Log("Room3 " + Room3FireNumber);
        for (int i= 0; i < Room1FireNumber; i++)
        {
            int temp = Random.Range(0, Room1.Count - 1);
                Room1Fires.Add(Room1[temp]);
            Debug.Log("For " + i + " " + Room1[temp] + "was chosen");
        }
        for (int i = 0; i < Room2FireNumber; i++)
        {
            Room2Fires.Add(Room2[Random.Range(0, Room2.Count - 1)]);
        }
        for (int i = 0; i < Room3FireNumber; i++)
        {
            Room3Fires.Add(Room3[Random.Range(0, Room3.Count - 1)]);
        }
    }
    void SortListsByTag()
    {
        for (int i = 0; i < allFires.Length; i++)
        {
            if(allFires[i] != null)
            {
                if (allFires[i].gameObject.tag == "Level1Room1")
                    Room1.Add(allFires[i]);
                if (allFires[i].gameObject.tag == "Level1Room2")
                    Room2.Add(allFires[i]);
                if (allFires[i].gameObject.tag == "Level1Room3")
                    Room3.Add(allFires[i]);
            }
        
        else
        break;
        }
    }
    void OnTriggerEnter(Collider col){
        //Logic for collision checks is in the Physics Layer collision pyramid
        //if (col.gameObject.layer == 26)
        col.GetComponent<RoomLocator>().ChangeTag(this.tag);
        if (col.name == "RoomLocator")
        {
            if (useInspectorFires && startFires.Count > 0)
            {
                setFires(startFires);
            }
            else
            {
                allFires = FindObjectsOfType<FireSpread>();
                SortListsByTag();
                Debug.Log(allFires.Length);
                Debug.Log("Room1 whole count:" + Room1.Count);
                Debug.Log("Room2 whole count:" + Room2.Count);
                Debug.Log("Room3 whole count:" + Room3.Count);
                Debug.Log(allFires.Length + "=" + (Room1.Count + Room2.Count + Room3.Count));
               // if (allFires.Length != Room1.Count + Room2.Count + Room3.Count)
                //{
                    pickRandomFiresByRoom();
                    Debug.Log(Room1Fires.Count);
                    Debug.Log(Room2Fires.Count);
                    Debug.Log(Room3Fires.Count);
                
                    if (col.tag == "Level1Room1")
                        setFires(Room1Fires);
                    if (col.tag == "Level1Room2")
                        setFires(Room2Fires);
                    if (col.tag == "Level1Room3")
                        setFires(Room3Fires);
               // }
            }

            
        }
    }

    void OnTriggerExit(){
        //Enable Occulsion
        //Stop Particles
    }

    void setFires(List<FireSpread> fires)
    {
        Debug.Log("This was called for" + fires.Count);
        for(int i = 0; i < fires.Count; i++)
        {
            Debug.Log("I set fire too" + fires[i].name + "" + fires[i].transform.position);
            fires[i].CatchFire();
        }
    }
}
