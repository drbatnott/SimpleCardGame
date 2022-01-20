using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDetailsEntry : MonoBehaviour
{
    TextReader readUserData = new StreamReader("C:/Users/peter/Desktop/userdata.txt");
    public Text scoreText;
    public InputField userNameField;
    public GameObject openGameButton;
    // Start is called before the first frame update
    void Start()
    {
        string lineRead = readUserData.ReadLine();
        openGameButton.SetActive(false);
        if (!lineRead.Contains("$default"))
        {
            userNameField.text = lineRead;
            openGameButton.SetActive(true);
        }
        readUserData.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInputFieldChanged()
    {
        openGameButton.SetActive(true);
    }
    public void OpenButtonClick()
    {
        TextWriter writeUserData = new StreamWriter("C:/Users/peter/Desktop/userdata.txt");
        writeUserData.WriteLine(userNameField.text);
        writeUserData.WriteLine(scoreText.text);
        writeUserData.Close();
        SceneManager.LoadScene("GameOpening");
    }
}
