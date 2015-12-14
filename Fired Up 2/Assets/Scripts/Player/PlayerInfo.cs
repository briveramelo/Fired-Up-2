using UnityEngine;
using System.Collections;
using System.Linq;
using System;

[Serializable]
public struct PlayerInfo {

    public string _playerName;
    public int _totalBest;
    public Rank _myRank;

    public PlayerInfo(string PlayerName) {
        _playerName = PlayerName;
        _totalBest = 0;
        _myRank = (Rank)0;
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
