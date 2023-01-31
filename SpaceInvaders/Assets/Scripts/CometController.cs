using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometController : MonoBehaviour
{
    public GameObject comet;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(-10.0f, 0.0f, 8.0f);

        // Spawn ships at certain intervals
        if (!Global.isGamePaused && !Global.levelWon)
        {
            InvokeRepeating("SpawnComet", 18f, 35f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnComet()
    {
        // instantiate the Mystery Ship
        GameObject obj = Instantiate(comet, spawnPosition, Quaternion.identity) as GameObject;
    }
}
