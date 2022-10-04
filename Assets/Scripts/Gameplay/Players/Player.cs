using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int food;
    public int rock;
    public int wood;
    public int hay;
    public int gold;

    public int armyPower;
    public int knowledge;

    public bool mineUnlocked;

    // Used when displaying the resources during the game.
    public int ReturnResource(string resourceName)    
    {
        switch(resourceName)
        {
           case "Food": 
                return (food);
           case "Rock":
                return (rock);
            case "Wood":
                return (wood);
            case "Hay":
                return (hay);
            case "Gold":
                return (gold);
            case "Current Army Power":
                return (armyPower);
            case "Knowledge":
                return (knowledge);
            default:
                return (0);
        }

    }
}
