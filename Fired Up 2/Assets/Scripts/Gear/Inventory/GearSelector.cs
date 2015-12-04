using UnityEngine;
using System.Collections;
using System;
using FU;

public class GearSelector : Inventory {
	
	void Start(){
		CurrentGear = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (Controls.ToggleGear)){
			ToggleGear();
		}
	}

	void ToggleGear(){
		if ((int)CurrentGear == Enum.GetNames(typeof(GearEnum)).Length-1){
			CurrentGear = GearEnum.SonicHose;
		}
		else{
			CurrentGear++;
			if (gearInventory[CurrentGear]<=0){
				ToggleGear();
                return;
			}
		}
        GearSelector_UI.Instance.HighlightActiveGearIcon();
    }

	
}
