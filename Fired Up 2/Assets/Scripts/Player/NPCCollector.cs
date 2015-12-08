using UnityEngine;
using System.Collections;
using FU;
public class NPCCollector : MonoBehaviour {

    public static NPCCollector Instance;

    [SerializeField] private Legs myLegs;
    private NPC_Legs npcLegs_Carrying;
    private float communicationDistance = 7f;
    private float dropOffDistance = 3f;
    private bool isCarryingPlayer;
    public bool IsCarryingPlayer { get { return isCarryingPlayer; } }
    private bool follow = true;
    private bool unFollow = false;

    private enum Dictation {
        FollowMe = 0,
        PickUp = 1,
        Highlight = 2,
        UnHighlight = 3
    }

    void Awake() {
        Instance = this;
    }

    void Update() {
        if (Input.GetButtonDown(Controls.FollowMe)) {
            if (!isCarryingPlayer)
                CheckForFollow(Dictation.FollowMe);
        }
        else if (Input.GetButtonDown(Controls.PickUpPlayer)) {
            if (!isCarryingPlayer)
                CheckForFollow(Dictation.PickUp);
            else
                DropPlayer();
        }
    }

    void CheckForFollow(Dictation Command) {
        RaycastHit[] npcHit;
        npcHit = Physics.SphereCastAll(transform.position, 0.5f, transform.forward, communicationDistance);
        if (npcHit.Length > 0) {
            foreach (RaycastHit rayHit in npcHit) {
                if (rayHit.collider.GetComponent<NPC_Legs>()) {
                    switch (Command) {
                        case Dictation.FollowMe:
                            TellToFollowMe(rayHit.collider.GetComponent<NPC_Legs>());
                            return;
                        case Dictation.PickUp:
                            PickUpPlayer(rayHit.collider.GetComponent<NPC_Legs>());
                            return;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Selectable_Light>()) {
            Highlight(col, true);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.GetComponent<Selectable_Light>()) {
            Highlight(col, false);
        }
    }

    void Highlight(Collider col, bool highlight) {
        Selectable_Light npcLight = col.GetComponent<Selectable_Light>();
        npcLight.IsHoveredOver = highlight;
        if (highlight)
            npcLight.HighlightOnHover();            
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * communicationDistance);
    }

    void TellToFollowMe(NPC_Legs npcLegs) {
        npcLegs.ToggleFollow();
    }

    void PickUpPlayer(NPC_Legs npcLegs) {
        isCarryingPlayer = true;

        npcLegs_Carrying = npcLegs;
        npcLegs_Carrying.ToggleFollow(unFollow);
        npcLegs_Carrying.PickUp();
        myLegs.ImmobilizeLegs(npcLegs);
    }

    void DropPlayer() {
        isCarryingPlayer = false;
        npcLegs_Carrying.DropOff(dropOffDistance);
        myLegs.ImmobilizeLegs(npcLegs_Carrying);
    }
}
