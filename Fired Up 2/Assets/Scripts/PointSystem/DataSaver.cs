using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

[RequireComponent (typeof(GameManager))]
public class DataSaver : MonoBehaviour {

    public static DataSaver Instance;
    private DataSave dataSaveFile;      public DataSave DataSaveFile { get { return dataSaveFile; } }

    // Use this for initialization
    void Awake () {
        Instance = this;
        //ClearData();
        Load();
    }

    void ClearData() {
        dataSaveFile = new DataSave();
        Save();
    }

    void Load(){
		if (File.Exists(Application.dataPath + "/DataSaves.dat")){
            BinaryFormatter bf = new BinaryFormatter ();
			FileStream fileStream = File.Open(Application.dataPath + "/DataSaves.dat", FileMode.Open);
            dataSaveFile = (DataSave)bf.Deserialize(fileStream);
            fileStream.Close();
		}
	}

    public void SaveLevelData(LevelSaveData newLevelSaveData){
        PlayerInfo playerInfo = newLevelSaveData.playerInfo;
        Difficulty difficulty = newLevelSaveData.levelDifficulty;
        Level ThisLevel = newLevelSaveData.thisLevel;
        
        if (newLevelSaveData.pointTotal > dataSaveFile.playersLevelBests[playerInfo][ThisLevel][difficulty].pointTotal) {
            dataSaveFile.playersLevelBests[playerInfo][ThisLevel][difficulty] = newLevelSaveData;
		    Save();
        }
	}
	
	void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fileStream = File.Create(Application.dataPath + "/DataSaves.dat");

		bf.Serialize (fileStream, dataSaveFile);
		fileStream.Close ();
	}

    public void CreateNewPlayer(string PlayerName) {
        if (dataSaveFile.playersLevelBests == null) {
            dataSaveFile = new DataSave(PlayerName);
        }
        else {
            dataSaveFile.AddNewPlayer(PlayerName);
        }
        Save();
    }
	
}

[Serializable]
public struct DataSave{
    public Dictionary<PlayerInfo, Dictionary<Level, Dictionary<Difficulty, LevelSaveData>>> playersLevelBests;

    public DataSave(string playerName) {
        playersLevelBests = new Dictionary<PlayerInfo, Dictionary<Level, Dictionary<Difficulty, LevelSaveData>>>();
        AddNewPlayer(playerName);
    }

    public void AddNewPlayer(string playerName) {
        Dictionary<Level, Dictionary<Difficulty, LevelSaveData>> newLevelDifficultyBests = new Dictionary<Level, Dictionary<Difficulty, LevelSaveData>>();
        LevelSaveData emptyLevelSaveData = new LevelSaveData();

        foreach (Level levelEnum in Enum.GetValues(typeof(Level))){
            Dictionary<Difficulty, LevelSaveData> newDifficultyBests = new Dictionary<Difficulty, LevelSaveData>();
            foreach (Difficulty difficultyEnum in Enum.GetValues(typeof(Difficulty))) {
                newDifficultyBests.Add(difficultyEnum, emptyLevelSaveData);
            }
            newLevelDifficultyBests.Add(levelEnum, newDifficultyBests);
        }

        PlayerInfo newPlayerInfo = new PlayerInfo(playerName);
        playersLevelBests.Add(newPlayerInfo, newLevelDifficultyBests);
    }

    public static bool IsRepeatName(string newPlayerName) {
        if (DataSaver.Instance.DataSaveFile.playersLevelBests != null) {
            return DataSaver.Instance.DataSaveFile.playersLevelBests.Any(playerLevelBest => newPlayerName == playerLevelBest.Key._playerName);
        }
        else {
            return false;
        }
    } 
}
