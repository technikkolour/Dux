using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject[] gameTiles;

    public GameValues gameValues;
    public int index;

    private void Awake()
    {
        GameObject gameBoard = GameObject.Find("GameBoard");
        gameValues = gameBoard.GetComponent<GameValues>();
    }

    private void Start()
    {
        int tileNumber = Random.Range(0, 3);

        CreateTile(tileNumber, gameTiles);
    }

    // Instantiates a game tile.
    public void CreateTile(int i, GameObject[] gameObjectSet)
    {
        GameObject createdTile = Instantiate(gameObjectSet[i], transform.position, Quaternion.identity);
        gameValues.gameBoardTilesTypes[index] = i;

        createdTile.tag = (index + 1).ToString();
    }

}
