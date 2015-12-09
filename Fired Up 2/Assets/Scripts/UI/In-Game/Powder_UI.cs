using UnityEngine;
using System.Collections;

public class Powder_UI : UI_Animations {

	[SerializeField] private ParticleSystem powderParticles;

    void Awake(){
		StartCoroutine (PlaySmoke(2f));
	}

	IEnumerator PlaySmoke(float timeToPause){
        powderParticles.Stop();
        powderParticles.Play();

		StartCoroutine (PauseSmoke(timeToPause));
        yield return null;
	}

	IEnumerator PauseSmoke(float timeToPause){
		yield return new WaitForSeconds(timeToPause);
        powderParticles.Pause();
    }


	public override void ActivateUI(){
		base.ActivateUI();
		StartCoroutine (PlaySmoke(.75f));
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
		StartCoroutine(PauseSmoke(0f));
	}
}
