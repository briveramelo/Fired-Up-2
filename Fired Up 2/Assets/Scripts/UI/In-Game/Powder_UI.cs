using UnityEngine;
using System.Collections;

public class Powder_UI : UI_Animations {

	[SerializeField] private ParticleSystem powderParticles;

    private Material powderMaterial;

    private Color unselectedPowderColor =   new Color(  118f /255f,  210f /255f,  255f /255f,  25f  /255f);
    private Color selectedPowderColor =     new Color(  118f /255f,  210f /255f,  255f /255f,  172f /255f);

    void Awake(){
        powderMaterial = powderParticles.GetComponent<ParticleSystemRenderer>().material;
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
        powderMaterial.SetColor("_TintColor", selectedPowderColor);
        StartCoroutine (PlaySmoke(.75f));
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
        powderMaterial.SetColor("_TintColor", unselectedPowderColor);
        StartCoroutine(PauseSmoke(0f));
	}
}
