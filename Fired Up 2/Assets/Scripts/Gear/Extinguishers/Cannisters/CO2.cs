using UnityEngine;
using System.Collections;
using FU;
using System.Collections.Generic;

public class CO2 : HandHeldExtinguisher{

    [SerializeField] private ParticleSystem gasParticles;

    protected override IEnumerator Use(){
        gasParticles.enableEmission = true;
        StartCoroutine(base.Use());
        yield return null;
    }

    protected override void DeActivateHose() {
        gasParticles.enableEmission = false;
        base.DeActivateHose();
    }
}
