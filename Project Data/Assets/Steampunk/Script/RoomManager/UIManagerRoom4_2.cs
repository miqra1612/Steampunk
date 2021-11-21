using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManagerRoom4_2 : MonoBehaviour
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

    [Header("This area is for other setting")]
    public Button inventorBtn;
    public GameObject inventorPanel1;
    public GameObject inventorPanel2;
    public GameObject stairsPanel1;
    public GameObject stairsPanel2;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput1;
    public Text notificationDisplay;
    public InputField puzzleInput2;
    public Text notificationDisplay2;
    public InputField puzzleInput3;
    public Text notificationDisplay3;

    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzleAnswer1;
    public string[] puzzleAnswer2;
    public string[] puzzleAnswer3;

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

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        cluePuzzleBtn2.onClick.AddListener(() => ClueWithPuzzle2(puzzleClue2));
        inventorBtn.onClick.AddListener(() => GetInside());

        CheckInventor();
        CheckStairs();
    }

    void GetInside()
    {
        GameData.instance.room4_2PuzzleOpen[4] = true;
        sceneController.OpenScene("Inventor Office");
    }

    void CheckInventor()
    {
        var complete = GameData.instance.room4_2PuzzleOpen[0];

        if (complete)
        {
            inventorPanel1.SetActive(false);
            inventorPanel2.SetActive(true);
        }
    }

    void CheckStairs()
    {
        var complete = GameData.instance.room4_2PuzzleOpen[1];

        if (complete)
        {
            stairsPanel1.SetActive(false);
            stairsPanel2.SetActive(true);
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
            GameData.instance.room4_2PuzzleOpen[0] = true;
           
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
        bool isComplete = GameData.instance.room4_2PuzzleOpen[0];
        var isComplete2 = GameData.instance.room4_2PuzzleOpen[4];

        if (isComplete2) GetInside();

        if (isComplete)
        {
            
            OpenPanel(panel);
        }
        else
        {
            //ClosePanel();
            OpenPanel(panel);
        }
    }

    public void ClueWithPuzzle2(RectTransform panel)
    {
        bool isComplete = GameData.instance.room4_2PuzzleOpen[1];
        var complete2 = GameData.instance.room4_2PuzzleOpen[2];

        if (isComplete && !complete2)
        {
            //SwitchCorrect1(true);
            OpenPanel(panel);
        }
        else if (complete2)
        {
           sceneController.OpenScene("Port");
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
            GameData.instance.room4_2PuzzleOpen[1] = true;
            AnswerCorrect(canAdvance);
            SwitchCorrect2(true);
        }
        else
        {
            StartCoroutine(AnswerFalse2());
        }
    }

    public void CheckingAnswer3(bool canAdvance)
    {
        var a = 0;

        for (int i = 0; i < puzzleAnswer3.Length; i++)
        {
            if (puzzleInput3.text.Equals(puzzleAnswer3[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            GameData.instance.room4_2PuzzleOpen[2] = true;
            AnswerCorrect(canAdvance);
            SwitchCorrect3(true);
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

    IEnumerator AnswerFalse3()
    {
        string prev = notificationDisplay3.text;

        notificationDisplay3.text = "Nothing happened";
        notificationDisplay3.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay3.text = prev;
        notificationDisplay3.color = Color.black;
    }

    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            clue1On = true;
            CheckInventor();
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
            clue1On = true;
            CheckStairs();
        }
        else
        {
            ClosePanel();
        }
    }

    private void SwitchCorrect3(bool isActive)
    {
        if (isActive)
        {
            sceneController.OpenScene("Port");
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
