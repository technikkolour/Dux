using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public Player player;
    public GameValues gameValues;

    public GameObject errorMessage;
    public Text weaponsRequirements;

    public bool isHumanPlayer = false;
    public int knowledgePrice;

    public void Awake()
    {
        if (isHumanPlayer == true) weaponsRequirements.text = "(Requires 3 rocks,  " + knowledgePrice + " knowledge)";
    }

    // Crafts weapons to increase army power.
    public void Weapons(Player player)
    {
        if (player.knowledge >= knowledgePrice)
        {
            if (player.rock >= 3)
            {
                player.armyPower += 3;
                player.rock -= 3;
                knowledgePrice += 2;
            }
            else if (isHumanPlayer == true)
            {
                errorMessage.SetActive(true);
                Invoke("ErrorDone", 2);
            }
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }

        if (isHumanPlayer == true) weaponsRequirements.text = "(Requires 3 rocks,  " + knowledgePrice + " knowledge)";
    }
    
    public void HumanPlayer()
    {
        isHumanPlayer = true;
    }

    // Crafts settlements to place.
    public void Settlement(SettlementPlacement settlementPlacement)
    { 
        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();

        string playerType = "ComputerPlayer";

        if (isHumanPlayer == true)
        {
            playerType = "HumanPlayer";
        }

        GameObject playerObject = GameObject.Find(playerType);
        player = playerObject.GetComponent<Player>();

        if (player.rock >= 5 && player.wood >= 3 && player.hay >= 1)
        {
            player.rock -= 5;
            player.wood -= 3;
            player.hay -= 1;

            gameValues.humanSettlementsToPlace += 1;

            settlementPlacement.SettlementsAvailable();
        }
        else  if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }
    }

    public void ErrorDone()
    {
        errorMessage.SetActive(false);
    }

}
