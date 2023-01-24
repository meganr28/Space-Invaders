using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public GameObject missile;
    public int invaderType; // small, medium, large

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireMissile();
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
        int pointValue = 10;                   // large
        if (invaderType == 1) pointValue = 20; // medium
        if (invaderType == 2) pointValue = 30; // small

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }

    public void FireMissile()
    {
        // Handle missile firing
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Enemy Missile fired!");

            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z -= 0.75f; // add slight offset so that bullet spawns at front of player ship

            // instantiate the Missile
            GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;
            // get the Missile Script Component of the new Bullet instance 
            EnemyMissile m = obj.GetComponent<EnemyMissile>();
            // set the direction the Bullet will travel in 
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
            m.direction = rot;
        }
    }
}
