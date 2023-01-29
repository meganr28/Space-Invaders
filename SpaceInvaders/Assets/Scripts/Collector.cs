using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
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
        if (collider.CompareTag("PlayerMissile"))
        {
            PlayerMissile playerMissile = collider.gameObject.GetComponent<PlayerMissile>();
            playerMissile.Die();
            Global.missilesRemaining++;
        }
        else if (collider.CompareTag("EnemyMissile"))
        {
            EnemyMissile enemyMissile = collider.gameObject.GetComponent<EnemyMissile>();
            enemyMissile.Die();
            Global.missilesRemaining++;
        }
        else if (collider.CompareTag("Invader"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.Destroy();
            Global.missilesRemaining += 2;
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            //Debug.Log("Collided with " + collider.tag);
        }
    }
}
