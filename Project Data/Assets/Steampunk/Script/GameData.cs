using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// This following script is used to save the data during the game. Some data that is saved are:
/// game progress, start time, end time, team name, penalty time from using clue, and activated clue condition
/// </summary>
public class GameData : MonoBehaviour
{
    public int gamePhase = 0;
    public string startTime;
    public string endTime;
    public string teamName;
    public double clueTime;
    public string username;

    [Header("This part is for all clue buttons data")]
    public bool clue1Open = false;
    public bool clue2Open = false;
    public bool clue3Open = false;
    public bool clue4Open = false;
    public bool clue5Open = false;
    public bool clue4_1Open = false;
    public bool clue4_2Open = false;
    public bool clue4_3Open = false;
    public bool clue4_4Open = false;
    public bool clue4_5Open = false;
    public bool clue6Open = false;
    public bool clue7Open = false;

    [Header("This part is for room1 data")]
    public bool[] room1PuzzleOpen;

    [Header("This part is for room2 data")]
    public bool[] room2PuzzleOpen;

    [Header("This part is for room3 data")]
    public bool[] room3PuzzleOpen;

    [Header("This part is for room4 data")]
    public bool[] room4PuzzleOpen;

    [Header("This part is for room5 data")]
    public bool[] room5PuzzleOpen;

    [Header("This part is for room1 data")]
    public bool[] room4_1PuzzleOpen;

    [Header("This part is for room2 data")]
    public bool[] room4_2PuzzleOpen;

    [Header("This part is for room3 data")]
    public bool[] room4_3PuzzleOpen;

    [Header("This part is for room4 data")]
    public bool[] room4_4PuzzleOpen;

    [Header("This part is for room5 data")]
    public bool[] room4_5PuzzleOpen;

    [Header("This part is for room1 data")]
    public bool[] room6PuzzleOpen;

    [Header("This part is for room2 data")]
    public bool[] room7PuzzleOpen;

    public static GameData instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        // read the begining time of the game
        startTime = DateTime.Now.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //This funtion is used to add a penalty time to the game duration by 5 minutes every time user activate a clue panel
    public void AddTime()
    {
        clueTime += 5;
    }

    // This function is used to read a finish time, add a penalty time to the finish time and write it in the end of the game
    public void CalculateFinishTime()
    {
        DateTime df = DateTime.Now;
        DateTime de = df.AddMinutes(clueTime);
        Debug.Log(de);
        endTime = de.ToString();
        
    }
}
