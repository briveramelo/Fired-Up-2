using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    private Level currentLevel;         public Level        CurrentLevel    { get { return currentLevel; } }
    private Difficulty levelDifficulty; public Difficulty   LevelDifficulty { get { return levelDifficulty; } }

    void Awake () {
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Application.targetFrameRate = 90;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
	}

    void OnLevelWasLoaded(int level) {
        currentLevel = (Level)level;
        //levelDifficulty = (Difficulty)
        //Search
    }
}
