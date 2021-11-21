using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// This script is used to create a player certificate consiste of team name and play time duration in the end of the game
/// </summary>

public class CertificateManager : MonoBehaviour
{
    public Text teamName;
    public Text timer;
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the scene to read the data
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();
        ShowCertificate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DateTime a = DateTime.Parse("4/22/2021 12:48:10 AM");

            Debug.Log(a);
        }
    }

    // This function is used to calculate the play duration of the game from the begining to the end of the game
    void ShowCertificate()
    {
        teamName.text = ": " + gameData.teamName;
        DateTime ts = DateTime.Parse(gameData.startTime);
        DateTime te = DateTime.Parse(gameData.endTime);
        timer.text = ": " +  te.Subtract(ts) + " Minute";
    }
}
