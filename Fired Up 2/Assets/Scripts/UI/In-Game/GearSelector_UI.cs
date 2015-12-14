using UnityEngine;
using System.Collections;

public class GearSelector_UI : Inventory {

    [SerializeField] private Animator UIMovementAnimator;
    new public static GearSelector_UI Instance;

    void Start() {
        Instance = this;
        HighlightActiveGearIcon();
    }

    public void HighlightActiveGearIcon()
    {
        if (CurrentGear == Gear.SonicHose)      sonicHose_UI.ActivateUI();
        else                                    sonicHose_UI.DeActivateUI();

        if (CurrentGear == Gear.CO2)            cO2_UI.ActivateUI();
        else                                    cO2_UI.DeActivateUI();

        if (CurrentGear == Gear.Powder)         powder_UI.ActivateUI();
        else                                    powder_UI.DeActivateUI();

        if (CurrentGear == Gear.K_Bomb)         k_Bomb_UI.ActivateUI();
        else                                    k_Bomb_UI.DeActivateUI();

        if (CurrentGear == Gear.BlackDeath)     blackDeath_UI.ActivateUI();
        else                                    blackDeath_UI.DeActivateUI();

        UIMovementAnimator.SetInteger("AnimState", (int)CurrentGear);
    }
}
