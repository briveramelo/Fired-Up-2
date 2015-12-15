using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ProfileLoader : MonoBehaviour {

    public static ProfileLoader Instance;

    [SerializeField] private GameObject FireFighterButton;
    [SerializeField] private RectTransform ContentPanelTransform;
    [SerializeField] private ToggleGroup toggleGroup;
    private List<ButtonClass> buttonProfiles; public List<ButtonClass> ButtonProfiles { get { return buttonProfiles; } }

    IEnumerator Start() {
        Instance = this;
        buttonProfiles = new List<ButtonClass>();
        yield return new WaitForSeconds(0.1f);
        LoadProfiles();
    }

    public void LoadProfiles() {
        if (DataSaver.Instance.DataSaveFile.playersLevelBests != null) {
            buttonProfiles.Clear();
            int i = 0;
            foreach (PlayerInfo playerInfo in DataSaver.Instance.DataSaveFile.playersLevelBests.Keys) {
                GameObject newButton = (Instantiate(FireFighterButton, transform.position, Quaternion.identity) as GameObject);
                ButtonClass button = newButton.GetComponent<ButtonClass>();
                if (i == 0)
                    button.HighlightMe();
                button.SetPlayerInfo(playerInfo);
                buttonProfiles.Add(button);
                newButton.transform.parent = ContentPanelTransform;
                i++;
            }
        }
    }
}
