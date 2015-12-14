using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent (typeof(GameManager))]
public class DataSaver : MonoBehaviour {


    public static DataSaver Instance;
    private DataSave dataSaveFile;      public DataSave DataSaveFile { get { return dataSaveFile; } }

    // Use this for initialization
    void Awake () {
        Instance = this;
    }
	
    public void Load(){
		if (File.Exists(Application.dataPath + "/DataSaves.dat")){

            BinaryFormatter bf = new BinaryFormatter ();
			FileStream fileStream = File.Open(Application.dataPath + "/DataSaves.dat", FileMode.Open);
            dataSaveFile = (DataSave)bf.Deserialize(fileStream);
			fileStream.Close();
		}
	}

    public void PromptSave(LevelSaveData newLevelSaveData){
        PlayerInfo playerInfo = newLevelSaveData.playerInfo;
        Level ThisLevel = newLevelSaveData.thisLevel;
        
        if (newLevelSaveData.pointTotal > dataSaveFile.playersLevelBests[playerInfo][ThisLevel].pointTotal) {
            dataSaveFile.playersLevelBests[playerInfo][ThisLevel] = newLevelSaveData;
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
        if (dataSaveFile.playersLevelBests.Count==0) {
            dataSaveFile = new DataSave(PlayerName);
        }
        else {
            dataSaveFile.AddNewPlayer(PlayerName);
        }
    }
	
}

[Serializable]
public struct DataSave{
    public Dictionary<PlayerInfo, Dictionary<Level, LevelSaveData>> playersLevelBests;

    public DataSave(string playerName) {
        playersLevelBests = new Dictionary<PlayerInfo, Dictionary<Level, LevelSaveData>>();
        if (PlayerInfo.IsRepeatName(playerName)) {
            //break and display repeat display code;
            //Search
        }

        AddNewPlayer(playerName);
    }

    public void AddNewPlayer(string playerName) {
        Dictionary<Level, LevelSaveData> newLevelBests = new Dictionary<Level, LevelSaveData>();
        LevelSaveData emptyLevelSaveData = new LevelSaveData();

        foreach (Level levelEnum in Enum.GetValues(typeof(Level))){
            newLevelBests.Add(levelEnum, emptyLevelSaveData);
        }

        PlayerInfo newPlayerInfo = new PlayerInfo(playerName);
        playersLevelBests.Add(newPlayerInfo, newLevelBests);
    }
}
