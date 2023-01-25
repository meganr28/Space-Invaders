using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public AudioClip deathKnell;
    public GameObject missile;
    public int invaderType; // small, medium, large

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            shieldPiece.Die();
        }
        else if (collider.CompareTag("PlayerShip"))
        {
            PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
            player.Die();

            // If invader collides with player, then automatic game over
            GameObject obj = GameObject.Find("GlobalObject");
            Global g = obj.GetComponent<Global>();
            if (g.lives > 0) g.lives--;
            g.GameOver();
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
        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.main.transform.position);

        int pointValue = 10;                   // large
        if (invaderType == 1) pointValue = 20; // medium
        if (invaderType == 2) pointValue = 30; // small

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }
}
