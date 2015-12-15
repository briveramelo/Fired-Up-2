using UnityEngine;
using System.Collections;

public class DifficultySelect : MonoBehaviour {
   // public enum difficultyPossibilies { Easy, Normal, Hard };
    public static Difficulty difficultyChoice;
    public Difficulty MyDifficulty;
    TextMesh text;
    DifficultySelect[] difficulties = new DifficultySelect[3];
    Color selectedColor = Color.cyan;
    Color defaultColor = Color.white;
    Color chosenColor = Color.blue;
    void Start()
    {
        difficultyChoice = Difficulty.Easy;
        text = this.GetComponent<TextMesh>();
        difficulties = FindObjectsOfType<DifficultySelect>();
        if (MyDifficulty == difficultyChoice)
            text.color = chosenColor;

    }

    public void OnHoverOverObject()
    {
        text.color = selectedColor;
    }
    public void OnHoverExitObject()
    {
        if (MyDifficulty != difficultyChoice)
            text.color = defaultColor;
        else if (MyDifficulty == difficultyChoice)
            text.color = chosenColor;
    }
    public void OnHoldForEnoughTime()
    {
        for (int i = 0; i < difficulties.Length; i++)
        {
            difficulties[i].text.color = defaultColor;
        }
        text.color = chosenColor;
        difficultyChoice = MyDifficulty;
    }
}
