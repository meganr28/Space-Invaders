using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShipController : MonoBehaviour
{
    public GameObject mysteryShip;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(-10.0f, 0.0f, 8.0f);

        // Spawn ships at certain intervals
        InvokeRepeating("SpawnMysteryShip", 5f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMysteryShip()
    {
        // instantiate the Mystery Ship
        GameObject obj = Instantiate(mysteryShip, spawnPosition, Quaternion.identity) as GameObject;

        //// update start position and speed
        //MysteryShip m = obj.GetComponent<MysteryShip>();
        //m.UpdateSpeed();
        //spawnPosition.x *= -1.0f;
    }
}
