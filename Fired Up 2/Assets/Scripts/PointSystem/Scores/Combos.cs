using UnityEngine;
using System.Collections;
using System;

public class Combos : ScoreType {

    public static Combos Instance;
    [HideInInspector] public int ComboPoints;

    void Awake() {
        Instance = this;
        MyScoreEnum = Score.Combos;
    }


    protected override int CalculateScore(){
        return ComboPoints;
    }

}
