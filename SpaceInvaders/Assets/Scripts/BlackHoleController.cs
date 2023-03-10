using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public GameObject blackHole;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(-15.0f, 0.0f, -4.0f);

        // Spawn ships at certain intervals
        if (!Global.isGamePaused && !Global.levelWon)
        {
            InvokeRepeating("SpawnBlackHole", 10f, 25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlackHole()
    {
        if (Global.gridZ > 0.5f)
        {
            // instantiate Black Hole
            GameObject obj = Instantiate(blackHole, spawnPosition, Quaternion.identity) as GameObject;
        }      
    }
}
