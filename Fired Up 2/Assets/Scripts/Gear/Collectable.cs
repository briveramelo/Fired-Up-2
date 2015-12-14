using UnityEngine;
using System.Collections;
using FU;
public class Collectable : MonoBehaviour {

    [SerializeField] Gear GearType;
    [SerializeField] private float quantity = 1;

    public void GetCollected(){
        Inventory.Instance.UpdateAmmo(GearType, quantity);
        Destroy(gameObject);
    }

    
}
