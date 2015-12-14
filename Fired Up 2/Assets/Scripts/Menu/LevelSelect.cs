using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {
    public static int levelChoice;
    public int MyLevel;
    Light light;
    public static Light chosenLight;
    float startIntensity;
    public bool isDefault;
    LevelSelect[] lights = new LevelSelect[5];
	// Use this for initialization
	void Start () {
        
        light = GetComponent<Light>();
        lights = FindObjectsOfType<LevelSelect>();
        startIntensity = light.intensity;
        Debug.Log(startIntensity);
        if (!isDefault)
            light.intensity = 0;
        else
        {
            chosenLight = light;
            levelChoice = MyLevel;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnHoverOverObject()
    {
        light.intensity = startIntensity;
        StartCoroutine(ChangeLight(1,light));
        if (chosenLight != light)
        {
            StartCoroutine(ChangeLight(-.75f, chosenLight));
        }

      
    }
    public void OnHoverExitObject()
    {
        StopAllCoroutines();
        Debug.Log(levelChoice);
        if (MyLevel != levelChoice)
        {

            light.intensity = 0;
        }
        else
            light.intensity = startIntensity;
            
    }
    public void OnHoldForEnoughTime()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].MyLevel != MyLevel)
            lights[i].light.intensity = 0;

        }
        //light.intensity = startIntensity;
       // StartCoroutine(ChangeLight(1, light));
        levelChoice = MyLevel;
        chosenLight = light;
    }
    public IEnumerator ChangeLight(float LightOrDark, Light light)
    {
        Debug.Log(light.name + "  " + LightOrDark);
        light.intensity = Mathf.Clamp(light.intensity + (LightOrDark * Time.deltaTime),0,3);
        light.range = Mathf.Clamp(light.range + (LightOrDark * Time.deltaTime), 0, 4);
        yield return new WaitForSeconds(.1f);
    }

}
