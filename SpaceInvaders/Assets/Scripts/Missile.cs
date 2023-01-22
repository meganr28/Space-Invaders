using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
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
        if (collider.CompareTag("SmallInvader"))
        {
            SmallInvader invader = collider.gameObject.GetComponent<SmallInvader>();
            // let the other object handle its own death throes 
            invader.Die();
            // Destroy the Bullet which collided with the Asteroid 
            Destroy(gameObject);
        }
        else if (collider.CompareTag("MediumInvader"))
        {
            MediumInvader invader = collider.gameObject.GetComponent<MediumInvader>();
            invader.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("LargeInvader"))
        {
            LargeInvader invader = collider.gameObject.GetComponent<LargeInvader>(); 
            invader.Die();
            Destroy(gameObject);
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            Debug.Log("Collided with " + collider.tag);
        }
    }
}
