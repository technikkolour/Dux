using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public Text resourceTypeText;
    public Player humanPlayer;
    
    // Displays the amount of one resource that the user has in storage.
    public void Update()
    {
        GameObject humanPlayerObject = GameObject.Find("HumanPlayer");
        humanPlayer = humanPlayerObject.GetComponent<Player>();

        string resourceType;
        resourceType = gameObject.name;

        resourceTypeText.text = resourceType + ": " + humanPlayer.ReturnResource(resourceType);
    }
}
