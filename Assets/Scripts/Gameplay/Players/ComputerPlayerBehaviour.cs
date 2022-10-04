using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerPlayerBehaviour : MonoBehaviour
{
    public GameValues gameValues;
    public GenerateBoard boardGenerator;
    public Player computerPlayer;
    public Player humanPlayer;

    public GameObject[] playerGameTiles;

    // Necessary player functions.
    public Trading trading;
    public Crafting crafting;
    public TrainingAndCombat trainingAndCombat;

    int playerField = 0;
    int playerForest = 0;
    int playerMountain = 0;


    public void Awake()
    {
        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();

        GameObject computer = GameObject.Find("ComputerPlayer");
        computerPlayer = computer.GetComponent<Player>();

        GameObject humanPlayerObject = GameObject.Find("HumanPlayer");
        humanPlayer = humanPlayerObject.GetComponent<Player>();

        trading = computer.GetComponent<Trading>();
        crafting = computer.GetComponent<Crafting>();
        trainingAndCombat = computer.GetComponent<TrainingAndCombat>();
    }

    // Returns the index of the tile the computer player would like to place a settlement on.
    public int TileFinder(int tileType)
    {
        for (int i = 0; i < 16; i++)
        {
            if (gameValues.gameBoardTilesTypes[i] == tileType && gameValues.PlayerSettlementMap[i] == 0) return (i + 1);
        }

        return (100);
    }

    // Returns the index of the first human settlement placed on a tile of a ceratin type.
    public int HumanSettlementFinder(int tileType)
    {
        for (int i = 0; i < 16; i++)
            if (gameValues.gameBoardTilesTypes[i] == tileType && gameValues.PlayerSettlementMap[i] == 1) return (i + 1);

        return (100);
    }
    
    // Checks whether the computer player is able to place a settlement.
    public int SettlementAvailable()
    {
        if (computerPlayer.rock >= 5 && computerPlayer.wood >= 3 && computerPlayer.hay >= 1) return (1);
        else if (computerPlayer.rock >= 5 && computerPlayer.hay >= 7)
        {
            while (computerPlayer.wood <3)
            {
                trading.HayWood(computerPlayer);
            }
            
            return (1);
        }
        else if (computerPlayer.rock >= 6 && computerPlayer.wood >= 3)
        {
            while (computerPlayer.hay < 1)
            {
                trading.RockHay(computerPlayer);
            }

            return (1);
        }
        else if (computerPlayer.wood >= 9 && computerPlayer.hay >= 1)
        {
            while (computerPlayer.rock < 5)
            {
                trading.WoodRock(computerPlayer);
            }

            return (1);
        }
        else if (computerPlayer.wood >= 9)
        {
            while (computerPlayer.rock <= 5)
            {
                trading.WoodRock(computerPlayer);
            }
            trading.RockHay(computerPlayer);

            SettlementAvailable();
        } 
        else if (computerPlayer.rock >= 9)
        {
            while (computerPlayer.hay < 8)
            {
                trading.RockHay(computerPlayer);
            }
            while (computerPlayer.wood < 3)
            {
                trading.HayWood(computerPlayer);
            }

            SettlementAvailable();
        }
        else if (computerPlayer.hay >= 19)
        {
            while (computerPlayer.wood < 9)
            {
                trading.HayWood(computerPlayer);
            }
            while (computerPlayer.rock < 5)
            {
                trading.WoodRock(computerPlayer);
            }

            SettlementAvailable();
        }

        return (0);
    }

    public void PlaceSettlement(int tileNumber)
    {
        string tileName = "Tile" + tileNumber;

        GameObject tile = GameObject.Find(tileName);
        boardGenerator = tile.GetComponent<GenerateBoard>();

        GameObject targetTile = GameObject.FindGameObjectWithTag(tileNumber.ToString());

        int i = tileNumber - 1;

        if (gameValues.PlayerSettlementMap[i] == 0 || (gameValues.PlayerSettlementMap[i] == 1 && trainingAndCombat.Combat(computerPlayer, humanPlayer, false) == 1))
        {
            

            gameValues.PlayerSettlementMap[i] = 2;

            int tileType = gameValues.gameBoardTilesTypes[i];

            Destroy(targetTile);
            boardGenerator.CreateTile(tileType, playerGameTiles);

            gameValues.computerSettlementsToPlace -= 1;

            if (tileType == 2) computerPlayer.mineUnlocked = true;
        }
    }

    public void CraftSettlement(int i)
    {
        computerPlayer.rock -= 5;
        computerPlayer.hay -= 1;
        computerPlayer.wood -= 3;

        gameValues.computerSettlementsToPlace += 1;

        PlaceSettlement(i);
    }

    public void ArmyFocusedStrategy()
    {
        if (gameValues.roundNumber == 2)
        {
            if (gameValues.computerSettlementsToPlace > 0)
            {
                PlaceSettlement(TileFinder(0));
                PlaceSettlement(TileFinder(2));
            }
            
        } 
        else
        {
            while(computerPlayer.food >= 2)
            {
                trainingAndCombat.TrainArmy(computerPlayer);
            }

            while(computerPlayer.gold >= 5)
            {
                trading.Books(computerPlayer);
            }

            while(computerPlayer.rock >= 3 && computerPlayer.knowledge >= crafting.knowledgePrice)
            {
                crafting.Weapons(computerPlayer);
            }

            if (SettlementAvailable() == 1)
            {
                if (HumanSettlementFinder(2) != 100) CraftSettlement(HumanSettlementFinder(2));
                else if (HumanSettlementFinder(0) != 100) CraftSettlement(HumanSettlementFinder(0));
                else if (HumanSettlementFinder(1) != 100) CraftSettlement(HumanSettlementFinder(1));
            }

            Invoke("GameFinished", 5);
        }
    }

    public void ExpansionFocusedStrategy()
    {
        if (gameValues.roundNumber == 2)
        {
            PlaceSettlement(TileFinder(0));
            playerField += 1;

            PlaceSettlement(TileFinder(2));
            playerMountain += 1;
        }
        else
        {
            while (computerPlayer.food >= 2)
            {
                trainingAndCombat.TrainArmy(computerPlayer);
            }

            if (SettlementAvailable() == 1)
            {
                if (playerForest <= playerField && playerForest <= playerMountain && TileFinder(1) != 100)
                {
                    playerForest += 1;
                    CraftSettlement(TileFinder(1));
                }
                else if (playerField < playerForest && playerField <= playerMountain && TileFinder(0) != 100)
                {
                    playerField += 1;
                    CraftSettlement(TileFinder(0));
                }
                else if (playerMountain < playerField && playerMountain < playerForest && TileFinder(2) != 100)
                {
                    playerMountain += 1;
                    CraftSettlement(TileFinder(2));
                }
                else if (HumanSettlementFinder(1) != 100)
                {
                    CraftSettlement(HumanSettlementFinder(1));
                }
                else if (HumanSettlementFinder(0) != 100)
                {
                    CraftSettlement(HumanSettlementFinder(0));
                }
                else if (HumanSettlementFinder(2) != 100)
                {
                    CraftSettlement(HumanSettlementFinder(2));
                }
            }

            Invoke("GameFinished", 5);
        }
    }

    // Verfies whether the game is finished for the user.
    // Checks whether it has any settlements left on the board.
    // If not, it verifies whether it has any settlements left to place, if the game is still in the beginning stage and if the human army power is higher than the computer player's.
    public void GameFinished()
    {
        bool gameIsFinished = true;

        for (int i = 0; i < 16; i++)
            if (gameValues.PlayerSettlementMap[i] == 1 || ( gameValues.humanSettlementsToPlace != 0 && ( humanPlayer.armyPower > computerPlayer.armyPower || gameValues.roundNumber < 6 ))) gameIsFinished = false;

        if (gameIsFinished == true) SceneManager.LoadScene("Loss");
    }

    // Used when selecting the army focused computer player at the start of the game. 
    public void SetArmyComputerPlayer()
    {
        gameValues.computerPlayerType = "ArmyFocusedStrategy";
    }

    // Used when selecting the expansion focused computer player at the start of the game.
    public void SetExpansionComputerPlayer()
    {
        gameValues.computerPlayerType = "ExpansionFocusedStrategy";
    }
}
