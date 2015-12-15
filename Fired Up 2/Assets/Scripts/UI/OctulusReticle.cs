using UnityEngine;
using System.Collections;

public class OctulusReticle : MonoBehaviour {
    Renderer material;
    bool hasBeenCalled;
	// Use this for initialization
	void Start () {
        material = this.GetComponent<Renderer>();
        material.material.SetFloat("_Cutoff", .01f);
        hasBeenCalled = false;
    }
	

    void OnTriggerStay(Collider col)
    {
        material.material.SetFloat("_Cutoff", material.material.GetFloat("_Cutoff") + .4f * Time.deltaTime);
        if (!hasBeenCalled)
        {
            if (col.gameObject.GetComponent<DifficultySelect>() != null)
            {
                col.gameObject.GetComponent<DifficultySelect>().OnHoverOverObject();
                if (material.material.GetFloat("_Cutoff") >= 1)
                {

                    if (!hasBeenCalled)
                    {
                        hasBeenCalled = true;
                        col.gameObject.GetComponent<DifficultySelect>().OnHoldForEnoughTime();
                    }
                        
                    
                }

            }
            else if (col.gameObject.GetComponent<LevelSelect>() != null)
            {
                col.gameObject.GetComponent<LevelSelect>().OnHoverOverObject();
                if (material.material.GetFloat("_Cutoff") >= 1)
                {
                    if (!hasBeenCalled)
                    {
                        hasBeenCalled = true;
                        col.gameObject.GetComponent<LevelSelect>().OnHoldForEnoughTime();
                    }
                        
                    
                }
            }
            else if (col.gameObject.GetComponent<GearSelectionMenu>() != null)
            {
                col.gameObject.GetComponent<GearSelectionMenu>().OnHoverOverObject();
                if (material.material.GetFloat("_Cutoff") >= 1)
                {
                    Debug.Log("Look at me" + hasBeenCalled);
                    if (!hasBeenCalled)
                    {
                        hasBeenCalled = true;
                        col.gameObject.GetComponent<GearSelectionMenu>().OnHoldForEnoughTime();
                    }
                        
                    
                }
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
        else if (col.gameObject.GetComponent<GearSelectionMenu>() != null)
        {
            col.gameObject.GetComponent<GearSelectionMenu>().OnHoverExitObject();
        }
        material.material.SetFloat("_Cutoff", .01f);
        hasBeenCalled = false;
    }
}
