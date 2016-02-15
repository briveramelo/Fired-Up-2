using UnityEngine;
using System.Collections;
public class Exit : DangerZone {

    [SerializeField] private Entrance entrance;
    protected override void TriggerZone(){
        entrance.StopTimer();
    }
}
