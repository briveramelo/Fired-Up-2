using UnityEngine;
using System.Collections;

public class ProfileLoader : MonoBehaviour {

    public static ProfileLoader Instance;

    [SerializeField] private GameObject FireFighterButton;
    [SerializeField] private Transform ContentPanelTransform;

    IEnumerator Start() {
        Instance = this;
        yield return new WaitForSeconds(0.1f);
        LoadProfiles();
    }

    public void LoadProfiles() {
        if (DataSaver.Instance.DataSaveFile.playersLevelBests != null) {
            foreach (ButtonClass buttonClass in FindObjectsOfType<ButtonClass>()) {
                Destroy(buttonClass.gameObject);
            }
            foreach (PlayerInfo playerInfo in DataSaver.Instance.DataSaveFile.playersLevelBests.Keys) {
                GameObject newButton = (Instantiate(FireFighterButton, transform.position, Quaternion.identity) as GameObject);
                newButton.GetComponent<ButtonClass>().SetPlayerInfo(playerInfo);
                newButton.transform.parent = ContentPanelTransform;
            }
        }
    }
}
