using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class LevelSelect : MonoBehaviour, IRiftSelectable {

    public static Level LevelChoice;

    public Level MyLevel;
    [SerializeField] private Light myLight;

    private bool isBeingHoveredOver;

    List<LevelSelect> otherLevelSelectScripts;

    void Start() {
        if (MyLevel == Level.Level_1) {
            LevelChoice = MyLevel;
        }
        
        otherLevelSelectScripts = FindObjectsOfType<LevelSelect>().Where(levelScript => levelScript != this).ToList();
    }

    public void OnHoverOverObject() {
        if (!isBeingHoveredOver) {
            myLight.enabled = true;
            isBeingHoveredOver = true;
        }
    }

    public void OnHoverExitObject() {
        isBeingHoveredOver = false;
        if (IsSelectable())
            myLight.enabled = false;
    }

    public void OnHoldForEnoughTime() {
        otherLevelSelectScripts.ForEach(levelSelectScript => levelSelectScript.myLight.enabled = false);
        myLight.enabled = true;
        LevelChoice = MyLevel;
    }

    public bool IsSelectable() {
        return MyLevel != LevelChoice;
    }
}
