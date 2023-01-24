using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion direction;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("PlayerShip"))
        {
            PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();

            // Update lives counter
            GameObject obj = GameObject.Find("GlobalObject");
            Global g = obj.GetComponent<Global>();

            // Either die or respawn player
            if (g.lives > 0)
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
            if (InvadersGrid.numMissilesFired > 0)
            {
                InvadersGrid.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("PlayerMissile"))
        {
            PlayerMissile playerMissile = collider.gameObject.GetComponent<PlayerMissile>();
            playerMissile.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
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
            Debug.Log("Collided with " + collider.tag);
        }
    }
}
