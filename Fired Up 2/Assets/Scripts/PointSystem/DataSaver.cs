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
        string playerName = newLevelSaveData.playerInfo.playerName;
        Difficulty difficulty = newLevelSaveData.levelDifficulty;
        Level ThisLevel = newLevelSaveData.thisLevel;

        Debug.Log(newLevelSaveData.pointTotal);

        if (newLevelSaveData.pointTotal > dataSaveFile.profiles[playerName].levelBests[ThisLevel][difficulty].pointTotal) {
            dataSaveFile.profiles[playerName].levelBests[ThisLevel][difficulty] = newLevelSaveData;
		    Save();
        }
	}
	
	void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fileStream = File.Create(Application.dataPath + "/DataSaves.dat");

		bf.Serialize (fileStream, dataSaveFile);
		fileStream.Close ();
    }

    public void CreateNewPlayer(string playerName) {
        if (dataSaveFile.profiles == null) {
            dataSaveFile = new DataSave(playerName);
        }
        else {
            dataSaveFile.AddNewPlayer(playerName);
        }
        Save();
    }
	
}

[Serializable]
public struct DataSave{
    public Dictionary<string, PlayerInfo> profiles;
    
    public DataSave(string playerName) {
        profiles = new Dictionary<string, PlayerInfo>();
        AddNewPlayer(playerName);
    }

    public void AddNewPlayer(string newPlayerName) {
        PlayerInfo newPlayerInfo = new PlayerInfo(newPlayerName);
        profiles.Add(newPlayerName, newPlayerInfo);
        Debug.Log(newPlayerName);
    }

    public static bool IsRepeatName(string newPlayerName) {
        if (DataSaver.Instance.DataSaveFile.profiles != null) {
            return DataSaver.Instance.DataSaveFile.profiles.Any(profile => newPlayerName == profile.Key);
        }
        else {
            return false;
        }
    } 
}
