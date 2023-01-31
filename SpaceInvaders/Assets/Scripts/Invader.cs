using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public AudioClip deathKnell;
    public GameObject deathExplosion;
    public GameObject missile;
    public int invaderType; // small, medium, large
    public float minX, maxX;

    public int state; // 0 = dead, 1 = alive

    // Start is called before the first frame update
    void Start()
    {
        // Start alive
        state = 1;
        minX = -12f;
        maxX = 12f;
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
        if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            if (state == 1)
            {
                shieldPiece.Die();
            }
        }
        else if (collider.CompareTag("PlayerShip"))
        {
            PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();

            if (state == 1)
            {
                player.Die();

                // If invader collides with player, then automatic game over
                GameObject obj = GameObject.Find("GlobalObject");
                Global g = obj.GetComponent<Global>();
                if (g.lives > 0) g.lives--;
                g.GameOver();
            }
        }
        else if (collider.CompareTag("Wall"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
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
        state = 0;
        transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("DeadObjects");

        // Instantiate particle effect
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        int pointValue = 10;                   // large
        if (invaderType == 1) pointValue = 20; // medium
        if (invaderType == 2) pointValue = 30; // small

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;

        // Make invader fall to the ground
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;

        // If invader is not already dead, decrement the number of invaders remaining
        if (Global.invadersRemaining > 0)
        {
            Global.invadersRemaining--;
        }
        Debug.Log("Invaders remaining: " + Global.invadersRemaining);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
