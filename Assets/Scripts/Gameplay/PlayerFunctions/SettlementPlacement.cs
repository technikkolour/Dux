using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettlementPlacement : MonoBehaviour
{
    public GameValues gameValues;
    public GenerateBoard boardGenerator;
    public GameObject settlementPlacementPrompt;
    public TrainingAndCombat combat;
    public Player humanPlayer;
    public Player computerPlayer;

    public InputField humanInput;
    public int tileNumber;
    public GameObject errorMessage;

    public GameObject[] playerGameTiles;

    public void Awake()
    {
        settlementPlacementPrompt = GameObject.Find("SettlementPlacement");
    }

    // Takes the user input and converts it into an int, representing the index of the desired tile.
    public int HumanInputConvertor() {
        humanInput = gameObject.GetComponent<InputField>();
        var input = humanInput.text;

        int tileCoordinates = 0;

        if (char.IsDigit(input[0]) && char.IsDigit(input[1]))
        {
            int firstCoordinate = int.Parse(input[0].ToString());
            int secondCoordinate = int.Parse(input[1].ToString());

            if ((firstCoordinate >= 1 && firstCoordinate <= 4) && (secondCoordinate >= 1 && secondCoordinate <= 4))
            {
                tileCoordinates = 4 * (firstCoordinate - 1) + secondCoordinate;

                if (gameValues.PlayerSettlementMap[tileCoordinates - 1] == 1)
                {
                    errorMessage.SetActive(true);
                    Invoke("ErrorDone", 2);

                    return (0);
                }
            }
            else
            {
                errorMessage.SetActive(true);
                Invoke("ErrorDone", 2);

                return (0);
            }
        }
        else
        {
            errorMessage.SetActive(true);
            Invoke("ErrorDone", 2);

            return (0);
        }
            
        return (tileCoordinates);
    }

    // Places a settlement onto the desired tile.
    public void Place()
    {
        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();

        GameObject humanPlayerObject = GameObject.Find("HumanPlayer");
        humanPlayer = humanPlayerObject.GetComponent<Player>();

        GameObject computerPlayerObject = GameObject.Find("ComputerPlayer");
        computerPlayer = computerPlayerObject.GetComponent<Player>();

        tileNumber = HumanInputConvertor();

        if (tileNumber != 0)
        {
            string tileName = "Tile" + tileNumber;

            GameObject tile = GameObject.Find(tileName);
            boardGenerator = tile.GetComponent<GenerateBoard>();

            GameObject targetTile = GameObject.FindGameObjectWithTag(tileNumber.ToString());

            int i = tileNumber - 1;

            if (gameValues.PlayerSettlementMap[i] == 0 || ( gameValues.PlayerSettlementMap[i] == 2 && combat.Combat(humanPlayer, computerPlayer, true) == 1))
            {
                gameValues.PlayerSettlementMap[i] = 1;

                int tileType = gameValues.gameBoardTilesTypes[i];

                Destroy(targetTile);
                boardGenerator.CreateTile(tileType, playerGameTiles);

                gameValues.humanSettlementsToPlace -= 1;

                Invoke("GameFinished", 3);

                NoMoreSettlements(gameValues.humanSettlementsToPlace);

                if (tileType == 2) humanPlayer.mineUnlocked = true;
            }

        }

    }

    // Verifies whether the human player has won.
    public void GameFinished()
    {
        int n = gameValues.PlayerSettlementMap[0];

        bool finished = true;

        for (int i = 1; i < 16; i++)
        {
            if (n != gameValues.PlayerSettlementMap[i] && n != 0) finished = false;
        }

        if (finished == true)
            if (n == 1) SceneManager.LoadScene("Win");
        
    }
    
    // Deactivates the settlement placement input prompt.
    public void NoMoreSettlements(int settlementsAvailable)
    {
        if (settlementsAvailable <= 0)
        {
            settlementPlacementPrompt.SetActive(false);
        }
    }

    public void SettlementsAvailable()
    {
        settlementPlacementPrompt.SetActive(true);
    }

    public void ErrorDone()
    {
        errorMessage.SetActive(false);
    }
}
