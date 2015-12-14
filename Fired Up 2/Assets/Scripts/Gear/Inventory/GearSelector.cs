using UnityEngine;
using System.Collections;
using System;
using FU;

public class GearSelector : Inventory {

    void Start() {
        CurrentHose = Gear.SonicHose;
        CurrentGrenade = Gear.K_Bomb;
    }

    void Update() {
        if (Input.GetButtonDown(Controls.ToggleHose)) {
            ToggleHose();
        }
        else if (Input.GetButtonDown(Controls.ToggleGrenade)) {
            ToggleGrenade();
        }
    }

    void ToggleHose() {
        if (CurrentHose == Gear.Powder) {
            CurrentHose = Gear.SonicHose;
        }
        else {
            CurrentHose++;
            if (GearInventory[CurrentHose] <= 0) {
                ToggleHose();
                return;
            }
        }

        HoseSelector_UI.Instance.HighlightActiveIcon();
    }

    void ToggleGrenade() {
        if (GearInventory[Gear.K_Bomb] <= 0 && GearInventory[Gear.BlackDeath] <= 0){
            CurrentGrenade = Gear.Empty;
        }
        else if (CurrentGrenade != Gear.K_Bomb && GearInventory[Gear.K_Bomb] > 0) {
            CurrentGrenade = Gear.K_Bomb;
        }
        else if (CurrentGrenade != Gear.BlackDeath && GearInventory[Gear.BlackDeath] > 0) {
            CurrentGrenade = Gear.BlackDeath;
        }

        GrenadeSelector_UI.Instance.HighlightActiveIcon();
    }

	
}
