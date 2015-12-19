using UnityEngine;
using System.Collections;
using FU;
using System.Linq;
public class SafetyZone : MonoBehaviour {

    [SerializeField] private GameObject finishLevel;
    [SerializeField] private GameObject AToLeave;
    public bool allCleared;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == Layers.People.NPC) {
            NPC_Legs npcLegs = col.GetComponent<NPC_Legs>();
            Selectable_Light npcLight = col.GetComponent<Selectable_Light>();
            npcLegs.EnterSafeZone(transform.position);
            npcLight.DisableLight();
        }
        else if (col.gameObject.layer == Layers.People.you){
            Entrance[] allEntrances = FindObjectsOfType<Entrance>();
            allCleared = allEntrances.All(entrance => entrance.HasZoneBeenTriggered);
            if (allCleared) {
                finishLevel.SetActive(true);
                AToLeave.SetActive(true);
            }
        }
    }

    void OnTriggerStay (Collider col) {
        if (col.gameObject.layer == Layers.People.you) {
            

            if (Input.GetButtonDown(Controls.Jump)){
                if (allCleared) {
                    EndOfLevel.Instance.SaveScores();
                    EndOfLevel.Instance.EndLevel();
                }
            }
        }
	}

    void OnTriggerExit(Collider col) {
        if (col.gameObject.layer == Layers.People.you){
            if (allCleared) {
                AToLeave.SetActive(false);
            }
        }
    }
}
