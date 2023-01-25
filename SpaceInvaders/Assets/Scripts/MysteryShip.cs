using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public AudioClip deathKnell;
    AudioSource flyingSound;
    public int pointValue;
    public float shipSpeed;

    // Start is called before the first frame update
    void Start()
    {
        flyingSound = GetComponent<AudioSource>();
        pointValue = 200;
        shipSpeed = 3.0f;

        flyingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused)
        {
            Vector3 updatedPosition = gameObject.transform.position;
            //updatedPosition.x += shipSpeed;
            updatedPosition += Vector3.right * shipSpeed * Time.deltaTime;

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
        else
        {
            flyingSound.Stop();
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
        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }
}
