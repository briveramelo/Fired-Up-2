﻿using UnityEngine;
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
    public int Room1FireNumber;
    public int Room2FireNumber;
    public int Room3FireNumber;
    int firesTagged;

    void Start()
    {
        if(useInspectorFires && startFires.Count > 0)
        {
            setFires(startFires);
        }
        else
        {
            allFires = FindObjectsOfType<FireSpread>();
            SortListsByTag();
            Debug.Log(allFires.Length);
            Debug.Log(Room1.Count);
            pickRandomFiresByRoom();
        }

    }
    void pickRandomFiresByRoom()
    {
        for(int i= 0; i < Room1FireNumber; i++)
        {
                Room1Fires.Add(Room1[Random.Range(0, Room1.Count - 1)]);
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
            if (col.tag == "Level1Room1")
                setFires(Room1Fires);
            if (col.tag == "Level1Room2")
                setFires(Room2Fires);
            if (col.tag == "Level1Room3")
                setFires(Room3Fires);
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
            Debug.Log("I set fire too" + fires[i].name);
            fires[i].isOnFire = true;
        }
    }
}
