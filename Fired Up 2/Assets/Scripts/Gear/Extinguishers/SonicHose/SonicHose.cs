using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class SonicHose : HandHeldExtinguisher {

    public static SonicHose Instance;

    [SerializeField] private GameObject sonicPulse;
    [SerializeField] private AudioSource chargePlayer;
    private float rechargeTime;
    private bool isCharging;

    protected override void Awake() {
        base.Awake();
        Instance = this;
        rechargeTime = .75f;
        minPercentToUse = 1f;
        percentFull = 1f;
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
        if (!isCharging){
            SonicPulse sonicPulseScript = (Instantiate(sonicPulse, transform.position, Quaternion.identity) as GameObject).GetComponent<SonicPulse>();
            sonicPulseScript.transform.parent = transform;
            sonicPulseScript.transform.localRotation = Quaternion.identity;
            sonicPulseScript.transform.parent = null;
            sonicPulseScript.Launch(transform.up);
            BlastHose();
            DeActivateHose();
        }
        yield return StartCoroutine(Charge());
    }

    void BlastHose() {
        myAnimator.SetInteger("AnimState", (int)HoseState.Engage);
        mySound.Play();
        BatteryPower = 0f;
    }

    protected override void DeActivateHose(){
        myAnimator.SetInteger("AnimState", (int)HoseState.Idle);
    }

    IEnumerator Charge() {
        isCharging = true;
        chargePlayer.Play();
        while (BatteryPower < 1f){
            BatteryPower += Time.deltaTime / rechargeTime;
            yield return null;
        }
        chargePlayer.Stop();
        BatteryPower = 1f;
        isCharging = false;
    }
}