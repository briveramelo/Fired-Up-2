using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreSaver : MonoBehaviour {

    public static ScoreSaver Instance;
    private Dictionary<LevelEnum, List<ScoreBoard>> levelScoreBoards;

    // Use this for initialization
    void Awake () {
        Instance = this;
        Load();
	}
	
    public void Load(){
		if (File.Exists(Application.dataPath + "/SaveData/ScoreSaves.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fileStream = File.Open(Application.dataPath + "/SaveData/ScoreSaves.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(fileStream);
			fileStream.Close();

            levelScoreBoards = saveData.savedLevelScoreBoards;
		}
	}

    public void PromptSave(ScoreBoard scoreBoard){
        levelScoreBoards[scoreBoard.ThisLevel].Add(scoreBoard);
		Save();
	}
	
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fileStream = File.Create(Application.dataPath + "/SaveData/ScoreSaves.dat");
        SaveData saveData = new SaveData();

        saveData.savedLevelScoreBoards = levelScoreBoards;
		bf.Serialize (fileStream, saveData);
		fileStream.Close ();
	}
	
}

[Serializable]
class SaveData{
    public Dictionary<LevelEnum, List<ScoreBoard>> savedLevelScoreBoards;
}
