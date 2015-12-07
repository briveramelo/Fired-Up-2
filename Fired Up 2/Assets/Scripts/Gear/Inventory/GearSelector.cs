using UnityEngine;
using System.Collections;
using System;
using FU;

public class GearSelector : Inventory {
    private bool forward = true;
    private bool back = false;

	void Start(){
		CurrentGear = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (Controls.ToggleForward)){
			ToggleGear(forward);
		}
        else if (Input.GetButtonDown (Controls.ToggleBack)){
			ToggleGear(back);
		}
	}

	void ToggleGear(bool toggleForward){
        if (toggleForward) {
            if ((int)CurrentGear == Enum.GetNames(typeof(GearEnum)).Length-1){
			    CurrentGear = GearEnum.SonicHose;
		    }
		    else{
			    CurrentGear++;
			    if (gearInventory[CurrentGear]<=0){
				    ToggleGear(forward);
                    return;
			    }
		    }
        }
        else {
            if (CurrentGear == 0){
                CurrentGear = (GearEnum)(Enum.GetNames(typeof(GearEnum)).Length - 1);
                if (gearInventory[CurrentGear] <= 0){
                    ToggleGear(back);
                    return;
                }
            }
		    else{
                CurrentGear--;
		    }
        }

        GearSelector_UI.Instance.HighlightActiveGearIcon();
    }

	
}
