using UnityEngine;
using System.Collections;
using FU;
public class Collectable : MonoBehaviour {

    [SerializeField] GearEnum GearType;

    public void GetCollected(){
        Inventory.Instance.UpdateAmmo(GearType, true);
        Destroy(gameObject);
    }

    
}
