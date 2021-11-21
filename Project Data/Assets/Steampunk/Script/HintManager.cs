using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// This script is used to control hint window inside the game room
/// </summary>

public enum Room
{
    Room1,
    Room2,
    Room3,
    Room4,
    Room5,
    Room4_1,
    Room4_2,
    Room4_3,
    Room4_4,
    Room4_5,
    Room6,
    Room7,
}

public class HintManager : MonoBehaviour
{
    [Header("This area is for clue panel")]
    public RectTransform cluePanel;
    public RectTransform clueWarning;
    public bool clueOpen = false;
    public Room room;
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the scene to read the data
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();
        CheckingRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is used to check the activated status of the game hint inside each room
    void CheckingRoom()
    {
        if(room == Room.Room1)
        {
            clueOpen = gameData.clue1Open;
        }
        else if (room == Room.Room2)
        {
            clueOpen = gameData.clue2Open;
        }
        else if (room == Room.Room3)
        {
            clueOpen = gameData.clue3Open;
        }
        else if (room == Room.Room4)
        {
            clueOpen = gameData.clue4Open;
        }
        else if (room == Room.Room5)
        {
            clueOpen = gameData.clue5Open;
        }
        else if (room == Room.Room6)
        {
            clueOpen = gameData.clue6Open;
        }
        else if (room == Room.Room7)
        {
            clueOpen = gameData.clue7Open;
        }
        else if (room == Room.Room4_1)
        {
            clueOpen = gameData.clue4_1Open;
        }
        else if (room == Room.Room4_2)
        {
            clueOpen = gameData.clue4_2Open;
        }
        else if (room == Room.Room4_3)
        {
            clueOpen = gameData.clue4_4Open;
        }
        else if (room == Room.Room4_4)
        {
            clueOpen = gameData.clue4_4Open;
        }
        else if (room == Room.Room4_5)
        {
            clueOpen = gameData.clue4_5Open;
        }
    }

    //This function is used to save activated hint status to the game data
    void SaveClueStatus()
    {
        if (room == Room.Room1)
        {
            gameData.clue1Open = clueOpen;
        }
        else if (room == Room.Room2)
        {
            gameData.clue2Open = clueOpen;
        }
        else if (room == Room.Room3)
        {
            gameData.clue3Open = clueOpen;
        }
        else if (room == Room.Room4)
        {
           gameData.clue4Open = clueOpen;
        }
        else if (room == Room.Room5)
        {
           gameData.clue5Open = clueOpen;
        }
        else if (room == Room.Room6)
        {
            gameData.clue6Open = clueOpen;
        }
        else if (room == Room.Room7)
        {
            gameData.clue7Open = clueOpen;
        }
        else if (room == Room.Room4_1)
        {
            gameData.clue4_1Open = clueOpen;
        }
        else if (room == Room.Room4_2)
        {
            gameData.clue4_2Open = clueOpen;
        }
        else if (room == Room.Room4_3)
        {
            gameData.clue4_3Open = clueOpen;
        }
        else if (room == Room.Room4_4)
        {
            gameData.clue4_4Open = clueOpen;
        }
        else if (room == Room.Room4_5)
        {
            gameData.clue4_5Open = clueOpen;
        }
    }
    public float tweenDelay = 1f;

    //This function will open a warning panel for the player before player activated the hint window
    public void OpenWarningPanel()
    {
        if (clueOpen)
        {
            cluePanel.DOAnchorPos(Vector2.zero, tweenDelay);
        }
        else
        {
            clueWarning.DOAnchorPos(Vector2.zero, tweenDelay);
        }
    }

    //This function will open up the game hint window and add a penalty time for the player
    public void OpenClue()
    {
        gameData.AddTime();
        clueWarning.DOAnchorPos(new Vector2(0, 2000), tweenDelay);
        cluePanel.DOAnchorPos(Vector2.zero, tweenDelay);
        clueOpen = true;
        SaveClueStatus();
    }

    //This function is used to close the hint window
    public void CloseHint()
    {
        cluePanel.DOAnchorPos(new Vector2(0, 2000), tweenDelay);
    }

    //This function is used to close the warning window
    public void CloseWarning()
    {
        clueWarning.DOAnchorPos(new Vector2(0, 2000), tweenDelay);
    }

}
