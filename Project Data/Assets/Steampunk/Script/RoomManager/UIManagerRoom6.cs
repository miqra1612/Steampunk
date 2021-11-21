using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class UIManagerRoom6 : MonoBehaviour
{
    public GameData gameData;

    [Header("This area is for other setting")]
    public Sprite[] afterFogSprite;
    public Image backgroundSprite;

    [Header("This area is for button clue")]
    public Button cluePuzzleBtn1;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button clueBtn4;
    public Button clueBtn5;
    public Button clueBtn6;
    public Button clueBtn7;
    public Button clueBtn8;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;
    public RectTransform clue4;
    public RectTransform clue5;
    public RectTransform clue6;
    public RectTransform clue7;
    public RectTransform clue8;

    [Header("This area is for all correct panel")]
    public RectTransform answerPanel1;

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

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));

        clueBtn1.onClick.AddListener(() => CheckStone());
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => CheckCollumn());
        clueBtn4.onClick.AddListener(() => CheckWall());
        clueBtn5.onClick.AddListener(() => NonPuzzleClue(clue5));
        clueBtn6.onClick.AddListener(() => PushStone());
        clueBtn7.onClick.AddListener(() => PushCollumn());
        clueBtn8.onClick.AddListener(() => PushWall());

        CheckingMap();
    }

    void CheckingMap()
    {
        var complete1 = GameData.instance.room6PuzzleOpen[0];
        var complete2 = GameData.instance.room6PuzzleOpen[1];
        var complete3 = GameData.instance.room6PuzzleOpen[2];

        if (complete1)
        {
            GameData.instance.room6PuzzleOpen[0] = true;
            backgroundSprite.sprite = afterFogSprite[1];
            
            clueBtn6.interactable = true;
            clueBtn3.interactable = true;
        }

        if (complete2)
        {
            GameData.instance.room6PuzzleOpen[1] = true;
            backgroundSprite.sprite = afterFogSprite[2];
           
            clueBtn7.interactable = true;
            clueBtn4.interactable = true;
        }

        if (complete3) 
        {
            GameData.instance.room6PuzzleOpen[2] = true;
            backgroundSprite.sprite = afterFogSprite[0];
            
            clueBtn8.interactable = true;
            cluePuzzleBtn1.interactable = true;
        }

    }

    void CheckStone()
    {
        var complete = GameData.instance.room6PuzzleOpen[0];

        if (complete) NonPuzzleClue(clue6);
        else NonPuzzleClue(clue1);
    }

    void CheckCollumn()
    {
        var complete = GameData.instance.room6PuzzleOpen[1];
        
        if(complete) NonPuzzleClue(clue7);
        else NonPuzzleClue(clue3);
       
    }

    void CheckWall()
    {
        var complete = GameData.instance.room6PuzzleOpen[2];
        if(complete) NonPuzzleClue(clue8); 
        else NonPuzzleClue(clue4);
    }

    void PushStone()
    {
        GameData.instance.room6PuzzleOpen[0] = true;
        backgroundSprite.sprite = afterFogSprite[1];
        NonPuzzleClue(clue6);
        clueBtn6.interactable = true;
        clueBtn3.interactable = true;
    }

    void PushCollumn()
    {
        GameData.instance.room6PuzzleOpen[1] = true;
        backgroundSprite.sprite = afterFogSprite[2];
        NonPuzzleClue(clue7);
        clueBtn7.interactable = true;
        clueBtn4.interactable = true;
    }

    void PushWall()
    {
        GameData.instance.room6PuzzleOpen[2] = true;
        backgroundSprite.sprite = afterFogSprite[0];
        NonPuzzleClue(clue8);
        clueBtn8.interactable = true;
        cluePuzzleBtn1.interactable = true;
    }

    // This is a fungtion to check puzzle 1 answer, if correct a new clue will be open and player can advance to the new map
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
            SwitchCorrect1(true);
            AnswerCorrect(canAdvance);
            GameData.instance.room6PuzzleOpen[3] = true;
        }
        else
        {
            StartCoroutine(AnswerFalse());
        }

    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room6PuzzleOpen[3];

        if (isComplete)
        {
            SwitchCorrect1(true);
        }
        else
        {
            ClosePanel();
            OpenPanel(panel);
        }
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
        else
        {
            CalculateFinishTime();
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

    //This coroutine is used to add a message for the player when player put a wrong answer in puzzle 1 input
    IEnumerator AnswerFalse()
    {
        string prev = notificationDisplay.text;

        notificationDisplay.text = "Nothing happen";
        notificationDisplay.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay.text = prev;
        notificationDisplay.color = Color.black;
    }

    // This function is used to activate a clue when puzzle 1 answer is correct
    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            ClosePanel();
            OpenPanel(answerPanel1);
            clue1On = true;
        }
        else
        {
            ClosePanel();
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
