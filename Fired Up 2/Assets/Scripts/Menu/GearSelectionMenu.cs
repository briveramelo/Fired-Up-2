using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearSelectionMenu : MonoBehaviour {
    public bool isThresholdValue;
    public bool isCostValue;
    public bool isSelected;
    public enum Gear {Co2,Powder,Sonic,IceGrenade,BlackHole};
    public Gear gear;
    public int cost;
    public int ThresholdValue;
    public int quantity;
    static int pointRemaining;
    static int CO2costValue;
    static int PowdercostValue;
    static int SoniccostValue;
    static int IceGrenadecostValue;
    static int BlackHolecostValue;
    static int CO2ThresholdValue;
    static int PowderThresholdValue;
    static int SonicThresholdValue;
    static int IceGrenadeThresholdValue;
    static int BlackHoleThresholdValue;
    TextMesh textMesh;
    BoxCollider boxCollider = null;
    GearSelectionMenu[] allGearQuatities = new GearSelectionMenu[35];
    List<GearSelectionMenu> otherQuantitiesOfThisGear = new List<GearSelectionMenu>();
    public TextMesh remainingPointsMesh;
    // Use this for initialization
    void Awake()
    {
        if (isCostValue)
        {
            if (gear == Gear.BlackHole)
            {
                BlackHolecostValue = cost;
            }
            else if (gear == Gear.Co2)
            {
                CO2costValue = cost;
            }
            else if (gear == Gear.IceGrenade)
            {
                IceGrenadecostValue = cost;
            }
            else if (gear == Gear.Powder)
            {
                PowdercostValue = cost;
            }
            else if (gear == Gear.Sonic)
            {
                SoniccostValue = cost;
            }

        }
        else if (isThresholdValue)
        {
            if (gear == Gear.BlackHole)
            {
                BlackHoleThresholdValue = ThresholdValue;
            }
            else if (gear == Gear.Co2)
            {
                CO2ThresholdValue = ThresholdValue;
            }
            else if (gear == Gear.IceGrenade)
            {
                IceGrenadeThresholdValue = ThresholdValue;
            }
            else if (gear == Gear.Powder)
            {
                PowderThresholdValue = ThresholdValue;
            }
            else if (gear == Gear.Sonic)
            {
                SonicThresholdValue = ThresholdValue;
            }
            pointRemaining = 30;
        }
            
    }
	void Start () {
        textMesh = this.GetComponent<TextMesh>();
        if (gear == Gear.BlackHole)
        {
            ThresholdValue = BlackHoleThresholdValue;
            cost = BlackHolecostValue;
        }
        else if (gear == Gear.Co2)
        {
            ThresholdValue = CO2ThresholdValue;
            cost = CO2costValue;
        }
        else if (gear == Gear.IceGrenade)
        {
            ThresholdValue = IceGrenadeThresholdValue;
            cost = IceGrenadecostValue;
        }
        else if (gear == Gear.Powder)
        {
            ThresholdValue = PowderThresholdValue;
            cost = PowdercostValue;
        }
        else if (gear == Gear.Sonic)
        {
            ThresholdValue = SonicThresholdValue;
            cost = SoniccostValue;
        }
        if (gameObject.GetComponent<BoxCollider>() != null)
            boxCollider = gameObject.GetComponent<BoxCollider>();
        if ((ThresholdValue > pointRemaining) || ((cost * quantity) > pointRemaining))
            DisableSelection();
        if (isSelected)
            isSelectedQuantity();
        allGearQuatities = FindObjectsOfType<GearSelectionMenu>();
        FindGearOfMyType();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void FindGearOfMyType()
    {
        for (int i = 0; i < allGearQuatities.Length; i++)
        {
            Debug.Log(allGearQuatities[i].gear + "Compared too" + gear);
            if (allGearQuatities[i].gear.Equals(gear))
                otherQuantitiesOfThisGear.Add(allGearQuatities[i]);
        }
    }
    void DisableSelection()
    {
        textMesh.color = Color.grey;
        if (boxCollider != null)
            boxCollider.enabled = false;
    }
    void EnableSelection()
    {
        textMesh.color = Color.white;
        if (boxCollider != null)
            boxCollider.enabled = true;
    }
    void isSelectedQuantity()
    {
        isSelected = true;
        textMesh.color = Color.blue;
        boxCollider.enabled = false;
    }
    public void OnHoverOverObject()
    {
        textMesh.color = Color.cyan;
    }
    public void OnHoverExitObject()
    {
        checkCurrentState();
    }
    public void OnHoldForEnoughTime()
    {
        Debug.Log("1Remaining Points" + pointRemaining);
            Debug.Log("Quantity" + quantity + "Cost" + cost);
            pointRemaining -= (quantity * cost);
        for (int i = 0; i < otherQuantitiesOfThisGear.Count; i++)
        {
            
            GearSelectionMenu temp = otherQuantitiesOfThisGear[i];
            if (temp != this)
            {
                Debug.Log("2Remaining Points" + pointRemaining);
                if (temp.isSelected == true)
                    pointRemaining += (temp.quantity * temp.cost);
                Debug.Log("3Remaining Points" + pointRemaining);
                temp.isSelected = false;
                temp.checkCurrentState();
            }
            
        }
        
        isSelectedQuantity();
        checkAllCurrentState();
        remainingPointsMesh.text = pointRemaining.ToString();
    }
    void checkAllCurrentState()
    {
        for (int i = 0; i < allGearQuatities.Length; i++)
        {
            if (allGearQuatities[i] != null)
            {
                allGearQuatities[i].checkCurrentState();
            }
        }
    }
    public void checkCurrentState()
    {

         if (isSelected)
            isSelectedQuantity();
         else if (((ThresholdValue > pointRemaining) || ((cost * quantity) > pointRemaining)) && quantity != 0 && !isLessThenCurrentlySelected())
            DisableSelection();
        else
            EnableSelection();
    }
    public bool isLessThenCurrentlySelected()
    {
        for (int i = 0; i < otherQuantitiesOfThisGear.Count; i++)
        {
            if (otherQuantitiesOfThisGear[i].isSelected && otherQuantitiesOfThisGear[i].quantity > quantity)
                return true;
        }
        return false;
    }
}
