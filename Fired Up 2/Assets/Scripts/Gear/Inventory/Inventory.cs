using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FU;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;
	public static Dictionary <Gear, int> gearInventory;
	public static Gear CurrentGear;

	protected static SonicHose_UI sonicHose_UI;
	protected static K_Bomb_UI k_Bomb_UI;
	protected static BlackDeath_UI blackDeath_UI;
    protected static CO2_UI cO2_UI;
    protected static Powder_UI powder_UI;

    // Use this for initialization
    void Awake(){
        Instance = this;
        gearInventory = new Dictionary<Gear, int>();
		gearInventory.Add(Gear.SonicHose,   1);
        gearInventory.Add(Gear.CO2,         2);
        gearInventory.Add(Gear.Powder,      2);
        gearInventory.Add(Gear.K_Bomb,      2);
		gearInventory.Add(Gear.BlackDeath,  3);

		sonicHose_UI =      FindObjectOfType<SonicHose_UI>();
        cO2_UI =            FindObjectOfType<CO2_UI>();
        powder_UI =         FindObjectOfType<Powder_UI>();
		k_Bomb_UI =         FindObjectOfType<K_Bomb_UI>();
		blackDeath_UI =     FindObjectOfType<BlackDeath_UI>();

        foreach (Gear GearType in (Gear[])Enum.GetValues(typeof(Gear))) {
            UpdateUIDisplay(GearType);
        }
    }

    public void UpdateAmmo(Gear GearToUpdate, float quantityToAdd) {
        UpdateInventoryBank(GearToUpdate, quantityToAdd);
        UpdateUIDisplay(GearToUpdate);
    }

    void UpdateInventoryBank(Gear GearToUpdate, float quantityToAdd) {
        if (GearToUpdate == Gear.SonicHose)
            SonicHose.Instance.BatteryPower += quantityToAdd;
        else
            gearInventory[GearToUpdate]+= (int)quantityToAdd;
    }

    void UpdateUIDisplay(Gear GearToUpdate) {
        UI_Animations UI_ToUpdate;

        if (GearToUpdate == Gear.SonicHose)                 UI_ToUpdate = sonicHose_UI;
        else if (GearToUpdate == Gear.CO2)                  UI_ToUpdate = cO2_UI;
        else if (GearToUpdate == Gear.Powder)               UI_ToUpdate = powder_UI;
        else if (GearToUpdate == Gear.K_Bomb)               UI_ToUpdate = k_Bomb_UI;
        else /*if (GearToUpdate == GearEnum.BlackDeath)*/   UI_ToUpdate = blackDeath_UI;

        UI_ToUpdate.UpdateInventory(gearInventory[GearToUpdate]);
    }

}
