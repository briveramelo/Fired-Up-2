using UnityEngine;
using System.Collections;

public class RotateInSync : StateMachineBehaviour {

    private bool startRotating;
    private bool stayIn;
    private float period = 3f;
    private float maxTilt = 10f;
    private float pi = Mathf.PI;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        startRotating = true;
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (startRotating) {
            float rotation = maxTilt * Mathf.Cos(1 / period * 2f * Mathf.PI * Time.realtimeSinceStartup);
            if (animator.transform.rotation.eulerAngles.z - rotation < 0.02f) {
                stayIn = true;
            }
            if (stayIn) {
                animator.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            }
        }
	}

	
}
