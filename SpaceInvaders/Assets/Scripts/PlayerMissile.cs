using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion direction;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the z-axis 
        thrust.z = 600.0f;

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
        if (collider.CompareTag("Invader"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            // let the other object handle its own death throes 
            invader.Die();
            // Increment the number of invaders killed
            Global.invadersRemaining--;
            Debug.Log("Invaders remaining: " + Global.invadersRemaining);
            // Destroy the Missile that collided with the Invader 
            Destroy(gameObject);
        }
        else if (collider.CompareTag("MysteryShip"))
        {
            MysteryShip ship = collider.gameObject.GetComponent<MysteryShip>();
            // let the other object handle its own death throes 
            ship.Die();
            // Destroy the Missile that collided with the MysteryShip 
            Destroy(gameObject);
        }
        else if (collider.CompareTag("ShieldPiece"))
        {
            Debug.Log("collided with shield piece");
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            // let the other object handle its own death throes 
            shieldPiece.Die();
            // Destroy the Missile that collided with the ShieldPiece 
            Destroy(gameObject);
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            Debug.Log("Collided with " + collider.tag);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
