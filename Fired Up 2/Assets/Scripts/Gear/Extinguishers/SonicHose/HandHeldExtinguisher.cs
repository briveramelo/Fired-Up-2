﻿using UnityEngine;
using System.Collections;
using FU;
using System.Collections.Generic;

public abstract class HandHeldExtinguisher : MonoBehaviour {

    [SerializeField] protected GearEnum MyGear;
    [SerializeField] protected Animator myAnimator;
    [SerializeField] protected Collider myCollider;
    [SerializeField] protected AudioSource mySound;

    protected GearEnum LastGear;
    protected float percentFull;
    protected float mySoundClipLength;
    protected float timeToExtinguish;
    protected List<Collider> fires;
    protected float minPercentToUse;

    protected virtual void Awake(){
        mySoundClipLength = mySound.clip.length;
        timeToExtinguish = 0.5f;
        percentFull = 1f;
        minPercentToUse = 0f;
        fires = new List<Collider>();
    }

    protected virtual void Update(){
        if (Inventory.CurrentGear == MyGear){
            if (LastGear != MyGear)
                Equip();
            if (Input.GetButtonDown(Controls.FightFire) && percentFull > minPercentToUse)
                StartCoroutine(Use());
        }
        else if (LastGear == MyGear && Inventory.CurrentGear != MyGear)
            StartCoroutine(PutAway());

        LastGear = Inventory.CurrentGear;
    }

    protected virtual void Equip(){
        myAnimator.SetInteger("AnimState", (int)HoseStates.Equip);
    }

    protected virtual IEnumerator Use(){
        myAnimator.SetInteger("AnimState", (int)HoseStates.Engage);
        myCollider.enabled = true;
        mySound.Play();
        while (Input.GetButton(Controls.FightFire) && Inventory.CurrentGear == MyGear && percentFull > 0f){
            percentFull -= Time.deltaTime / mySoundClipLength;
            yield return null;
        }
        DeActivateHose();
    }

    protected virtual void DeActivateHose(){
        mySound.Stop();
        myAnimator.SetInteger("AnimState", (int)HoseStates.Idle);
        myCollider.enabled = false;
        if (fires.Count > 0) fires.Clear();
        StartCoroutine(Recharge());
    }

    IEnumerator Recharge(){
        while (percentFull < 1f && myAnimator.GetInteger("AnimState") != (int)HoseStates.Engage){
            percentFull += Time.deltaTime / mySoundClipLength;
            yield return null;
        }
    }

    protected virtual IEnumerator PutAway(){
        yield return null;
        myAnimator.SetInteger("AnimState", (int)HoseStates.PutAway);
    }

    protected virtual void OnTriggerEnter(Collider col){
        if (LayerMaskExtensions.IsInLayerMask(col.gameObject, Layers.LayerMasks.allFires) && !fires.Contains(col))
            StartCoroutine(TryToExtinguish(col));
    }

    protected virtual IEnumerator TryToExtinguish(Collider col){
        fires.Add(col);
        float timePassed = 0;
        while (fires.Contains(col) && timePassed < timeToExtinguish){
            timePassed += Time.deltaTime;
            yield return null;
        }
        if (fires.Contains(col)){
            FireSpread fireSpreadScript = col.GetComponent<FireSpread>();
            fireSpreadScript.ExtinguishFire();
            fires.Remove(col);
        }
    }

    void OnTriggerExit(Collider col){
        if (fires.Contains(col))
            fires.Remove(col);
    }
}