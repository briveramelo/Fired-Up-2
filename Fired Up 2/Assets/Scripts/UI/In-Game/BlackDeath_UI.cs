using UnityEngine;
using System.Collections;

public class BlackDeath_UI : UI_Animations {
	
	[SerializeField] private EffectSettings blackDeathEffectSettings;
	[SerializeField] private RotateAround[] rotateAroundScripts;
	[SerializeField] private UVTextureAnimator texAnimScript;
	[SerializeField] private ParticleSystem rockParticleSystem;
	[SerializeField] private ParticleSystem dustParticleSystem;

    [SerializeField] private GameObject jet; private Material jetMaterial;
    [SerializeField] private GameObject dust; private Material dustMaterial;
    [SerializeField] private GameObject distortion; private Material distortionMaterial;
    [SerializeField] private GameObject distortionCore; private Material distortionCoreMaterial;
    [SerializeField] private GameObject rocks;

    private Color unselectedJetColor =          new Color(  25f /255f,  17f /255f,  128f /255f,  60f  /255f);
    private Color selectedJetColor =            new Color(  25f /255f,  17f /255f,  128f /255f,  255f /255f);

    private Color unselectedDustColor =         new Color(  128f /255f,  128f /255f,  128f /255f,  4f  /255f);
    private Color selectedDustColor =           new Color(  128f /255f,  128f /255f,  128f /255f,  12f /255f);

    private Color unselectedDistortionColor =   new Color(  128f /255f,  128f /255f,  128f /255f,  10f  /255f);
    private Color selectedDistortionColor =     new Color(  128f /255f,  128f /255f,  128f /255f,  255f /255f);

    private Color unselectedDistortionCoreColor=new Color(  25f /255f,  17f /255f,  128f /255f,  0f /255f);
    private Color selectedDistortionCoreColor=  new Color(  25f /255f,  17f /255f,  128f /255f,  255f /255f);

    void Awake(){
        dustMaterial = dustParticleSystem.GetComponent<ParticleSystemRenderer>().material;
        jetMaterial = jet.GetComponent<MeshRenderer>().material;
        distortionMaterial = distortion.GetComponent<MeshRenderer>().material;
        distortionCoreMaterial = distortionCore.GetComponent<MeshRenderer>().material;

        DeActivateUI();
    }

	public override void ActivateUI(){
		base.ActivateUI();

        jetMaterial.SetColor("_Color", selectedJetColor);
        dustMaterial.SetColor("_TintColor", selectedDustColor);
        distortionMaterial.SetColor("_TintColor", selectedDistortionColor);
        distortionCoreMaterial.SetColor("_TintColor", selectedDistortionCoreColor);
        rocks.SetActive(true);

        StartCoroutine (ToggleActivation(true));
		StartCoroutine (DelayedDeActivation());
	}
	
	public override void DeActivateUI(){
		base.DeActivateUI();

        jetMaterial.SetColor("_Color", unselectedJetColor);
        dustMaterial.SetColor("_TintColor", unselectedDustColor);
        distortionMaterial.SetColor("_TintColor", unselectedDistortionColor);
        distortionCoreMaterial.SetColor("_TintColor", unselectedDistortionCoreColor);
        rocks.SetActive(false);

        StartCoroutine (ToggleActivation(false));
	}

	IEnumerator ToggleActivation(bool isOn){
		texAnimScript.enabled = isOn;
		if (isOn){
			rockParticleSystem.Play();
			dustParticleSystem.Play();
        } 
		else{
            rockParticleSystem.Pause();
			dustParticleSystem.Pause();
        }
		foreach (RotateAround rot in rotateAroundScripts){
			rot.enabled = isOn;
		}
		yield return null;
	}

	IEnumerator DelayedDeActivation(){
		yield return new WaitForSeconds(1f);
		StartCoroutine (ToggleActivation(false));
	}
}
