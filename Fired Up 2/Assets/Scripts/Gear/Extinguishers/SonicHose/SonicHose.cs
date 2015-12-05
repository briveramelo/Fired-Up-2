using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class SonicHose : HandHeldExtinguisher {

    public static SonicHose Instance;

    [SerializeField] private GameObject sonicPulse;
    private float rechargeTime;

    protected override void Awake() {
        base.Awake();
        Instance = this;
        rechargeTime = 1f;
        minPercentToUse = 1f;
    }


    public float BatteryPower{
        get { return percentFull; }
        set{
            percentFull = Mathf.Clamp(value, 0f, 2f);
            if (percentFull == 0f)
                DeActivateHose();
        }
    }

    protected override IEnumerator Use(){
        myAnimator.SetInteger("AnimState", (int)HoseStates.Engage);
        mySound.Play();
       
        BatteryPower = 0f;
        SonicPulse sonicPulseScript = (Instantiate(sonicPulse, transform.position, Quaternion.Euler (90f,transform.root.rotation.eulerAngles.y,0f)) as GameObject).GetComponent<SonicPulse>();
        sonicPulseScript.Launch(transform.up);

        DeActivateHose();
        StartCoroutine(Recharge());
        yield return null;
    }

    protected override void DeActivateHose(){
        myAnimator.SetInteger("AnimState", (int)HoseStates.Idle);
    }

        IEnumerator Recharge() {
        while (BatteryPower < 1f){
            BatteryPower += Time.deltaTime / rechargeTime;
            yield return null;
        }
        BatteryPower = 1f;
    }
}