using UnityEngine;
using System.Collections;

public class ITweenCameraPath : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(cameraPause());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator cameraPause()
    {
        yield return new WaitForSeconds(10f);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraOverlook"), "time", 60));
        yield return null;
    }
}
