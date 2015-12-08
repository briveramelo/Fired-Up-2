using UnityEngine;
using System.Collections;
using FU;

public class PlayerDeath : MonoBehaviour {
    // Use this for initialization
    static bool playerDead = false;
    public static PlayerDeath playerDeath;

    void Start()
    {
        playerDead = this;
    }
	 void Update () {
	if(playerDead)
        {
            
            if (Input.GetButtonDown(Controls.Jump))
                Application.LoadLevel(Application.loadedLevelName);
        }
            
	}
   /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("FireFighter"))
        {
            KillPlayer(); 

        }
    }*/
    
    public void KillPlayer()
    {
        //Debug.Log("Player Died");
        playerDead = true;
        Player.player.StopMovements();
        StartCoroutine(CameraDeathRotation());
    }

    IEnumerator CameraDeathRotation()
    {
        while(Camera.main.transform.rotation.eulerAngles.z < 75){
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 80)),.5f*Time.deltaTime);
            yield return null;
        }
        
    }
}
