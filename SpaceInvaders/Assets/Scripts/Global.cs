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
    public static bool firePlayerMissile = true;
    public static bool timeWarpMode = false;
    public static int invadersRemaining = 55;
    public static int missilesRemaining = 20;
    public static int timeWarpsGranted = 0;
    public static float gridZ = 3;
    public int score;
    public int hiScore;
    public int level;
    public int lives;
    public bool infiniteMissiles = false;

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
        ResetStaticVariables();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScene");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (timeWarpsGranted > 0)
            {
                timeWarpsGranted--;
                SetPostProcess();
                StartCoroutine(TimeWarp(10.0f));
            }
        }

        if (missilesRemaining <= 0)
        {
            firePlayerMissile = false;
        }
        else
        {
            firePlayerMissile = true;
        }
    }

    public void NextLevel()
    {
        Debug.Log("CHANGING LEVEL");
        // Reset variables
        levelWon = true;
        invadersRemaining = 55;
        missilesRemaining = 20;
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
        ResetStaticVariables();
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

    public IEnumerator TimeWarp(float warpDuration)
    {
        Debug.Log("In Pause Lost Life");
        // Time warp for 10 seconds
        timeWarpMode = true;
        float timeWarpEndTime = Time.realtimeSinceStartup + warpDuration;
        while (Time.realtimeSinceStartup < timeWarpEndTime)
        {
            yield return 0;
        }
        // Stop time warp and restore everything to normal speed
        timeWarpMode = false;
        SetPostProcess();
    }

    public void SetPostProcess()
    {
        TimeWarpPostProcess timeWarpTop = topCamera.GetComponent<TimeWarpPostProcess>();
        TimeWarpPostProcess timeWarpFront = frontCamera.GetComponent<TimeWarpPostProcess>();
        timeWarpTop.enabled = !timeWarpTop.enabled;
        timeWarpFront.enabled = !timeWarpFront.enabled;
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

    public void ResetStaticVariables()
    {
        levelWon = false;
        resetGrid = false;
        isGameOver = false;
        isGamePaused = false;
        invadersRemaining = 55;
        missilesRemaining = 20;
        timeWarpsGranted = 0;
        gridZ = 3 - (level - 1);
    }

    public void FireInfinite()
    {
        infiniteMissiles = true;
        // Start coroutine that let's player fire infinitely for 5 seconds
        // Then turns off infinite missiles
        Invoke("SetInfiniteFalse", 5.0f);
    }

    public void SetInfiniteFalse()
    {
        Debug.Log("SETTING INFINITE TO FALSE");
        infiniteMissiles = false;
    }
}
