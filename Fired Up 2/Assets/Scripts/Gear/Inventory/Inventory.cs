using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FU;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;
	protected static Dictionary <GearEnum,int> gearInventory;
	public static GearEnum CurrentGear;

	protected static SonicHose_UI sonicHose_UI;
	protected static K_Bomb_UI k_Bomb_UI;
	protected static BlackDeath_UI blackDeath_UI;
    protected static CO2_UI cO2_UI;
    protected static Powder_UI powder_UI;

    // Use this for initialization
    void Awake(){
        Instance = this;
        gearInventory = new Dictionary<GearEnum, int>();
		gearInventory.Add(GearEnum.SonicHose,   1);
        gearInventory.Add(GearEnum.CO2,         2);
        gearInventory.Add(GearEnum.Powder,      2);
        gearInventory.Add(GearEnum.K_Bomb,      2);
		gearInventory.Add(GearEnum.BlackDeath,  3);

		sonicHose_UI =      FindObjectOfType<SonicHose_UI>();
        cO2_UI =            FindObjectOfType<CO2_UI>();
        powder_UI =         FindObjectOfType<Powder_UI>();
		k_Bomb_UI =         FindObjectOfType<K_Bomb_UI>();
		blackDeath_UI =     FindObjectOfType<BlackDeath_UI>();

        foreach (GearEnum GearType in (GearEnum[])Enum.GetValues(typeof(GearEnum))) {
            UpdateUIDisplay(GearType);
        }
    }

    public void UpdateAmmo(GearEnum GearToUpdate, float quantityToAdd) {
        UpdateInventoryBank(GearToUpdate, quantityToAdd);
        UpdateUIDisplay(GearToUpdate);
    }

    void UpdateInventoryBank(GearEnum GearToUpdate, float quantityToAdd) {
        if (GearToUpdate == GearEnum.SonicHose)
            SonicHose.Instance.BatteryPower += quantityToAdd;
        else
            gearInventory[GearToUpdate]+= (int)quantityToAdd;
    }

    void UpdateUIDisplay(GearEnum GearToUpdate) {
        UI_Animations UI_ToUpdate;

        if (GearToUpdate == GearEnum.SonicHose)             UI_ToUpdate = sonicHose_UI;
        else if (GearToUpdate == GearEnum.CO2)              UI_ToUpdate = cO2_UI;
        else if (GearToUpdate == GearEnum.Powder)           UI_ToUpdate = powder_UI;
        else if (GearToUpdate == GearEnum.K_Bomb)           UI_ToUpdate = k_Bomb_UI;
        else /*if (GearToUpdate == GearEnum.BlackDeath)*/   UI_ToUpdate = blackDeath_UI;

        UI_ToUpdate.UpdateInventory(gearInventory[GearToUpdate]);
    }

}
