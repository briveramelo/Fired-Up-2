using UnityEngine;
using System.Collections;

public class DifficultySelect : MonoBehaviour {
    public enum difficultyPossibilies { Easy, Normal, Hard };
    public static difficultyPossibilies difficultyChoice;
    public difficultyPossibilies MyDifficulty;
    TextMesh text;
    DifficultySelect[] difficulties = new DifficultySelect[3];
    Color selectedColor = Color.blue;
    Color defaultColor = Color.white;
    void Start()
    {
        
        text = this.GetComponent<TextMesh>();
        difficulties = FindObjectsOfType<DifficultySelect>();
        if (MyDifficulty == difficultyChoice)
            text.color = selectedColor;
    }

    public void OnHoverOverObject()
    {
        text.color = selectedColor;
    }
    public void OnHoverExitObject()
    {
        if (MyDifficulty != difficultyChoice)
            text.color = defaultColor;
    }
    public void OnHoldForEnoughTime()
    {
        for (int i = 0; i < difficulties.Length; i++)
        {
            difficulties[i].text.color = defaultColor;
        }
        text.color = selectedColor;
        difficultyChoice = MyDifficulty;
    }
}
