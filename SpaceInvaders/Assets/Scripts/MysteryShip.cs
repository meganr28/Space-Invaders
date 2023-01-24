using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public int pointValue;
    public float shipSpeed;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 200;
        shipSpeed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPosition = gameObject.transform.position;
        updatedPosition.x += shipSpeed;

        if (shipSpeed < 0 && updatedPosition.x < -12.0f || shipSpeed > 0 && updatedPosition.x > 12.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.position = updatedPosition;
        }

        // If win level, reset grid 
        if (Global.invadersRemaining == 0)
        {
            ResetShip();
        }
    }

    public void UpdateSpeed()
    {
        shipSpeed *= -1.0f;
    }

    public void ResetShip()
    {
        shipSpeed = 0.05f;
    }

    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }
}
