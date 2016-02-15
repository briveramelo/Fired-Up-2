using UnityEngine;
using System.Collections;

public class CO2_UI : UI_Animations {

    [SerializeField] private ParticleSystem CO2Particles;

    void Awake(){
		StartCoroutine (PlaySmoke(2f));
	}

	IEnumerator PlaySmoke(float timeToPause){
        CO2Particles.Stop();
        CO2Particles.Play();

		StartCoroutine (PauseSmoke(timeToPause));
        yield return null;
	}

	IEnumerator PauseSmoke(float timeToPause){
		yield return new WaitForSeconds(timeToPause);
        CO2Particles.Pause();
    }


	public override void ActivateUI(){
		base.ActivateUI();
		StartCoroutine (PlaySmoke(0.75f));
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
		StartCoroutine(PauseSmoke(0f));
	}
	
}
