using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GearSelectionMenu : MonoBehaviour {

    public static GearSelectionMenu Instance;

    public static int PowderQuantity_Selected;
    public static int CO2Quantity_Selected;
    public static int KBombQuantity_Selected;
    public static int BlackholeQuantity_Selected;

    [SerializeField] private TextMesh remainingPointsMesh;

    [SerializeField] private int cO2_Cost;              public int CO2_Cost { get { return cO2_Cost; } }
    [SerializeField] private int powder_Cost;           public int Powder_Cost { get { return powder_Cost; } }
    [SerializeField] private int iceGrenade_Cost;       public int IceGrenade_Cost { get { return iceGrenade_Cost; } }
    [SerializeField] private int blackHole_Cost;        public int BlackHole_Cost { get { return blackHole_Cost; } }

    [SerializeField] private int cO2_Threshold;         public int CO2_Threshold { get { return cO2_Threshold; } }
    [SerializeField] private int powder_Threshold;      public int Powder_Threshold { get { return powder_Threshold; } }
    [SerializeField] private int iceGrenade_Threshold;  public int IceGrenade_Threshold { get { return iceGrenade_Threshold; } }
    [SerializeField] private int blackHole_Threshold;   public int BlackHole_Threshold { get { return blackHole_Threshold; } }

    private int pointsRemaining;    public int PointsRemaining { get { return pointsRemaining; } }
    private int startingPoints { get { return 90; } }
    List<GearMenuQuantity> allGearQuantities;

    void Awake() {
        Instance = this;
        pointsRemaining = startingPoints;
        allGearQuantities = FindObjectsOfType<GearMenuQuantity>().ToList();
    }

    public void SetGearQuantity(Gear GearType, int quantity) {
        switch (GearType) {
            case Gear.BlackDeath:   BlackholeQuantity_Selected = quantity; break;
            case Gear.CO2:          CO2Quantity_Selected =       quantity; break;
            case Gear.K_Bomb:       KBombQuantity_Selected =     quantity; break;
            case Gear.Powder:       PowderQuantity_Selected =    quantity; break;
        }
        ReCalculatePointsRemaining();
    }

    public void ReCalculatePointsRemaining() {
        pointsRemaining = startingPoints;
        allGearQuantities.ForEach(gearQuantity => pointsRemaining -= gearQuantity.isSelected ? gearQuantity.NetCost : 0);
        remainingPointsMesh.text = pointsRemaining.ToString();
    }

    public void SetAllGearQuantityStates() {
        allGearQuantities.ForEach(gearQuantity => gearQuantity.SetCurrentState());
    }
}
