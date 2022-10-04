using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Used to start a new game from the main menu.
    public void NewGame()
    {
        SceneManager.LoadScene("Game Window");
    }
}
