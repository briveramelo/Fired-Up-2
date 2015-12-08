using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {

	[SerializeField] private TextMesh textToDisplay;
    [SerializeField] AudioSource soundPlayer;

    void OnEnable(){
        soundPlayer.Play();
    }
}
