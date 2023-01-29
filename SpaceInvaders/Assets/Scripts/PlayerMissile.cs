using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
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

        // travel straight in the z-axis, add offset to x to prevent missiles from stacking
        thrust.z = 1000.0f;
        thrust.x = 20.0f * Random.Range(0.0f, 1.0f);

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
        if (currentPosition.z > 10.5)
        {
            // Turn on drag
            GetComponent<Rigidbody>().drag = 1;
            GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;

            //Destroy(gameObject);

            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
        }

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

        if (collider.CompareTag("Invader"))
        {
            // Get invader component
            Invader invader = collider.gameObject.GetComponent<Invader>();

            // If invader hit is alive and bullet is alive, kill invader
            if (invader.state == 1 && state == 1)
            {
                invader.Die();
                Deactivate();
            }

            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("MysteryShip"))
        {
            MysteryShip ship = collider.gameObject.GetComponent<MysteryShip>();

            // if mystery ship hit is alive and bullet is alive, kill ship
            if (ship.state == 1 && state == 1)
            { 
                ship.Die();
                Deactivate();
            }

            // Destroy the Missile that collided with the MysteryShip 
            //Destroy(gameObject);
            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            // let the other object handle its own death throes 
            shieldPiece.Die();
            // Destroy the Missile that collided with the ShieldPiece 
            Destroy(gameObject);
            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
        }
        else if (collider.CompareTag("Wall"))
        {
            // If player missile collides with ground wall, then set z constraint
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            if (PlayerShip.numMissilesFired > 0)
            {
                PlayerShip.numMissilesFired--;
            }
            Deactivate();
        }
        else if (collider.CompareTag("PlayerMissile"))
        {
            Deactivate();
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
        //Destroy(gameObject);

        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.gray);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
