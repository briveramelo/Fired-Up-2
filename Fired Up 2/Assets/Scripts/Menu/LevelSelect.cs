using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {
    public static int levelChoice;
    int MyLevel;
    public Level levelEnum;
    Light light;
    public static Light chosenLight;
    float startIntensity;
    public bool isDefault;
    LevelSelect[] lights = new LevelSelect[5];
    float startingRange;
	// Use this for initialization
	void Start () {
        MyLevel = (int)levelEnum;
        light = GetComponent<Light>();
        lights = FindObjectsOfType<LevelSelect>();
        startIntensity = light.intensity;
        startingRange = light.range;
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
        this.StopAllCoroutines();
      
    }
    public void OnHoverExitObject()
    {
        this.StopAllCoroutines();
        Debug.Log(levelChoice);
        if (MyLevel != levelChoice)
        {

            light.intensity = 0;
            light.range = startingRange;
        }
        else
        {
            Debug.Log(light.name + "This is alskdhf;l");
            light.intensity = startIntensity;
           light.range = startingRange;
        }
            
    }
    public void OnHoldForEnoughTime()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].MyLevel != MyLevel)
            {
                lights[i].light.intensity = 0;
                light.range = startingRange;
            }
            

        }
        //light.intensity = startIntensity;
       // StartCoroutine(ChangeLight(1, light));
        levelChoice = MyLevel;
        chosenLight = light;
        this.StopAllCoroutines();
    }
    public IEnumerator ChangeLight(float LightOrDark, Light light)
    {
        float intensityMax = 3;
        float intensityMin= 0;
        float rangeMax = 4;
        float rangeMin = 0;
        Debug.Log(light.name + "  " + LightOrDark);
        light.intensity = Mathf.Clamp(light.intensity + (LightOrDark * Time.deltaTime),0,intensityMax);
        light.range = Mathf.Clamp(light.range + (LightOrDark * Time.deltaTime), rangeMin, rangeMax);
        if (light.intensity == intensityMax || light.intensity == intensityMin)
            light.intensity = startIntensity;
        if (light.range == intensityMax || light.range == intensityMin)
            light.range = startingRange;
        yield return new WaitForSeconds(.1f);
    }

}
