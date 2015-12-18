using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FU;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;
	public static Dictionary <Gear, int> GearInventory;
	public static Gear CurrentHose;
    public static Gear CurrentGrenade;

    protected static SonicHose_UI sonicHose_UI;
	protected static K_Bomb_UI k_Bomb_UI;
	protected static BlackDeath_UI blackDeath_UI;
    protected static CO2_UI cO2_UI;
    protected static Powder_UI powder_UI;

    // Use this for initialization
    void Awake(){
        Instance = this;
        GearInventory = new Dictionary<Gear, int>();

        #region ALL THE PERKS
        GearInventory.Add(Gear.SonicHose,       int.MaxValue);
        GearInventory.Add(Gear.CO2,             int.MaxValue);
        GearInventory.Add(Gear.Powder,          int.MaxValue);
        GearInventory.Add(Gear.K_Bomb,          int.MaxValue);
        GearInventory.Add(Gear.BlackDeath,      int.MaxValue);
        GearInventory.Add(Gear.Empty,           int.MaxValue);
        #endregion

  //      #region Your Purchases
  //      GearInventory.Add(Gear.SonicHose,   GameManager.Instance.SonicHoseQuantity);
  //      GearInventory.Add(Gear.CO2,         GameManager.Instance.C02Quantity);
  //      GearInventory.Add(Gear.Powder,      GameManager.Instance.PowderQuantity);
  //      GearInventory.Add(Gear.K_Bomb,      GameManager.Instance.KBombQuantity);
		//GearInventory.Add(Gear.BlackDeath,  GameManager.Instance.BlackDeathQuantity);
  //      GearInventory.Add(Gear.Empty,       int.MaxValue);
  //      #endregion

        sonicHose_UI =      FindObjectOfType<SonicHose_UI>();
        cO2_UI =            FindObjectOfType<CO2_UI>();
        powder_UI =         FindObjectOfType<Powder_UI>();
		k_Bomb_UI =         FindObjectOfType<K_Bomb_UI>();
		blackDeath_UI =     FindObjectOfType<BlackDeath_UI>();

        foreach (Gear GearType in (Gear[])Enum.GetValues(typeof(Gear))) {
            if (GearType != Gear.Empty) {
                UpdateUIDisplay(GearType);
            }
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
            GearInventory[GearToUpdate]+= (int)quantityToAdd;
    }

    public void UpdateInventoryBank() {

    }

    void UpdateUIDisplay(Gear GearToUpdate) {
        UI_Animations UI_ToUpdate = sonicHose_UI;

        if (GearToUpdate == Gear.SonicHose)                 UI_ToUpdate = sonicHose_UI;
        else if (GearToUpdate == Gear.CO2)                  UI_ToUpdate = cO2_UI;
        else if (GearToUpdate == Gear.Powder)               UI_ToUpdate = powder_UI;
        else if (GearToUpdate == Gear.K_Bomb)               UI_ToUpdate = k_Bomb_UI;
        else if (GearToUpdate == Gear.BlackDeath)           UI_ToUpdate = blackDeath_UI;

        UI_ToUpdate.UpdateInventory(GearInventory[GearToUpdate]);
    }

}
