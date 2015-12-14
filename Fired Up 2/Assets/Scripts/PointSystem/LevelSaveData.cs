using System;
[Serializable]
public class LevelSaveData{
    public int combos;
    public int confidence;
    public int problemSolving;
    public int resilience;
    public int situationalAwareness;
    public int timeBonus;
    public int pointTotal;
    public DateTime nowTime;
    public Difficulty levelDifficulty;
    public PlayerInfo playerInfo;
    public Level thisLevel;
}
