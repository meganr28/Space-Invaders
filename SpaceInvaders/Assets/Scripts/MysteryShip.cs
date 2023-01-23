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
        shipSpeed = 0.003f;
        //gameObject.transform.position = new Vector3(-10.0f, 0.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPosition = gameObject.transform.position;
        updatedPosition.x += shipSpeed;

        if (shipSpeed < 0 && updatedPosition.x < -10.0f || shipSpeed > 0 && updatedPosition.x > 10.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.position = updatedPosition;
        }
    }

    public void UpdateSpeed()
    {
        shipSpeed *= -1.0f;
    }

    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }
}
