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
        if (CurrentGear == GearEnum.SonicHose)  sonicHose_UI.ActivateUI();
        else                                    sonicHose_UI.DeActivateUI();

        if (CurrentGear == GearEnum.CO2)        cO2_UI.ActivateUI();
        else                                    cO2_UI.DeActivateUI();

        if (CurrentGear == GearEnum.Powder)     powder_UI.ActivateUI();
        else                                    powder_UI.DeActivateUI();

        if (CurrentGear == GearEnum.K_Bomb)     k_Bomb_UI.ActivateUI();
        else                                    k_Bomb_UI.DeActivateUI();

        if (CurrentGear == GearEnum.BlackDeath) blackDeath_UI.ActivateUI();
        else                                    blackDeath_UI.DeActivateUI();

        UIMovementAnimator.SetInteger("AnimState", (int)CurrentGear);
    }
}
