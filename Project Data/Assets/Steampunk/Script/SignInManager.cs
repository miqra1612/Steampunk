using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

/// <summary>
/// This script is used to controll user sign in process to the game
/// </summary>

public class SignInManager : MonoBehaviour
{

    public InputField signInPassword;
    public InputField teamName;
    public GameObject signInPanel;
    public GameObject failText;
    public Text userMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.V))
        //{
        //    PastingText();
        //}

        //if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.C))
        //{
        //    CopyingText();
        //}
    }

    void CopyingText()
    {
        TextEditor tc = new TextEditor();
        tc.text = signInPassword.text;
        tc.SelectAll();
        tc.Copy();
        
    }

    void PastingText()
    {
        TextEditor te = new TextEditor();
        te.multiline = true;
        te.Paste();
        signInPassword.text = te.text;
    }

    // This function read the password input from the user and allow user to begin the game when the password is correct
    public void DebugSignIn()
    {
        if(signInPassword.text != "")
        {
            GameData.instance.teamName = teamName.text;
            signInPanel.SetActive(false);
        }
        else
        {
            failText.SetActive(true);
            StartCoroutine(LogInFail());
        }
       
    }

    public void SignIn()
    {
        if (signInPassword.text != "")
        {
            StartCoroutine(RealLogIn());
        }
        else
        {
            failText.SetActive(true);
            StartCoroutine(LogInFail());
        }

    }

    // This coroutine is used to give message for the player when there is problem during log in
    IEnumerator LogInFail()
    {
        yield return new WaitForSeconds(10);
        failText.SetActive(false);
    }

    IEnumerator RealLogIn()
    {
        WWWForm form = new WWWForm();
      
        form.AddField("code", signInPassword.text);  
        
        WWW host = new WWW("https://api.teambreak.com/code", form);
        yield return host;

        Debug.Log(host.text);

        JSONNode jsonNode = SimpleJSON.JSON.Parse(host.text);

        //using json if success = true continue else display message into the user
        
        string success = jsonNode["success"].Value.ToString();

        if (success == "True")
        {
            GameData.instance.teamName = teamName.text;;
            signInPanel.SetActive(false);
            //StartCoroutine(UsedPassword());
        }
        else
        {
            userMessage.text = jsonNode["message"].Value.ToString();
            failText.SetActive(true);
            StartCoroutine(LogInFail());
        }
    }

    //IEnumerator UsedPassword()
    //{
    //    yield return new WaitForEndOfFrame();

    //    int a = 1;

    //    WWWForm form = new WWWForm();
    //    form.AddField("username", GameData.instance.username);
    //    form.AddField("count", a);

    //    WWW www = new WWW("https://api.teambreak.com/code", form);
    //    yield return www;

    //    Debug.Log("www:  " + www.text);

    //    if (www.text.Split()[2] != "0")
    //    {
    //        Debug.Log("password used, you cannot use it anymore!");
    //    }
    //    else
    //    {
    //        Debug.Log("save data error: password used count fail");
    //    }

    //}
}
