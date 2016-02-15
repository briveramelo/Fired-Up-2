using UnityEngine;
using System.Collections;

public static class DamageTracker {

    private static int damageTaken;
    public static int DamageTaken{
        get { return damageTaken; }
        set { damageTaken = value; }
    }

    private static int livesLost;
    public static int LivesLost{
        get { return livesLost; }
        set { livesLost = value; }
    }

}
