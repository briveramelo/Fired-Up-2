using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class SonicHose : HandHeldExtinguisher {

    public static SonicHose Instance;

    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip blastSound;
    [SerializeField] private GameObject sonicPulse;
    private float rechargeTime;
    private bool isCharging;

    protected override void Awake() {
        base.Awake();
        Instance = this;
        rechargeTime = .75f;
        minPercentToUse = 0f;
        percentFull = 0f;
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
            yield return StartCoroutine(Charge());
            SonicPulse sonicPulseScript = (Instantiate(sonicPulse, transform.position, Quaternion.Euler (90f,transform.root.rotation.eulerAngles.y,0f)) as GameObject).GetComponent<SonicPulse>();
            sonicPulseScript.Launch(transform.up);
            BlastHose();
            DeActivateHose();
        }
        yield return null;
    }

    void BlastHose() {
        myAnimator.SetInteger("AnimState", (int)HoseState.Engage);
        mySound.clip = blastSound;
        mySound.Play();
        BatteryPower = 0f;
    }

    protected override void DeActivateHose(){
        myAnimator.SetInteger("AnimState", (int)HoseState.Idle);
    }

    IEnumerator Charge() {
        isCharging = true;
        mySound.clip = chargeSound;
        mySound.Play();
        while (BatteryPower < 1f){
            BatteryPower += Time.deltaTime / rechargeTime;
            yield return null;
        }
        mySound.Stop();
        BatteryPower = 1f;
        isCharging = false;
    }
}