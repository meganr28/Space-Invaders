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
        GameObject globalObj = GameObject.Find("GlobalObject");
        Global g = globalObj.GetComponent<Global>();
        Collider collider = collision.collider;
        if (collider.CompareTag("PlayerMissile"))
        {
            PlayerMissile playerMissile = collider.gameObject.GetComponent<PlayerMissile>();
            playerMissile.Die();
            if (!g.infiniteMissiles)
            {
                Global.missilesRemaining++;
            }
        }
        else if (collider.CompareTag("EnemyMissile"))
        {
            EnemyMissile enemyMissile = collider.gameObject.GetComponent<EnemyMissile>();
            enemyMissile.Die();
            if (!g.infiniteMissiles)
            {
                Global.missilesRemaining++;
            }
        }
        else if (collider.CompareTag("Invader"))
        {
            Invader invader = collider.gameObject.GetComponent<Invader>();
            invader.Destroy();
            if (!g.infiniteMissiles)
            {
                Global.missilesRemaining++;
            }
        }
        else if (collider.CompareTag("MysteryShip"))
        {
            MysteryShip mysteryShip = collider.gameObject.GetComponent<MysteryShip>();
            mysteryShip.Destroy();
            if (!g.infiniteMissiles)
            {
                Global.missilesRemaining += 3;
            }
        }
        else
        {
            // if we collided with something else, print to console 
            // what the other thing was 
            //Debug.Log("Collided with " + collider.tag);
        }
    }
}
