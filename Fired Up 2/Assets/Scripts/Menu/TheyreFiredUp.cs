using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
public class TheyreFiredUp : MonoBehaviour, IRiftSelectable {

    [SerializeField] private List<EffectSettings> fireEffects;
    [SerializeField] private List<GameObject> fireParts;
    private bool firstTimeIgniting = true;

    public void OnHoverOverObject(){
        if (firstTimeIgniting) {
            firstTimeIgniting = false;
            fireParts.ForEach(firePart => firePart.SetActive(true));
        }
        fireEffects.ForEach(fireEffect => fireEffect.IsVisible = true);
    }

    public void OnHoverExitObject(){
        fireEffects.ForEach(fireEffect => fireEffect.IsVisible = false);
    }

    public void OnHoldForEnoughTime(){
        loadSceneAndStoreData();
    }

    public bool IsSelectable() {
        return true;
    }

    void loadSceneAndStoreData(){
        GameManager.Instance.SetDifficulty(DifficultySelect.DifficultyChoice);
        GameManager.Instance.SetInventory(Gear.BlackDeath, 400);//GearSelectionMenu.BlackholeQuantity);
        GameManager.Instance.SetInventory(Gear.CO2, 400); //GearSelectionMenu.CO2Quantity);
        GameManager.Instance.SetInventory(Gear.K_Bomb, 400); //GearSelectionMenu.KBombQuantity);
        GameManager.Instance.SetInventory(Gear.Powder, 400); //GearSelectionMenu.PowderQuantity);
        GameManager.Instance.SetInventory(Gear.SonicHose, 400); //GearSelectionMenu.SonicQuantity);
        SceneManager.LoadScene((int)LevelSelect.LevelChoice); //Level.Level_1
    }
}
