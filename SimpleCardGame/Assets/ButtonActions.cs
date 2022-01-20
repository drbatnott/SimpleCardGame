using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRulesButtonClick()
    {
        SceneManager.LoadScene("RulesScene");
    }
    public void OnCloseGame()
    {
        Application.Quit();
    }
}
