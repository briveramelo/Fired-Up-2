using UnityEngine;
using System.Collections;
using FU;
public class ContentPanelMover : MonoBehaviour {

    private RectTransform myRectTran;
    private float movementMultiplier = 3f;

    void Awake() {
        myRectTran = GetComponent<RectTransform>();
    }

	void Update () {
        float direction = Input.GetAxisRaw(Controls.Forward) * movementMultiplier;
        Vector3 newPosition = new Vector3(myRectTran.position.x, myRectTran.position.y + direction, myRectTran.position.z);
        myRectTran.position = newPosition;
    }
}
