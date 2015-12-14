using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {
    public static int levelChoice;
    public int MyLevel;
    Behaviour halo;
    LevelSelect[] halos = new LevelSelect[5];
	// Use this for initialization
	void Start () {
        halo = (Behaviour)GetComponent("Halo");
        halos = FindObjectsOfType<LevelSelect>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver()
    {
        halo.enabled = true;
    }
    void OnMouseExit()
    {
        if(MyLevel != levelChoice)
        halo.enabled = false;
    }
    void OnMouseDown()
    {
        for (int i = 0; i < halos.Length; i++)
        {
            halos[i].halo.enabled = false;
        }
        halo.enabled = true;
        levelChoice = MyLevel;
    }

}
