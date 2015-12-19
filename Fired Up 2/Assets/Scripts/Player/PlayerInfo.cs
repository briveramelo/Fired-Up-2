using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[Serializable]
public struct PlayerInfo {

    public Dictionary<Level, Dictionary<Difficulty, LevelSaveData>> levelBests;

    private string _playerName; public string playerName { get { return _playerName; } }
    private Rank _myRank; public Rank myRank { get { return _myRank; } }
    private int _totalBest;
    public int totalBest{
        get { return _totalBest; }
        set{
            _totalBest = value;
            _myRank = (Rank)(Mathf.Clamp(_totalBest / 1000000, 0, (int)Rank.Chief));
        }
    }

    public PlayerInfo(string newPlayerName) {
        _playerName = newPlayerName;
        _myRank = Rank.Volunteer;
        _totalBest = 0;
        levelBests = new Dictionary<Level, Dictionary<Difficulty, LevelSaveData>>();

        LevelSaveData emptyLevelSaveData = new LevelSaveData();

        for(int i=(int)Level.Level_1; i<(int)Level.Level_6; i++){
            Dictionary<Difficulty, LevelSaveData> newDifficultyBests = new Dictionary<Difficulty, LevelSaveData>();
            foreach (Difficulty difficultyEnum in Enum.GetValues(typeof(Difficulty))) {
                newDifficultyBests.Add(difficultyEnum, emptyLevelSaveData);
            }
            levelBests.Add((Level)i, newDifficultyBests);
        }
    }
}
