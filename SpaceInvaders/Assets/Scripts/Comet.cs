using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    public GameObject stardustPrefab;
    public AudioClip deathKnell;
    AudioSource flyingSound;
    public float cometSpeed;
    public float minX, maxX;
    public float timer;
    public float firePeriod;
    public float numStarsSpawned;
    public Vector3 lastSpawnPosition;

    public GameObject[] invaders;
    public GameObject[] mysteryShips;
    public GameObject[] playerMissiles;
    public GameObject[] enemyMissiles;

    // Start is called before the first frame update
    void Start()
    {
        minX = -12f;
        maxX = 12f;
        timer = 0;
        firePeriod = 5.0f;
        numStarsSpawned = 0;
        lastSpawnPosition = gameObject.transform.position;

        flyingSound = GetComponent<AudioSource>();
        flyingSound.Play();
        cometSpeed = 8.0f;

        // Make sure player's stars collected resets
        PlayerShip.numStarsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused && !Global.levelWon)
        {
            Vector3 updatedPosition = gameObject.transform.position;

            if (Global.timeWarpMode)
            {
                cometSpeed = 0.55f;
            }
            else
            {
                cometSpeed = 8.0f;
            }

            updatedPosition += Vector3.right * cometSpeed * Time.deltaTime;

            if (cometSpeed < 0 && updatedPosition.x < minX || cometSpeed > 0 && updatedPosition.x > maxX)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.transform.position = updatedPosition;
            }

            // Spawn stardust three times
            float xi = Random.Range(0.0f, 1.0f);
            Vector3 dist = gameObject.transform.position - lastSpawnPosition;
            if (xi < 0.15f && numStarsSpawned < 3 && dist.x > 5)
            {
                numStarsSpawned++;
                lastSpawnPosition = gameObject.transform.position;
                //Debug.Log("Enemy Missile fired! Num Missiles Fired: " + numMissilesFired);

                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.z -= 0.75f; // add slight offset so that missile spawns at bottom of invader

                // instantiate the Missile
                GameObject obj = Instantiate(stardustPrefab, spawnPos, Quaternion.identity) as GameObject;
            }

            // If win level, reset ship speed 
            if (Global.invadersRemaining == 0)
            {
                ResetComet();
            }
        }
        else
        {
            flyingSound.Stop();
        }
    }

    public void ResetComet()
    {
        cometSpeed = 8.0f;
    }

    public void Die()
    {
        // Clear any debris on the ground
        ClearDebris();

        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        // Let player fire unlimited number of missiles
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.FireInfinite();
        Destroy(gameObject);
    }

    public void ClearDebris()
    {
        invaders = GameObject.FindGameObjectsWithTag("Invader");
        mysteryShips = GameObject.FindGameObjectsWithTag("MysteryShip");
        playerMissiles = GameObject.FindGameObjectsWithTag("PlayerMissile");
        enemyMissiles = GameObject.FindGameObjectsWithTag("EnemyMissile");

        for (var i = 0; i < invaders.Length; i++)
        {
            Invader invader = invaders[i].GetComponent<Invader>();
            if (invader.state == 0)
            {
                Destroy(invaders[i]);
            }
        }

        for (var i = 0; i < mysteryShips.Length; i++)
        {
            MysteryShip mysteryShip = mysteryShips[i].GetComponent<MysteryShip>();
            if (mysteryShip.state == 0)
            {
                Destroy(mysteryShips[i]);
            }
        }

        for (var i = 0; i < playerMissiles.Length; i++)
        {
            PlayerMissile playerMissile = playerMissiles[i].GetComponent<PlayerMissile>();
            if (playerMissile.state == 0)
            {
                Destroy(playerMissiles[i]);
            }
        }

        for (var i = 0; i < enemyMissiles.Length; i++)
        {
            EnemyMissile enemyMissile = enemyMissiles[i].GetComponent<EnemyMissile>();
            if (enemyMissile.state == 0)
            {
                Destroy(enemyMissiles[i]);
            }
        }
    }
}
