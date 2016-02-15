using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public LevelSaveData PreviousLevelSaveData { get; set; }

    private PlayerInfo selectedPlayerInfo;  public PlayerInfo   SelectedPlayerInfo  { get { return selectedPlayerInfo; } }
    private Level currentLevel;             public Level        CurrentLevel    { get { return currentLevel; } }
    private Level previousLevel;            public Level        PreviousLevel { get { return previousLevel; } }
    private Difficulty levelDifficulty;     public Difficulty   LevelDifficulty { get { return levelDifficulty; } }

    private int sonicHoseQuantity;          public int SonicHoseQuantity { get { return sonicHoseQuantity; } }
    private int c02Quantity;                public int C02Quantity { get { return c02Quantity; } }
    private int powderQuantity;             public int PowderQuantity { get { return powderQuantity; } }
    private int kBombQuantity;              public int KBombQuantity { get { return kBombQuantity; } }
    private int blackDeathQuantity;         public int BlackDeathQuantity { get { return blackDeathQuantity; } }

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

    public void SetInventory(Gear GearType, int quantity) {
        switch (GearType) {
            case Gear.SonicHose:    sonicHoseQuantity   = quantity;  break;
            case Gear.CO2:          c02Quantity         = quantity;  break;
            case Gear.Powder:       powderQuantity      = quantity;  break;
            case Gear.K_Bomb:       kBombQuantity       = quantity;  break;
            case Gear.BlackDeath:   blackDeathQuantity  = quantity;  break;
        }
    }

    public void OnLevelWasLoaded(int levelNum) {
        previousLevel = currentLevel;
        currentLevel = (Level)levelNum;
    }

    public void SetDifficulty(Difficulty newLevelDifficulty) {
        levelDifficulty = newLevelDifficulty;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo) {
        selectedPlayerInfo = playerInfo;
    }

}
