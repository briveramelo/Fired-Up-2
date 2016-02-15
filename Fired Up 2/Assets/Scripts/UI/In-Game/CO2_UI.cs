using UnityEngine;
using System.Collections;

public class CO2_UI : UI_Animations {

    [SerializeField] private ParticleSystem CO2Particles;
    private Material CO2Material;

    private Color unselectedC02Color = new Color(  141f /255f,  141f /255f,  141f /255f,  25f  /255f);
    private Color selectedC02Color =   new Color(  141f /255f,  141f /255f,  141f /255f,  172f /255f);

    void Awake(){
        CO2Material = CO2Particles.GetComponent<ParticleSystemRenderer>().material;
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
        CO2Material.SetColor("_TintColor", selectedC02Color);
        StartCoroutine (PlaySmoke(0.75f));
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
        CO2Material.SetColor("_TintColor", unselectedC02Color);
        StartCoroutine(PauseSmoke(0f));
	}
	
}
