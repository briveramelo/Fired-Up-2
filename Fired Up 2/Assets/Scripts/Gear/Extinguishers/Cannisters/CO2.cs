using UnityEngine;
using System.Collections;
using FU;
using System.Collections.Generic;

public class CO2 : HandHeldExtinguisher{

    [SerializeField] private ParticleSystem gasParticles;

    protected override IEnumerator Use(){
        ParticleSystem.EmissionModule em = gasParticles.emission;
        em.enabled = true;
        StartCoroutine(base.Use());
        yield return null;
    }

    protected override void DeActivateHose() {
        ParticleSystem.EmissionModule em = gasParticles.emission;
        em.enabled = false;
        base.DeActivateHose();
        if (percentFull <= 0f) {
            Inventory.Instance.UpdateAmmo(MyGear, -1);
            if (Inventory.GearInventory[MyGear] > 0) {
                percentFull = 1f;
            }
        }
    }
}
