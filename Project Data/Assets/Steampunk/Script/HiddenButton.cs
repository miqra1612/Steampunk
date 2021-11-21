using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenButton : MonoBehaviour
{
    public InputField puzzleInput;
    public GameObject hiddenButton;
    public string answer;

    public void OpenHiddenButton()
    {
        if(puzzleInput.text == answer)
        {

            hiddenButton.SetActive(true);
        }

    }
}
