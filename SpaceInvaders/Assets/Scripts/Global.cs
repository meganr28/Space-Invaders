using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject playerPrefab;
    public static Vector3 respawnPosition;

    public static bool levelWon = false;
    public static bool isGameOver = false;
    public static bool isGamePaused = false;
    public static int invadersRemaining = 55;
    public int score;
    public int hiScore;
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
        if (invadersRemaining == 0 && !isGameOver)
        {
            levelWon = true;
        }
    }

    public void NextLevel()
    {
        // Reset variables
        invadersRemaining = 55;
        lives = 3;
        level = (level + 1) % 5;
    }

    public void LoseLife()
    {
        // Pause game and instantiate player at different position
        StartCoroutine(PauseGameLife(3.0f));
    }

    public IEnumerator PauseGameLife(float pauseDuration)
    {
        Debug.Log("In Pause Lost Life");
        // Pause game for 3 seconds
        isGamePaused = true;
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseDuration;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        // Unpause game and respawn player
        Instantiate(playerPrefab, respawnPosition, Quaternion.identity);
        isGamePaused = false;
    }

    public IEnumerator PauseGame(float pauseDuration)
    {
        Debug.Log("In Pause Game Over");
        // Pause game for 5 seconds
        isGamePaused = true;
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseDuration;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        // Unpause game
        isGamePaused = false;
        RestartGame();
    }

    public void RestartGame()
    {
        Global.isGameOver = false;
        SceneManager.LoadScene("StartScene");
    }

    public void GameOver()
    {
        Global.isGameOver = true;
        StartCoroutine(PauseGame(5.0f));
    }
}
