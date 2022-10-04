using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    public bool isHumanPlayer = false;
    public GameObject errorMessage;

    // Trades food for wood.
    public void FoodWood(Player player)
    {
        if (player.food >= 3)
        {
            player.food -= 3;
            player.wood += 2;
        }
        else if (isHumanPlayer == true) 
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2); 
        }
    }

    // Trades wood for rock.
    public void WoodRock(Player player)
    {
        if (player.wood >= 2)
        {
            player.wood -= 2;
            player.rock += 2;
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }
    }

    // Trades rock for hay.
    public void RockHay(Player player)
    {
        if (player.rock >= 1)
        {
            player.rock -= 1;
            player.hay += 2;
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }
    }

    // Trades hay for wood.
    public void HayWood(Player player)
    {
        if (player.hay >= 2)
        {
            player.hay -= 2;
            player.wood += 1;
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }
    }

    // Purchases books for gold.
    public void Books(Player player)
    {
        if (player.gold >= 5)
        {
            player.gold -= 5;
            player.knowledge += 1;
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }
    }

    // Hides the error message.
    public void ErrorDone()
    {
        errorMessage.SetActive(false);
    }
}
