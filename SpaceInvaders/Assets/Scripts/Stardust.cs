using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stardust : MonoBehaviour
{
    public AudioClip deathKnell;
    public Vector3 thrust;
    public Quaternion direction;

    // Start is called before the first frame update
    void Start()
    {
        // travel down in the z-axis 
        if (Global.timeWarpMode)
        {
            thrust.z = -100.0f;
        }
        else
        {
            thrust.z = -200.0f;
        }

        // do not passively decelerate 
        GetComponent<Rigidbody>().drag = 0.5f;

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
            // Play explosion clip
            AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

            Die();
        }
        else if (collider.CompareTag("Wall"))
        {
            Die();
        }
        else if (collider.CompareTag("Invader"))
        {
            //Invader invader = collider.gameObject.GetComponent<Invader>();

            //if (invader.state == 0)
            //{
            //    Die();
            //}
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            //Debug.Log("Collided with " + collider.tag);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
