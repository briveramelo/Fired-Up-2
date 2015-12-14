using UnityEngine;
using System.Collections;
using System.Linq;

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
        return DataSaver.Instance.DataSaveFile.playersLevelBests.Any(playerLevelBest => newPlayerName == playerLevelBest.Key._playerName);
    }
}
