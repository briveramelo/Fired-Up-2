using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DifficultySelect : MonoBehaviour, IRiftSelectable {

    public static Difficulty DifficultyChoice;
    public Difficulty MyDifficulty;

    TextMesh myText;
    List<DifficultySelect> otherDifficultySelectScripts;

    Color defaultColor = Color.white;
    Color highlightedColor = Color.cyan;
    Color selectedColor = Color.blue;

    void Start(){
        myText = GetComponent<TextMesh>();
        otherDifficultySelectScripts = FindObjectsOfType<DifficultySelect>().Where(difficultyScript => difficultyScript != this).ToList();
        if (!IsSelectable())
            myText.color = selectedColor;
    }

    public void OnHoverOverObject(){
        if (IsSelectable())
            myText.color = highlightedColor;
    }

    public void OnHoverExitObject(){
        if (IsSelectable())
            myText.color = defaultColor;
    }

    public void OnHoldForEnoughTime(){
        otherDifficultySelectScripts.ForEach(difficultyScript => difficultyScript.myText.color = defaultColor);

        myText.color = selectedColor;
        DifficultyChoice = MyDifficulty;
    }

    public bool IsSelectable() {
        return MyDifficulty != DifficultyChoice;
    }
}
