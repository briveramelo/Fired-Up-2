using UnityEngine;
using System.Collections;

public class GrenadeSelector_UI : GearSelector_UI {

    new public static GrenadeSelector_UI Instance;


    void Start() {
        Instance = this;
        HighlightActiveIcon();
    }

    public void HighlightActiveIcon(){
        if (CurrentGrenade == Gear.K_Bomb) k_Bomb_UI.ActivateUI();
        else k_Bomb_UI.DeActivateUI();

        if (CurrentGrenade == Gear.BlackDeath) blackDeath_UI.ActivateUI();
        else blackDeath_UI.DeActivateUI();

        UIGearSelectorAnimator.SetInteger("AnimState", (int)CurrentGrenade);
    }
}
