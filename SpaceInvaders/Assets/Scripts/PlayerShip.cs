using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public static int numMissilesFired = 0;

    public AudioClip deathKnell;
    public GameObject deathExplosion;
    public GameObject missile;
    public float playerSpeed;
    public float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 7.0f;
        minX = -11.5f;
        maxX = 11.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused)
        {
            Vector3 updatedPosition = gameObject.transform.position; // gameObject refers to object this is currently attached to

            // Only move if within boundaries
            if (updatedPosition.x < maxX && Input.GetAxisRaw("Horizontal") > 0)
            {
                updatedPosition += Vector3.right * playerSpeed * Time.deltaTime;
            }
            else if (updatedPosition.x > minX && Input.GetAxisRaw("Horizontal") < 0)
            {
                updatedPosition -= Vector3.right * playerSpeed * Time.deltaTime;
            }

            gameObject.transform.position = updatedPosition;

            // Handle missile firing
            if (Input.GetKeyDown("space"))    
            {
                Debug.Log("Missile fired!");    

                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.z += 0.5f; // add slight offset so that bullet spawns at front of player ship

                // instantiate the Missile
                GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;

                // get the Missile Script Component of the new Bullet instance 
                PlayerMissile m = obj.GetComponent<PlayerMissile>();
                // set the direction the Bullet will travel in 
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
                m.direction = rot;

                numMissilesFired++;

                // Increment total shots taken
                MysteryShip.playerShots++;
            }
        }
    }

    public void Die()
    {
        // Instantiate particle effect
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        Destroy(gameObject);
    }

    public void Respawn()
    {
        gameObject.transform.position = Global.respawnPosition;
    }
}
