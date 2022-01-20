using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStartScript : MonoBehaviour
{
    public Text userMessage;
    public GameObject gameStart;
    public GameObject gameRules;
    public GameObject newUser;
    TextReader readUserData = new StreamReader("C:/Users/peter/Desktop/userdata.txt");
    // Start is called before the first frame update
    void Start()
    {
        string lineRead = readUserData.ReadLine();
        gameStart.SetActive(false);
        gameRules.SetActive(false);
        newUser.SetActive(false);
        if (lineRead.Contains("$default"))
        {
            userMessage.text = "Hello New User";
            SceneManager.LoadScene("UseerDetails");
            //newUser.SetActive(true);
        }
        else
        {
            userMessage.text = "Hello " + lineRead;
            gameStart.SetActive(true);
            gameRules.SetActive(true);
            userMessage.text = "\n got here";
        }
        readUserData.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
