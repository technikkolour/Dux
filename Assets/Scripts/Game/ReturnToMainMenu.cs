using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // Used to return the user to the main menu.
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
