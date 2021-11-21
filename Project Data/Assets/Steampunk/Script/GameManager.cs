using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script controll the area in the map that open according to the player progress
/// </summary>

public class GameManager : MonoBehaviour
{

    public GameData gameData;
    public Image mapImage;
    public GameObject[] hiddenMapButtons;
    public Sprite[] mapSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the scene to read the data
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        OpenNextArea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is used to Open the map area according to the player progression
    void OpenNextArea()
    {
        for(int i = 0; i < gameData.gamePhase;i++)
        {
            if(i < hiddenMapButtons.Length)
            {
                hiddenMapButtons[i].SetActive(true);
                mapImage.sprite = mapSprite[i];
            }
            
        }
    }
}
