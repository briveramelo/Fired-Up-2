using UnityEngine;
using System.Collections;
using System;

public class Combos : Score {

    public static Combos Instance;
    [HideInInspector] public int ComboPoints;

    void Awake() {
        Instance = this;
        MyScoreType = ScoreType.Combos;
    }


    protected override int CalculateScore(){
        return ComboPoints;
    }

}
