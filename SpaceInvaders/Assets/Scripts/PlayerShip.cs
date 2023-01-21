using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public GameObject missile;
    public float playerSpeed;
    public float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 0.1f;
        minX = -9.5f;
        maxX = 9.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPosition = gameObject.transform.position; // gameObject refers to object this is currently attached to

        // Only move if within boundaries
        if (updatedPosition.x < maxX && Input.GetAxisRaw("Horizontal") > 0)
        { 
            updatedPosition.x += playerSpeed;
        }
        else if (updatedPosition.x > minX && Input.GetAxisRaw("Horizontal") < 0)
        {
            updatedPosition.x -= playerSpeed;
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
            Missile m = obj.GetComponent<Missile>();
            // set the direction the Bullet will travel in 
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
            m.direction = rot;
        }
    }
}
