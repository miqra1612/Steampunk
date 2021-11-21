using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManagerRoom3 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;
    public GameObject exitLabirynth;


    [Header("button array guide: 0 left, 1 mid, 2 right")]
    [Header("not every room have the same button")]
    public LabyrinthRoom[] labyrinthRooms;

    [Header("Make sure room data amount always the same with labyrinth room amount")]
    [Header("RoomDatas.CurrentRoomName must always equal LabirynthRooms.RoomName in the element list")]
    public RoomData[] roomDatas;

   

    [Header("This area is for non puzzle clue panel")]
    public RectTransform gauge;
    public Image gaugeImage;

    [Header("Check this variable if this is the last puzzle room in the game")]
    public bool lastRoom = false;

    private bool clue1On = false;
    private bool clue2On = false;
    private bool clue3On = false;
    private bool clue4On = false;
    private bool clue5On = false;

    //new system here
    public float tweenDelay = 1f;
    public List<RectTransform> openWindow = new List<RectTransform>();

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the begining of the scene
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        for(int i = 0; i< labyrinthRooms.Length; i++) 
        { 
            var currentRoom = labyrinthRooms[i].roomPanel.gameObject;
            RoomTransition(i, currentRoom, roomDatas[i].previousRoomName, 
                roomDatas[i].leftRoomName, 
                roomDatas[i].midRoomName, 
                roomDatas[i].rightRoomName
                );

           // Debug.Log($"name: {labyrinthRooms[i].roomName}");
        }
    }

    void RoomTransition(int roomId, GameObject cr, RoomName previousRoom , RoomName left, RoomName mid, RoomName right)
    {
        labyrinthRooms[roomId].backButton.onClick.AddListener(() => BackToPreviousRoom(cr, previousRoom));
        var img = labyrinthRooms[roomId].gaugeImg;
        labyrinthRooms[roomId].gauge.onClick.AddListener(() => GaugeOpen(img));

        var buttons = labyrinthRooms[roomId].doorButton;
        var currentRoom = labyrinthRooms[roomId].roomPanel.gameObject;
        
        GameObject nextRoomLeft = null;
        GameObject nextRoomMid = null;
        GameObject nextRoomRight = null;

        for (int i = 0; i< labyrinthRooms.Length; i++)
        {
            if (labyrinthRooms[i].roomName == left) nextRoomLeft = labyrinthRooms[i].roomPanel.gameObject;
            else if (labyrinthRooms[i].roomName == mid) nextRoomMid = labyrinthRooms[i].roomPanel.gameObject;
            else if (labyrinthRooms[i].roomName == right) nextRoomRight = labyrinthRooms[i].roomPanel.gameObject;
            else if (left == RoomName.ExitLabyrinth) nextRoomLeft = exitLabirynth;
            else if (mid == RoomName.ExitLabyrinth) nextRoomMid = exitLabirynth;
            else if (right == RoomName.ExitLabyrinth) nextRoomRight = exitLabirynth;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == 0 && buttons[0] != null) buttons[0].onClick.AddListener(() => ChangeLabyrinthRoom(currentRoom, nextRoomLeft));
            if (i == 1 && buttons[1] != null) buttons[1].onClick.AddListener(() => ChangeLabyrinthRoom(currentRoom, nextRoomMid));
            if (i == 2 && buttons[2] != null) buttons[2].onClick.AddListener(() => ChangeLabyrinthRoom(currentRoom, nextRoomRight));
            
        }
    }

    void GaugeOpen(Sprite sp)
    {
        gaugeImage.sprite = sp;
        OpenPanel(gauge);
    }

    void BackToPreviousRoom(GameObject currentRoom,RoomName prevRoomName)
    {
        for(int i = 0; i < labyrinthRooms.Length; i++)
        {
            if(labyrinthRooms[i].roomName == prevRoomName)
            {
                labyrinthRooms[i].roomPanel.gameObject.SetActive(true);
                currentRoom.SetActive(false);
                break;
            }
        }
        Debug.Log($"from Room: {currentRoom.name}, current Room: {prevRoomName}");
    }

    void ChangeLabyrinthRoom(GameObject currentRoom, GameObject nextRoom)
    {
        if (currentRoom == null) { Debug.Log("aa"); return; }
        if (nextRoom == null){ Debug.Log("cc"); return; }
        Debug.Log($"from Room: {currentRoom.name}, current Room: {nextRoom.name}");
        
        if(nextRoom.name == "ExitPanel")
        {
            sceneController.OpenScene("Port");
        }
        else
        {
            nextRoom.SetActive(true);
            currentRoom.SetActive(false);
        }
        
    }

    void CheckButton()
    {
        bool isComplete = GameData.instance.room3PuzzleOpen[1];

        if (isComplete)
        {
            
        }
        
    }
    
    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }

    public void CalculateFinishTime()
    {
        if (lastRoom)
        {
            gameData.CalculateFinishTime();
        }
    }

    void OpenPanel(RectTransform rt)
    {
        rt.DOAnchorPos(Vector2.zero, tweenDelay);
        openWindow.Add(rt);
    }

    public void ClosePanel()
    {
        for (int i = 0; i < openWindow.Count; i++)
        {
            openWindow[i].DOAnchorPos(new Vector2(0, 2000), tweenDelay);
        }

        openWindow.Clear();
    }

}

[System.Serializable]
public class LabyrinthRoom
{
    /// <summary>
    /// array element guide: 0 button left, 1 button mid, 2 button right
    /// not every room have the same button
    /// </summary>
    public RoomName roomName;
    public RectTransform roomPanel;
    
    public Button backButton;

    public Sprite gaugeImg;
    public Button gauge;
    
    public Button[] doorButton;

}

[System.Serializable]
public class RoomData
{
    public RoomName currentRoomName;
    public RoomName previousRoomName;
    public RoomName leftRoomName;
    public RoomName midRoomName;
    public RoomName rightRoomName;
}

public enum RoomName
{
    None,
    Room1_1,
    Room1_2,
    Room1_3,
    Room2_1,
    Room2_2,
    Room3_1,
    Room3_2,
    Room3_3,
    Room4_1,
    Room5_1,
    Room5_2,
    Room6_1,
    Room6_2,
    Room7_1,
    Room8_1,
    Room8_2,
    Room9_1,
    Room9_2,
    Room9_3,
    Room10_1,
    Room11_1,
    Room11_2,
    Room12_1,
    ExitLabyrinth
}
