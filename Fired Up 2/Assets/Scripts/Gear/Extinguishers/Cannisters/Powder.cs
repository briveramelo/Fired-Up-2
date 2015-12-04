using UnityEngine;
using System.Collections;

public class Powder : HandHeldExtinguisher {

    [SerializeField] private ParticleSystem gasParticles;
   // [SerializeField] private ParticleSystem flecParticles;
    protected override IEnumerator Use(){
        gasParticles.enableEmission = true;
       // flecParticles.enableEmission = true;
        StartCoroutine(base.Use());
        yield return null;
    }

    protected override void DeActivateHose(){
        gasParticles.enableEmission = false;
        base.DeActivateHose();
        if (percentFull <= 0f){
            Inventory.Instance.UpdateAmmo(MyGear, -1);
            if (Inventory.gearInventory[MyGear] > 0)
                percentFull = 1f;
        }
    }
}
