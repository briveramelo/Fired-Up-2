using UnityEngine;
using System.Collections;

public class OctulusReticle : MonoBehaviour {
    Renderer material;
	// Use this for initialization
	void Start () {
        material = this.GetComponent<Renderer>();
        material.material.SetFloat("_Cutoff", .01f);
    }
	

    void OnTriggerStay(Collider col)
    {
        material.material.SetFloat("_Cutoff", material.material.GetFloat("_Cutoff") + .4f * Time.deltaTime);
        if (col.gameObject.GetComponent<DifficultySelect>() != null)
        {
            col.gameObject.GetComponent<DifficultySelect>().OnHoverOverObject();
            if (material.material.GetFloat("_Cutoff") >= 1)
            {

                col.gameObject.GetComponent<DifficultySelect>().OnHoldForEnoughTime();
            }
            
        }
        else if (col.gameObject.GetComponent<LevelSelect>() != null)
        {
            col.gameObject.GetComponent<LevelSelect>().OnHoverOverObject();
            if (material.material.GetFloat("_Cutoff") >= 1)
            {

                col.gameObject.GetComponent<LevelSelect>().OnHoldForEnoughTime();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<DifficultySelect>() != null)
        {
            col.gameObject.GetComponent<DifficultySelect>().OnHoverExitObject();
        }
        else if (col.gameObject.GetComponent<LevelSelect>() != null)
        {
            col.gameObject.GetComponent<LevelSelect>().OnHoverExitObject();
        }
            material.material.SetFloat("_Cutoff", .01f);
    }
}
