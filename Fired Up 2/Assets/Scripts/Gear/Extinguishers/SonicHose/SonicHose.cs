using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;

public class SonicHose : HandHeldExtinguisher {

    public static SonicHose Instance;

    [SerializeField]
    private EffectSettings beamEffectSettingsScript;

    public float BatteryPower{
        get { return percentFull; }
        set{
            percentFull = Mathf.Clamp(value, 0f, 2f);
            if (percentFull == 0f)
                DeActivateHose();
        }
    }

    protected override void Awake(){
        base.Awake();
        Instance = this;
    }

    protected override void OnTriggerEnter(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allFires) && !fires.Contains(col))
            StartCoroutine(TryToExtinguish(col));
    }

    protected override IEnumerator Use(){
        beamEffectSettingsScript.IsVisible = true;
        StartCoroutine(base.Use());
        StartCoroutine(Recharge());
        yield return null;
    }

    IEnumerator Recharge(){
        while (percentFull < 1f && myAnimator.GetInteger("AnimState") != (int)HoseStates.Engage) {
            percentFull += Time.deltaTime / mySoundClipLength;
            yield return null;
        }
    }

    protected override void DeActivateHose(){
        beamEffectSettingsScript.IsVisible = false;
        base.DeActivateHose();
    }
}