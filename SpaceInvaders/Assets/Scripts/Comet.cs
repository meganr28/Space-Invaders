using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    public AudioClip deathKnell;
    //AudioSource flyingSound;
    public float cometSpeed;
    public float minX, maxX;
    public float timer;
    public float firePeriod;

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

        //flyingSound = GetComponent<AudioSource>();
        cometSpeed = 8.0f;

        //flyingSound.Play();
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

            // If win level, reset ship speed 
            if (Global.invadersRemaining == 0)
            {
                ResetComet();
            }
        }
        else
        {
            //flyingSound.Stop();
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

        //for (var i = 0; i < enemyMissiles.Length; i++)
        //{
        //    Destroy(enemyMissiles[i]);
        //}
    }
}
