using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceRound : MonoBehaviour
{
    public Text roundButtonText;

    public GameValues gameValues;
    public RandomEvents events;

    public Player humanPlayer;
    public Player computerPlayer;

    // Increases the round number by 1.
    // Awards the players with resources.
    // Calls the computer player behaviour method.
    public void Advance()
    {
        events.TriggerEvent();

        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();

        gameValues.roundNumber += 1;

        roundButtonText.text = "Round " + gameValues.roundNumber;

        GameObject humanPlayerObject = GameObject.Find("HumanPlayer");
        humanPlayer = humanPlayerObject.GetComponent<Player>();

        GameObject computerPlayerObject = GameObject.Find("ComputerPlayer");
        computerPlayer = computerPlayerObject.GetComponent<Player>();

        for (int i=0; i<16; i++)
        {
            if (gameValues.PlayerSettlementMap[i] == 1)
            {
                if (gameValues.gameBoardTilesTypes[i] == 0)
                {
                    humanPlayer.food += 3;
                    humanPlayer.hay += 1;
                }
                else if (gameValues.gameBoardTilesTypes[i] == 1)
                {
                    humanPlayer.wood += 2;
                }
                else if (gameValues.gameBoardTilesTypes[i] == 2)
                {
                    humanPlayer.rock += 2;
                    if (humanPlayer.mineUnlocked == true) humanPlayer.gold += 3;
                }
                
            }
            else if (gameValues.PlayerSettlementMap[i] == 2)
            {
                if (gameValues.gameBoardTilesTypes[i] == 0)
                {
                    computerPlayer.food += 3;
                    computerPlayer.hay += 1;
                }
                else if (gameValues.gameBoardTilesTypes[i] == 1)
                {
                    computerPlayer.wood += 2;
                }
                else if (gameValues.gameBoardTilesTypes[i] == 2)
                {
                    computerPlayer.rock += 2;
                    if (computerPlayer.mineUnlocked == true) computerPlayer.gold += 3;
                }
            }
        }

        ComputerPlayerBehaviour computerBehaviour = computerPlayerObject.GetComponent<ComputerPlayerBehaviour>();
        if (gameValues.computerPlayerType == "ExpansionFocusedStrategy")
        {
            computerBehaviour.ExpansionFocusedStrategy();
        } 
        else if (gameValues.computerPlayerType == "ArmyFocusedStrategy")
        {
            computerBehaviour.ArmyFocusedStrategy();
        }
        
    }
}
