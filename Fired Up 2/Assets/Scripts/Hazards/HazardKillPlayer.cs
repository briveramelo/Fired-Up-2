using UnityEngine;
using System.Collections;

public class HazardKillPlayer : MonoBehaviour {



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("FireFighter"))
        {
            Debug.Log("Player Died");
            Player.player.KillPlayer();

        }
    }
}
