using UnityEngine;
using System.Collections;
using FU;

public class PlayerDeath : MonoBehaviour {
    // Use this for initialization
    static bool playerDead = false;
    public static PlayerDeath playerDeath;


   void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("FireFighter"))
        {
            Player.player.KillPlayer(); 

        }
    }
    

}
