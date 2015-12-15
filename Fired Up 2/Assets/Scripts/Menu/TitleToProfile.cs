using UnityEngine;
using System.Collections;
using FU;
using UnityEngine.SceneManagement;

public class TitleToProfile : MonoBehaviour {

    void Awake() {
        Controls.SetControls();
    }

	void Update () {
        if (Input.GetButtonDown(Controls.Jump)) {
            SceneManager.LoadScene((int)Level.ProfileScreen);
        }
	}
}
