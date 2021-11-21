using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class OutroRoomManager : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public bool lastRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        clueBtn1.onClick.AddListener(() => CalculateFinishTime());
    }

    public void CalculateFinishTime()
    {
        if (lastRoom)
        {
            gameData.CalculateFinishTime();
            sceneController.OpenScene("Winner Room");
        }
    }
}
