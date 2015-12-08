using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FU;
public class Player : MonoBehaviour {
    public static Player player;
    public List<MonoBehaviour> movements;
    static bool playerDead = false;
    // Use this for initialization
    void Start () {
        player = this;
	}

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {

            if (Input.GetButtonDown(Controls.Jump))
                Application.LoadLevel(Application.loadedLevelName);
        }

    }
    public void StopMovements()
    {
     for(int i = 0; i <  movements.Count; i++)
        {
            movements[i].enabled = false;
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
        while (Camera.main.transform.rotation.eulerAngles.z < 75)
        {
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 80)), .5f * Time.deltaTime);
            yield return null;
        }

    }
}
