using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public static Vector3 respawnPosition;
    public static bool levelWon = false;
    public static bool isGameOver = false;
    public static int invadersRemaining = 55;
    public int score;
    public int level;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = new Vector3(-9.0f, 0.0f, -7.5f);
        score = 0;
        level = 0;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel()
    {
        invadersRemaining = 9;
        lives = 3;
        level = (level + 1) % 5;
    }

    public IEnumerator PauseGame(float pauseDuration)
    {
        Debug.Log("In Pause Game");
        // Pause game for 5 seconds
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseDuration;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        ResetGame();
    }

    public void ResetGame()
    {
        Global.isGameOver = false;
        Debug.Log("Is Game Over 2: " + Global.isGameOver);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // TODO: change this to start screen
    }

    public void GameOver()
    {
        Global.isGameOver = true;
        Debug.Log("Is Game Over 1: " + Global.isGameOver);

        StartCoroutine(PauseGame(5.0f));
    }
}
