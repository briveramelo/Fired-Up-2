using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using FU;

public class NameInputHandler : MonoBehaviour {

    public static NameInputHandler Instance;
    [SerializeField] private InputField inputField;
    /*[HideInInspector]*/ public bool isSelected;

    void Awake() {
        Instance = this;
        Controls.SetControls();
    }

    void Update(){
        if (isSelected) {
            foreach (KeyCode typedKey in letters){
                if (Input.GetKeyDown(KeyCode.Backspace) && inputField.text.Length>0){
                    string newText = inputField.text.Remove(inputField.text.Length - 1, 1);
                    inputField.text = newText;
                    break;
                }
                else if (Input.GetKeyDown(typedKey) && inputField.text.Length < inputField.characterLimit && typedKey !=KeyCode.Backspace){
                    string correctedCase = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? typedKey.ToString().ToUpper() : typedKey.ToString().ToLower();
                    inputField.text += correctedCase;
                    break;
                }
            }

            if (Input.GetButtonDown(Controls.Jump) || Input.GetKeyDown(KeyCode.Return)) {
                SubmitName(inputField.text);
                inputField.text = "";
            }
        }
    }

    void SubmitName(string PlayerName) {
        isSelected = false;
        if (!DataSave.IsRepeatName(PlayerName)) {
            AddNewPlayer(PlayerName);
        }
    }

    void AddNewPlayer(string PlayerName) {
        DataSaver.Instance.CreateNewPlayer(PlayerName);
        ProfileLoader.Instance.LoadProfiles();
        //advance to next screen
    }

    private KeyCode[] letters =
    {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
        KeyCode.Backspace
    };
}
