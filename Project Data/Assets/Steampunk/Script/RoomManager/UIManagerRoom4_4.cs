using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManagerRoom4_4 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    [Header("This area is for button clue puzzle")]
    public Button cluePuzzleBtn1;

    [Header("This area is for button clue")]
    public Button clueBtn1;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleCluePanel1;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput1;
    public Text notificationDisplay;

    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzleAnswer1;

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

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleCluePanel1));
        clueBtn1.onClick.AddListener(() => ToAirShip());
    }

    void ToAirShip()
    {
        sceneController.OpenScene("Airship");
    }

    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room4_4PuzzleOpen[0];

        if (isComplete)
        {
            sceneController.OpenScene("Labyrinth");
        }
        else
        {
            OpenPanel(panel);
        }
    }

    public void CheckingAnswer1(bool canAdvance)
    {
        var a = 0;

        for (int i = 0; i < puzzleAnswer1.Length; i++)
        {
            if (puzzleInput1.text.Equals(puzzleAnswer1[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            GameData.instance.room4_4PuzzleOpen[0] = true;

            SwitchCorrect1(true);
            AnswerCorrect(canAdvance);
        }
        else
        {
            StartCoroutine(AnswerFalse1());
        }
    }

    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            clue1On = true;
            sceneController.OpenScene("Labyrinth");
        }
        else
        {
            ClosePanel();
        }
    }

    IEnumerator AnswerFalse1()
    {
        string prev = notificationDisplay.text;

        notificationDisplay.text = "Nothing happened";
        notificationDisplay.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay.text = prev;
        notificationDisplay.color = Color.black;
    }

    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }

    //This function is used to increase the game progression data and close all clue window the is open.
    //It is also serve to check if the last puzzle already solve or not, if yes then the game finish time will be calculated
    public void AnswerCorrect(bool continueMap)
    {
        if (continueMap && !lastRoom)
        {
            gameData.gamePhase++;
        }

        if (clue1On)
        {
            clue1On = false;
        }
        else if (clue2On)
        {
            clue2On = false;
        }
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
