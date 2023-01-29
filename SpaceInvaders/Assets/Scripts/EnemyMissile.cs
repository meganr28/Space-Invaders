using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion direction;
    public float minX, maxX;

    public int state;

    // Start is called before the first frame update
    void Start()
    {
        // Start alive
        state = 1;
        minX = -12f;
        maxX = 12f;

        // travel down in the z-axis 
        thrust.z = -600.0f;

        // do not passively decelerate 
        GetComponent<Rigidbody>().drag = 0;

        // set the direction it will travel in 
        GetComponent<Rigidbody>().MoveRotation(direction);

        // apply thrust once, no need to apply it again since 
        // it will not decelerate 
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;

        if (currentPosition.z < -10.5)
        {
            Destroy(gameObject);
        }

        if (currentPosition.x < minX || currentPosition.x > maxX)
        {
            // Let invader fall down the sides
            GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();

        if (!collider.CompareTag("PlayerShip"))
        {
            Deactivate();
        }

        if (collider.CompareTag("PlayerShip"))
        {
            PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();

            // Update lives counter

            // Either die or respawn player
            if (g.lives > 0 && state == 1)
            {
                g.lives--;
                player.Die();

                // If no more lives, then game over
                if (g.lives == 0)
                {
                    // Display GameOver and then reload scene
                    g.GameOver();
                }
                // else, pause and respawn player
                else
                {
                    g.LoseLife();
                }
            }

            Destroy(gameObject);
            //Deactivate();
            if (InvadersGrid.numMissilesFired > 0)
            {
                InvadersGrid.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("PlayerMissile"))
        {
            PlayerMissile playerMissile = collider.gameObject.GetComponent<PlayerMissile>();
            playerMissile.Deactivate();
            //Destroy(gameObject);
            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("Wall"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;

            //Destroy(gameObject);
            Deactivate();
            if (InvadersGrid.numMissilesFired > 0)
            {
                InvadersGrid.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            // let the other object handle its own death throes 
            shieldPiece.Die();
            // Destroy the Missile that collided with the ShieldPiece 
            Destroy(gameObject);
            if (InvadersGrid.numMissilesFired > 0)
            {
                InvadersGrid.numMissilesFired--;
            }
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            //Debug.Log("Collided with " + collider.tag);
        }
    }

    public void Deactivate()
    {
        state = 0;

        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.gray);
    }
}
