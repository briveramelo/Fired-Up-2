using UnityEngine;
using System.Collections;
using FU;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ProfileSelector : MonoBehaviour {

    int selectedProfileIndex;
    float currentInput;
    float lastInput;

    void Awake() {
        Controls.SetControls();
    }

    void Update() {
        CheckToHighlightNewProfile();
        CheckToSelectProfile();
    }

    void CheckToHighlightNewProfile() {
        int numProfiles = ProfileLoader.Instance.ButtonProfiles.Count;
        if (numProfiles > 0) {
            currentInput = -Input.GetAxisRaw(Controls.Forward);
            if (currentInput==1 && lastInput!=1 && (selectedProfileIndex -1) >= 0) {
                selectedProfileIndex--;
                Debug.Log(selectedProfileIndex);
                HighlightNewProfile(ProfileLoader.Instance.ButtonProfiles[selectedProfileIndex]);
            }
            else if (currentInput==-1 && lastInput!=-1 && (selectedProfileIndex +1) < numProfiles) {
                selectedProfileIndex++;
                Debug.Log(selectedProfileIndex);
                HighlightNewProfile(ProfileLoader.Instance.ButtonProfiles[selectedProfileIndex]);
            }
            lastInput = currentInput;
        }
    }

    void HighlightNewProfile(ButtonClass highlightedButton) {
        ProfileLoader.Instance.ButtonProfiles.ForEach(button => button.UnHighlightMe());
        highlightedButton.HighlightMe();
    }

    void CheckToSelectProfile() {
        if (Input.GetButtonDown(Controls.Jump)) {
            foreach (ButtonClass button in ProfileLoader.Instance.ButtonProfiles) {
                if (button.IsSelected) {
                    SelectProfile(button);
                }
            }
        }
    }

    void SelectProfile(ButtonClass selectedButton) {
        GameManager.Instance.SetPlayerInfo(selectedButton.MyPlayerInfo);
        SceneManager.LoadScene((int)Level.LevelSelect);
    }
}
