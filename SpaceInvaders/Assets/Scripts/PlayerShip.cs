using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
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

        if (updatedPosition.x < maxX && Input.GetAxisRaw("Horizontal") > 0)
        { 
            updatedPosition.x += playerSpeed;
        }
        else if (updatedPosition.x > minX && Input.GetAxisRaw("Horizontal") < 0)
        {
            updatedPosition.x -= playerSpeed;
        }

        gameObject.transform.position = updatedPosition;
    }
}

// Add x positions that it cannot go past (constrain to remain within screen)
