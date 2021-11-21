using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManagerRoom4_1 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    [Header("This area is for button clue puzzle")]
    public Button cluePuzzleBtn1;
    public Button cluePuzzleBtn2;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button clueBtn4;

    [Header("This is button for bartender")]
    public Button bartenderBtn2_8;
    public Button bartenderBtn2_9;
    public Button bartenderBtn2_10;
    public Button bartenderBtn2_11;
    public Button bartenderBtn2_12;
    public Button bartenderBtn2_13;
    public Button bartenderBtn2_14;
    public Button bartenderBtn2_15;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;
    public RectTransform puzzleClue2;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;
    public RectTransform clue4;

    [Header("This area is for all correct panel")]
    public GameObject bartenderPanel1;
    public GameObject bartenderPanel2;
    public GameObject bartenderPanel2_8;
    public GameObject bartenderPanel2_9;
    public GameObject bartenderPanel2_10;
    public GameObject bartenderPanel2_11;
    public GameObject bartenderPanel2_12;
    public GameObject bartenderPanel2_13;
    public GameObject bartenderPanel2_14;
    public GameObject bartenderPanel2_15;

    public GameObject[] bartenderPanel;

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

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        cluePuzzleBtn2.onClick.AddListener(() => ClueWithPuzzle2(puzzleClue2));

        clueBtn1.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => NonPuzzleClue(clue3));
        clueBtn4.onClick.AddListener(() => NonPuzzleClue(clue4));

        bartenderBtn2_8.onClick.AddListener(() => BartenderProgress("2_8",bartenderPanel2_8, bartenderPanel2_9));
        bartenderBtn2_9.onClick.AddListener(() => BartenderProgress("2_9", bartenderPanel2_9, bartenderPanel2_10));
        bartenderBtn2_10.onClick.AddListener(() => BartenderProgress("2_10", bartenderPanel2_10, bartenderPanel2_11));
        bartenderBtn2_11.onClick.AddListener(() => BartenderProgress("2_11", bartenderPanel2_11, bartenderPanel2_12));
        bartenderBtn2_12.onClick.AddListener(() => BartenderProgress("2_12", bartenderPanel2_12, bartenderPanel2_13));
        bartenderBtn2_13.onClick.AddListener(() => BartenderProgress("2_13", bartenderPanel2_13, bartenderPanel2_14));
        bartenderBtn2_14.onClick.AddListener(() => BartenderProgress("2_14", bartenderPanel2_14, bartenderPanel2_15));
        bartenderBtn2_15.onClick.AddListener(() => BartenderProgress("2_15", bartenderPanel2_15, bartenderPanel2_15));

        CheckBartender();
    }

    void BartenderProgress(string id, GameObject currentPanel, GameObject nextPanel)
    {
        if(id == "2_8")
        {
            GameData.instance.room4_1PuzzleOpen[2] = true;
        }
        else if(id == "2_9")
        {
            GameData.instance.room4_1PuzzleOpen[3] = true;
        }
        else if (id == "2_10")
        {
            GameData.instance.room4_1PuzzleOpen[4] = true;
        }
        else if (id == "2_11")
        {
            GameData.instance.room4_1PuzzleOpen[5] = true;
        }
        else if (id == "2_12")
        {
            GameData.instance.room4_1PuzzleOpen[6] = true;
        }
        else if (id == "2_13")
        {
            GameData.instance.room4_1PuzzleOpen[7] = true;
        }
        else if (id == "2_14")
        {
            GameData.instance.room4_1PuzzleOpen[8] = true;
        }
        else if (id == "2_15")
        {
            var complete = GameData.instance.room4_1PuzzleOpen[9];
            if (!complete) AnswerCorrect(true);
            GameData.instance.room4_1PuzzleOpen[9] = true;
            sceneController.OpenScene("Back Alley");
        }

        currentPanel.SetActive(false);
        nextPanel.SetActive(true);

    }

    void CheckBartender()
    {
        bool[] isComplete = GameData.instance.room4_1PuzzleOpen;

        for (int i = 0; i < bartenderPanel.Length; i++)
        {
            bartenderPanel[i].SetActive(false);
        }

        if (!isComplete[0]) bartenderPanel1.SetActive(true);

        if (isComplete[0] && !isComplete[1]) bartenderPanel2.SetActive(true);
        else if (isComplete[1] && !isComplete[2]) bartenderPanel2_8.SetActive(true);
        else if (isComplete[2] && !isComplete[3]) bartenderPanel2_9.SetActive(true);
        else if (isComplete[3] && !isComplete[4]) bartenderPanel2_10.SetActive(true);
        else if (isComplete[4] && !isComplete[5]) bartenderPanel2_11.SetActive(true);
        else if (isComplete[5] && !isComplete[6]) bartenderPanel2_12.SetActive(true);
        else if (isComplete[6] && !isComplete[7]) bartenderPanel2_13.SetActive(true);
        else if (isComplete[7] && !isComplete[8]) bartenderPanel2_14.SetActive(true);
        else if (isComplete[8]) bartenderPanel2_15.SetActive(true);
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
            GameData.instance.room4_1PuzzleOpen[0] = true;
            AnswerCorrect(canAdvance);
            SwitchCorrect1(true);
        }
        else
        {
            StartCoroutine(AnswerFalse1());
        }
    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room4_1PuzzleOpen[0];

        if (isComplete)
        {
            //SwitchCorrect1(true);
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
        bool isComplete = GameData.instance.room4_1PuzzleOpen[10];

        if (isComplete)
        {
            sceneController.OpenScene("Cigar Room");
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
            GameData.instance.room4_1PuzzleOpen[1] = true;
            AnswerCorrect(canAdvance);
            CheckBartender();
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
            GameData.instance.room4_1PuzzleOpen[10] = true;
            AnswerCorrect(canAdvance);
            sceneController.OpenScene("Cigar Room");
        }
        else
        {
            StartCoroutine(AnswerFalse2());
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

    //This coroutine is used to add a message for the player when player put a wrong answer in puzzle 1 input
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

    // This function is used to activate a clue when puzzle 1 answer is correct
    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            clue1On = true;
            CheckBartender();
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
