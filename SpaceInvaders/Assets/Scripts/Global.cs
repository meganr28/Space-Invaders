using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public GameObject topCamera;
    public GameObject frontCamera;
    public GameObject playerPrefab;
    public static Vector3 respawnPosition;

    public static bool levelWon = false;
    public static bool resetGrid = false;
    public static bool isGameOver = false;
    public static bool isGamePaused = false;
    public static int invadersRemaining = 9;
    public int score;
    public int hiScore;
    public int level;
    public int lives;

    public GameObject[] invaders;
    public GameObject[] mysteryShips;
    public GameObject[] playerMissiles;
    public GameObject[] enemyMissiles;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = new Vector3(-9.0f, 0.0f, -8.3f);
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

        if (topCamera.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
        {
            topCamera.SetActive(false);
            frontCamera.SetActive(true);
        }
        else if (frontCamera.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
        {
            frontCamera.SetActive(false);
            topCamera.SetActive(true);
        }
    }

    public void NextLevel()
    {
        Debug.Log("CHANGING LEVEL");
        // Reset variables
        levelWon = true;
        invadersRemaining = 9;
        lives = 3;
        level = (level + 1) % 5;

        StartCoroutine(WaitForContinue());
    }

    public IEnumerator WaitForContinue()
    {
        Debug.Log("In wait for continue");
        while(!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }
        levelWon = false;
        resetGrid = true;

        // Clear any remaining debris on the screen
        invaders = GameObject.FindGameObjectsWithTag("Invader");
        mysteryShips = GameObject.FindGameObjectsWithTag("MysteryShip");
        playerMissiles = GameObject.FindGameObjectsWithTag("PlayerMissile");
        enemyMissiles = GameObject.FindGameObjectsWithTag("EnemyMissile");

        for (var i = 0; i < invaders.Length; i++)
        {
            Destroy(invaders[i]);
        }

        for (var i = 0; i < mysteryShips.Length; i++)
        {
            Destroy(mysteryShips[i]);
        }

        for (var i = 0; i < playerMissiles.Length; i++)
        {
            Destroy(playerMissiles[i]);
        }

        for (var i = 0; i < enemyMissiles.Length; i++)
        {
            Destroy(enemyMissiles[i]);
        }
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
