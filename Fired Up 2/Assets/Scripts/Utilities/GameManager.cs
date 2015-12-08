using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    private LevelEnum currentLevel = LevelEnum.One;
    public LevelEnum CurrentLevel { get { return currentLevel; } }

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
        currentLevel = (LevelEnum)level;
    }
}
