﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheyreFiredUp : MonoBehaviour {
    FireSpread[] fires = new FireSpread[2];

	void Start () {
        fires = FindObjectsOfType<FireSpread>();
        turnFiresOff();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnHoverOverObject()
    {
    turnFiresOn();
    }
    public void OnHoverExitObject()
    {
        turnFiresOff();
    }
    public void OnHoldForEnoughTime()
    {
        loadSceneAndStoreData();
    }
    void loadSceneAndStoreData()
    {
        Debug.Log("Stored Data");
        Debug.Log((Difficulty)DifficultySelect.difficultyChoice);
        GameManager.Instance.SetDifficulty((Difficulty)DifficultySelect.difficultyChoice);
        GameManager.Instance.SetInventory(Gear.BlackDeath, GearSelectionMenu.BlackholeQuantity);
        GameManager.Instance.SetInventory(Gear.CO2, GearSelectionMenu.CO2Quantity);
        GameManager.Instance.SetInventory(Gear.K_Bomb, GearSelectionMenu.KBombQuantity);
        GameManager.Instance.SetInventory(Gear.Powder, GearSelectionMenu.PowderQuantity);
        GameManager.Instance.SetInventory(Gear.SonicHose, GearSelectionMenu.SonicQuantity);
        SceneManager.LoadScene(LevelSelect.levelChoice);
    }
    void turnFiresOn()
    {
        EffectSettings fireEffectSettings;
        for (int i = 0; i < fires.Length; i++)
        {
            fireEffectSettings = fires[i].GetComponent<EffectSettings>();
            fireEffectSettings.IsVisible = true;
        }
    }
    void turnFiresOff()
    {
        EffectSettings fireEffectSettings;
        for (int i = 0; i < fires.Length; i++)
        {
            fireEffectSettings = fires[i].GetComponent<EffectSettings>();
            fireEffectSettings.IsVisible = false;
        }
    }
}
