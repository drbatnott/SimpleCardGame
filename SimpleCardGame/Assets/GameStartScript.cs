using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    public Text userMessage;
    TextReader readUserData = new StreamReader("C:/Users/peter/Desktop/userdata.txt");
    // Start is called before the first frame update
    void Start()
    {
        string lineRead = readUserData.ReadLine();
        if (lineRead.Contains("$default_user"))
        {
            userMessage.text = "Hello New User";
        }
        else
            userMessage.text = "Hello " + lineRead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
