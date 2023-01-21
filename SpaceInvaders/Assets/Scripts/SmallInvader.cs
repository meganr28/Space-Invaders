using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallInvader : MonoBehaviour
{
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Debug.Log("Score is now " + g.score);
        Destroy(gameObject);
    }
}
