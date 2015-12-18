using UnityEngine;
using System.Collections;

public class SonicHose_UI : UI_Animations {

	[SerializeField] private Transform sonicBeamTransform;

    void Update(){
		sonicBeamTransform.localScale = Vector3.one * SonicHose.Instance.BatteryPower;
	}

	public override void ActivateUI(){
		base.ActivateUI();
    }
	
	public override void DeActivateUI(){
		base.DeActivateUI();
    }

}
