using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If 'X' is pressed, then start game
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("GameplayScene");
        }

        // If 'esc' is pressed, then exit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
