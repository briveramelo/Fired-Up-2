using UnityEngine;
using System.Collections;

public class HoseSelector_UI : Inventory {

    [SerializeField] private Animator UIHoseSelectorAnimator;
    new public static HoseSelector_UI Instance;

    void Start() {
        Instance = this;
        HighlightActiveIcon();
    }

    public void HighlightActiveIcon(){
        if (CurrentHose == Gear.SonicHose)      sonicHose_UI.ActivateUI();
        else                                    sonicHose_UI.DeActivateUI();

        if (CurrentHose == Gear.CO2)            cO2_UI.ActivateUI();
        else                                    cO2_UI.DeActivateUI();

        if (CurrentHose == Gear.Powder)         powder_UI.ActivateUI();
        else                                    powder_UI.DeActivateUI();

        UIHoseSelectorAnimator.SetInteger("AnimState", (int)CurrentHose);
    }
}
