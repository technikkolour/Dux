using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomEvents : MonoBehaviour
{
    public GameValues gameValues;
    public Player humanPlayer;

    public GameObject eventWindow;
    public Text eventMessage; 

    public List<Action> NegativeEvents;
    public List<Action> PositiveEvents;
    public List<Action> AllEvents;

    public Player GetHumanPlayer()
    {
        GameObject humanPlayerObject = GameObject.Find("HumanPlayer");
        humanPlayer = humanPlayerObject.GetComponent<Player>();

        return (humanPlayer);
    }

    public GameValues GetGameValues()
    {
        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();

        return (gameValues);
    }

    public int TileCount(int tileType)
    {
        GetGameValues();

        int counter = 0;

        for (int i = 0; i < 16; i++)
            if (gameValues.PlayerSettlementMap[i] == 1 && gameValues.gameBoardTilesTypes[i] == tileType) counter += 1;

        return (counter);
    }

    public void Drought()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "The Gods of Nature were not kind the past year and a terrible drought befell the land. You collect fewer resources at the start of this round.";
        eventMessage.text = message;

        if (gameValues.calamityMeter <= 8) gameValues.calamityMeter += 2;
        else gameValues.calamityMeter = 10;

        humanPlayer.food -= TileCount(0) * 2;
        humanPlayer.hay -= TileCount(0);
        humanPlayer.wood -= TileCount(1);
    }

    public void EnemyAttack()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "Your settlement was attacked by an enemy army and many of your men were killed. You lost a portion of your food and your army power decreased.";
        eventMessage.text = message;

        if (gameValues.calamityMeter <= 8) gameValues.calamityMeter += 2;
        else gameValues.calamityMeter = 10;

        if (humanPlayer.armyPower > 10 && humanPlayer.food > 5)
        {
            humanPlayer.armyPower = (humanPlayer.armyPower * 65) / 100;
            humanPlayer.food = (humanPlayer.food * 65) / 100; 
        }
        else if (humanPlayer.armyPower > 0 && humanPlayer.food > 0)
        {
            humanPlayer.armyPower = (humanPlayer.armyPower * 40) / 100;
            humanPlayer.food = (humanPlayer.food * 40) / 100;
        }
    }

    public void Earthquake()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "Your settlement was struck and destroyed by an earthquake. In order to rebuild it you spend a significant amount of your resources.";
        eventMessage.text = message;

        if (gameValues.calamityMeter <= 7) gameValues.calamityMeter += 2;
        else gameValues.calamityMeter = 10;

        if (humanPlayer.rock > 0) humanPlayer.rock /= 2;
        if (humanPlayer.hay > 0) humanPlayer.hay /= 2;
        if (humanPlayer.wood > 0) humanPlayer.wood /= 2;
    }

    public void Avalanche()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "While in the mountains your men have been struck by an avalanche. You collect significantly less rock this round.";
        eventMessage.text = message;

        if (gameValues.calamityMeter <= 8) gameValues.calamityMeter += 2;
        else gameValues.calamityMeter = 10;

        humanPlayer.rock -= TileCount(2);
    }

    public void AllyVisit()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "A foreign envoy arrived at your settlement, bringing gifts. You gain gold and knowledge.";
        eventMessage.text = message;

        if (gameValues.calamityMeter >= 2) gameValues.calamityMeter -= 2;
        else gameValues.calamityMeter = 0;

        humanPlayer.gold += 10;
        humanPlayer.knowledge += 5;
    }

    public void FertileSoil()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "You were blessed by the Gods with fertile soil the past year. You collect more food, hay and wood this round.";
        eventMessage.text = message;

        if (gameValues.calamityMeter >= 2) gameValues.calamityMeter -= 2;
        else gameValues.calamityMeter = 0;

        humanPlayer.food += TileCount(0) * 2;
        humanPlayer.hay += TileCount(0);
        humanPlayer.wood += TileCount(1);
    }

    public void RainyYear()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "Dark clouds loomed over the land the past year. You collect more wood from your forests this round.";
        eventMessage.text = message;

        if (gameValues.calamityMeter >= 2) gameValues.calamityMeter -= 2;
        else gameValues.calamityMeter = 0;

        humanPlayer.wood += TileCount(1);
    }

    public void GoodFortune()
    {
        GetHumanPlayer();
        GetGameValues();

        string message = "While exploring a mine your men have found a fountain of riches. You collect significantly more gold this round.";
        eventMessage.text = message;

        if (gameValues.calamityMeter >= 2) gameValues.calamityMeter -= 2;
        else gameValues.calamityMeter = 0;

        if (TileCount(2) > 2) humanPlayer.gold += TileCount(2) * 2;
        else humanPlayer.gold += 4;
    }

    // Called at the star of each round.
    // Randomly chooses an event to trigger.
    // There is a 40% chance that an event is not triggered.
    public void TriggerEvent()
    {
        GetGameValues();

        NegativeEvents = new List<Action> { Drought, EnemyAttack, Earthquake, Avalanche };
        PositiveEvents = new List<Action> { AllyVisit, FertileSoil, RainyYear, GoodFortune };

        AllEvents = new List<Action> { Drought, EnemyAttack, Earthquake, Avalanche, AllyVisit, FertileSoil, RainyYear, GoodFortune };

        int chance = UnityEngine.Random.Range(0, 100);

        if (chance <= 60)
        {
            if (gameValues.calamityMeter == 10)
            {
                int method = UnityEngine.Random.Range(0, 4);
                PositiveEvents[method]();
            }
            else if (gameValues.calamityMeter == 0)
            {
                int method = UnityEngine.Random.Range(0, 4);
                NegativeEvents[method]();
            }
            else
            {
                int method = UnityEngine.Random.Range(0, 8);
                AllEvents[method]();
            }
        }
        else eventMessage.text = "A year has passed... You collect the expected amount of resources."; 

        eventWindow.SetActive(true);
    }
}
