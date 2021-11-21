using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManagerRoom4 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    [Header("This area is for button clue puzzle")]
    public Button cluePuzzleBtn1;
    public Button cluePuzzleBtn2;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;
    public RectTransform puzzleClue2;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button goBtn;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;

    [Header("This area is for answer puzzle panel")]
    public RectTransform answerPanel1;
    public RectTransform answerPanel2;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput1;
    public Text notificationDisplay;
    public InputField puzzleInput2;
    public Text notificationDisplay2;

    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzleAnswer1;
    public string[] puzzleAnswer2;

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

        clueBtn1.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => NonPuzzleClue(clue3));

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        cluePuzzleBtn2.onClick.AddListener(() => ClueWithPuzzle2(puzzleClue2));

        goBtn.onClick.AddListener(() => AirshipGo());

        var complete = GameData.instance.room4PuzzleOpen[1];
            
        if(complete) goBtn.interactable = true;
    }

    void AirshipGo()
    {
        sceneController.OpenScene("Outro Room");
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
            GameData.instance.room4PuzzleOpen[0] = true;

            SwitchCorrect1(true);
            AnswerCorrect(canAdvance);
        }
        else
        {
            StartCoroutine(AnswerFalse1());
        }
    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room4PuzzleOpen[0];

        if (isComplete)
        {
            SwitchCorrect1(true);
        }
        else
        {
            OpenPanel(panel);
        }
    }

    public void ClueWithPuzzle2(RectTransform panel)
    {
        bool isComplete = GameData.instance.room4PuzzleOpen[1];

        if (isComplete )
        {
            SwitchCorrect2(true);
        }
        else
        {
            OpenPanel(panel);
        }
    }

    public void CheckingAnswer2(bool canAdvance)
    {
        var a = 0;

        for (int i = 0; i < puzzleAnswer2.Length; i++)
        {
            if (puzzleInput2.text.Equals(puzzleAnswer2[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            GameData.instance.room4PuzzleOpen[1] = true;
            AnswerCorrect(canAdvance);
            SwitchCorrect2(true);
        }
        else
        {
            StartCoroutine(AnswerFalse2());
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

    IEnumerator AnswerFalse2()
    {
        string prev = notificationDisplay2.text;

        notificationDisplay2.text = "Nothing happened";
        notificationDisplay2.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay2.text = prev;
        notificationDisplay2.color = Color.black;
    }

    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            ClosePanel();
            clue1On = true;
            OpenPanel(answerPanel1);
        }
        else
        {
            ClosePanel();
        }
    }

    public void SwitchCorrect2(bool isActive)
    {
        if (isActive)
        {
            ClosePanel();
            clue2On = true;
            goBtn.interactable = true;
            OpenPanel(answerPanel2);
        }
        else
        {
            ClosePanel();
        }
    }

    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }

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
