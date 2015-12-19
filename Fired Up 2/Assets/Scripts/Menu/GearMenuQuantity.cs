using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GearMenuQuantity : MonoBehaviour, IRiftSelectable {

    [SerializeField] private Gear myGear;       public Gear MyGear { get { return myGear; } }

    public bool isSelected;
    [SerializeField] private int quantity;
    private int cost;
    private int netCost;     public int NetCost { get { return netCost; } }

    private int earnedThreshold;
 
    int numberOfStartingPoints;
    private Color selectedColor = Color.blue;
    private Color highligthedColor = Color.cyan;
    private Color selectableColor = Color.white;
    private Color unSelectableColor = Color.grey;

    TextMesh myQuantityText;
    GearMenuQuantity[] gearMenuQuantityScripts;
    List<GearMenuQuantity> otherQuantitiesOfThisGear = new List<GearMenuQuantity>();

	void Start (){
        myQuantityText = GetComponent<TextMesh>();
        otherQuantitiesOfThisGear = FindObjectsOfType<GearMenuQuantity>().Where(gearQuantity => gearQuantity.MyGear == MyGear && gearQuantity != this).ToList();
        numberOfStartingPoints = GearSelectionMenu.Instance.PointsRemaining;

        SetThresholdAndCost();

        if ((earnedThreshold > GearSelectionMenu.Instance.PointsRemaining) || (netCost > GearSelectionMenu.Instance.PointsRemaining))
            DisableSelection();
        if (isSelected)
            SelectThisQuantity();

    }
    #region SetThresholdAndCost
    void SetThresholdAndCost() {
        switch (myGear){
            case Gear.BlackDeath:
                earnedThreshold =   GearSelectionMenu.Instance.BlackHole_Threshold;
                cost =              GearSelectionMenu.Instance.BlackHole_Cost;
                break;
            case Gear.CO2:
                earnedThreshold =   GearSelectionMenu.Instance.CO2_Threshold;
                cost =              GearSelectionMenu.Instance.CO2_Cost;
                break;
            case Gear.K_Bomb:
                earnedThreshold =   GearSelectionMenu.Instance.IceGrenade_Threshold;
                cost =              GearSelectionMenu.Instance.IceGrenade_Cost;
                break;
            case Gear.Powder:
                earnedThreshold =   GearSelectionMenu.Instance.Powder_Threshold;
                cost =              GearSelectionMenu.Instance.Powder_Cost;
                break;
        }
        netCost = quantity * cost;
    }
    #endregion


    public void OnHoverOverObject(){
        if (!isSelected)
            myQuantityText.color = highligthedColor;
    }

    public void OnHoverExitObject(){
        SetCurrentState();
    }

    public void SetCurrentState(){
        if (isSelected)
            SelectThisQuantity();
        else if (!IsSelectable())
            DisableSelection();
        else
            EnableSelection();
    }

    public void OnHoldForEnoughTime(){
        for (int i = 0; i < otherQuantitiesOfThisGear.Count; i++){
            otherQuantitiesOfThisGear[i].isSelected = false;
            otherQuantitiesOfThisGear[i].SetCurrentState();
        }
        
        SelectThisQuantity();
        GearSelectionMenu.Instance.SetAllGearQuantityStates();
    }

    public bool IsSelectable() {
        bool thresholdIsMet = numberOfStartingPoints >= earnedThreshold;
        bool isAffordable = netCost <= (GearSelectionMenu.Instance.PointsRemaining + SelectedPeerGearNetCost());

        return ( !isSelected && thresholdIsMet && isAffordable );
    }

    void SelectThisQuantity(){
        isSelected = true;
        myQuantityText.color = selectedColor;
        GearSelectionMenu.Instance.SetGearQuantity(MyGear, quantity);
    }

    void DisableSelection(){
        myQuantityText.color = unSelectableColor;
    }

    void EnableSelection(){
        myQuantityText.color = selectableColor;
    }

    int SelectedPeerGearNetCost(){
        if (isSelected)
            return netCost;

        for (int i = 0; i < otherQuantitiesOfThisGear.Count; i++){
            if (otherQuantitiesOfThisGear[i].isSelected)
                return otherQuantitiesOfThisGear[i].NetCost;
        }

        return 0;
    }
}
