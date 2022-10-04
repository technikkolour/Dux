using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingAndCombat : MonoBehaviour
{
    public bool isHumanPlayer = false;

    public GameObject errorMessage;
    public GameObject battleResult;

    public Text battleResultsMessage;

    public void IsHumanPlayer()
    {
        isHumanPlayer = true;
    }

    // Increases the player's army power.
    public void TrainArmy(Player player)
    {
        if (player.food >= 2)
        {
            player.armyPower += 1;
            player.food -= 2;
        }
        else if (isHumanPlayer == true)
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);
        }

    }

    // Allows the players to attack one another and sets the possible outcomes for the battles.
    public int Combat(Player challenger, Player challenged, bool isHumanPlayer)
    {
        string message;

        if (challenger.armyPower - challenged.armyPower > 0)
        {
            if (challenger.armyPower - challenged.armyPower >= 10) message = "A resounding victory!";
            else message = "Victory!";

            challenger.armyPower = (challenger.armyPower * 90) / 100;
            challenged.armyPower = (challenged.armyPower * 80) / 100;

            if (isHumanPlayer == false)
            {
                message = "You were challenged to a battle and were defeated. You have lost your settlement.";
            }
            battleResult.SetActive(true);
            battleResultsMessage.text = message;

            return (1);
        }
        else if (challenger.armyPower - challenged.armyPower < 0)
        {
            if (challenger.armyPower - challenged.armyPower <= -10) message = "A terrible loss...";
            else message = "Loss...";

            challenger.armyPower = (challenger.armyPower * 80) / 100;
            challenged.armyPower = (challenged.armyPower * 90) / 100;

            if (isHumanPlayer == false)
            {
                message = "You were challenged to a battle and won.";
                
            }

            battleResult.SetActive(true);
            battleResultsMessage.text = message;
            
            return (0);
        }
        else
        {
            message = "Draw...";

            challenger.armyPower = (challenger.armyPower * 90) / 100;
            challenged.armyPower = (challenged.armyPower * 90) / 100;

            if (isHumanPlayer == false)
            {
                message = "You were challenged to a battle and you drew...";

            }
            battleResult.SetActive(true);
            battleResultsMessage.text = message;

        }

        return (2);
    }

    // Hides the error.
    public void ErrorDone()
    {
        errorMessage.SetActive(false);
    }
}
