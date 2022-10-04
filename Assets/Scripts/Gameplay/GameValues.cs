using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    // Stores the type of each game tile, in order.
    public List<int> gameBoardTilesTypes = new List<int>();

    // Where each player has placed a settlement:
    // 0 for an unclaimed tile;
    // 1 for the human player;
    // 2 for the computer player.
    public List<int> PlayerSettlementMap = new List<int>();

    // The changes made to the amount of resources in storage depending on the tile
    // the settlement is placed on.
    // The elements are in the following order: food, hay, rock, wood, gold.
    public List<int> valueChanges;

    // The current round of the game.
    public int roundNumber;

    // The calamity meter measures how many negative and positive events have occured.
    // The higher the value is, the more negative events have happened.
    public int calamityMeter;

    // The number of settlements the human player has available to place.
    public int humanSettlementsToPlace;

    // The number of settlements the computer player has available to place.
    public int computerSettlementsToPlace;

    // The type of computer player.
    public string computerPlayerType;
}
