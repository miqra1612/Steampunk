using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManager : MonoBehaviour
{
    public SceneController sceneController;
    public GameData gameData;
    
    [Header("This area is for button clue puzzle")]
    public Button cluePuzzleBtn1;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button clueBtn4;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;
    public RectTransform clue4;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput;
    public Text notificationDisplay;

    [Header("This area is for the answer")]
    public RectTransform doorBlock;


    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzle1Answers;
    

    [Header("Check this variable if this is the last puzzle room in the game")]
    public bool lastRoom = false;

    //this is to mark the pauzzle that already solve so whenever player reopen the puzzle the answer menu is open directly
    private bool clue1On = false;
    private bool clue2On = false;
    private bool clue3On = false;
    private bool clue4On = false;
    private bool clue5On = false;

    //new system here
    public float tweenDelay = 1f;
    public List<RectTransform> openWindow = new List<RectTransform>();

    private void Awake()
    {
        // Find game data object in the begining of the scene
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        
        clueBtn1.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => NonPuzzleClue(clue3));
        clueBtn4.onClick.AddListener(() => NonPuzzleClue(clue4));

       
    }
    
    // This is a fungtion to check puzzle 1 answer, if correct a new clue will be open and player can advance to the new map
    public void CheckingAnswer1(bool canAdvance)
    {
       
        var a = 0;

        for (int i = 0; i < puzzle1Answers.Length; i++)
        {
            if (puzzleInput.text.Equals(puzzle1Answers[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            
            AnswerCorrect(canAdvance);
            GameData.instance.room1PuzzleOpen[0] = true;
            SwitchCorrect1(true);

        }
        else
        {
            StartCoroutine(AnswerFalse());
        }
       
    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room1PuzzleOpen[0];

        var isBlock = GameData.instance.room4_1PuzzleOpen[9];

        if(isBlock) OpenPanel(doorBlock);
        else
        {
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
        
    }

    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }
    
    //This function is used to increase the game progression data and close all clue window that is open.
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
            clue4On = false;
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

        notificationDisplay.text = "Nothing happened";
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

            clue1On = true;
            sceneController.OpenScene("Pub Inside");
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
        for(int i = 0; i < openWindow.Count; i++)
        {
            openWindow[i].DOAnchorPos(new Vector2(0, 2000), tweenDelay);
        }
       
        openWindow.Clear();
    }
}
