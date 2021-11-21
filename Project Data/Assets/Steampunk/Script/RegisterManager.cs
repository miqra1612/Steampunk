using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RegisterManager : MonoBehaviour
{
    public InputField password;

    public void RegisteringUser()
    {
        StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", password.text);
        form.AddField("password", password.text);
        form.AddField("count", 0); 
        WWW host = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return host;

        if(host.text == "0")
        {
            Debug.Log("success registering user");
        }
        else
        {
            Debug.Log("fail registering user");
        }
    }
}
