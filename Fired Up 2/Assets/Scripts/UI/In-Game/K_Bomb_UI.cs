using UnityEngine;
using System.Collections;

public class K_Bomb_UI : UI_Animations {


	[SerializeField]	private ParticleSystem iceFog;
	[SerializeField]	private ParticleSystem iceEruption;
    private Material iceFogMaterial;
    private Material iceEruptionMaterial;

    private Color unselectedIceFogColor =       new Color(  75f /255f,  145f /255f,  214f /255f,  7f   /255f);
    private Color selectedIceFogColor =         new Color(  75f /255f,  145f /255f,  214f /255f,  128f /255f);
    private Color unselectedIceEruptionColor =  new Color(  75f /255f,  136f /255f,  255f /255f,  45f  /255f);
    private Color selectedIceEruptionColor =    new Color(  75f /255f,  136f /255f,  255f /255f,  147f /255f);

    void Awake(){
        iceFogMaterial = iceFog.GetComponent<ParticleSystemRenderer>().material;
        iceEruptionMaterial = iceEruption.GetComponent<ParticleSystemRenderer>().material;
        StartCoroutine (PlayIce());
	}

	IEnumerator PlayIce(){
		iceFog.Stop();
		iceEruption.Stop();
		
		iceFog.Play();
		iceEruption.Play();
		yield return new WaitForSeconds(0.3f);
		StartCoroutine (PauseIce(0f));
	}

	IEnumerator PauseIce(float timeToPause){
		yield return new WaitForSeconds(timeToPause);
		iceFog.Pause();
		iceEruption.Pause();
	}


	public override void ActivateUI(){
		base.ActivateUI();
        iceFogMaterial.SetColor("_TintColor", selectedIceFogColor);
        iceEruptionMaterial.SetColor("_TintColor", selectedIceEruptionColor);
        StartCoroutine (PlayIce());
	}

	public override void DeActivateUI(){
		base.DeActivateUI();
        iceFogMaterial.SetColor("_TintColor", unselectedIceFogColor);
        iceEruptionMaterial.SetColor("_TintColor", unselectedIceEruptionColor);
        StartCoroutine(PauseIce(0f));
	}



}
