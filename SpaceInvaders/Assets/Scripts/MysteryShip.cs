using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public AudioClip deathKnell;
    AudioSource flyingSound;
    public float shipSpeed;
    public float minX, maxX;

    public int state; // 0 = dead, 1 = alive

    public static int playerShots = 0;
    public int[] pointValues = { 100, 50, 50, 100, 150, 100, 100, 50, 300, 100, 100, 100, 50, 150, 100, 50 };

    // Start is called before the first frame update
    void Start()
    {
        // Start alive
        state = 1;
        minX = -12f;
        maxX = 12f;
        playerShots = 0;

        flyingSound = GetComponent<AudioSource>();
        shipSpeed = 3.0f;

        flyingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused && !Global.levelWon)
        {
            Vector3 updatedPosition = gameObject.transform.position;

            if (Global.timeWarpMode)
            {
                shipSpeed = 0.35f;
            }
            else
            {
                shipSpeed = 3.0f;
            }
            updatedPosition += Vector3.right * shipSpeed * Time.deltaTime;

            if (shipSpeed < 0 && updatedPosition.x < minX || shipSpeed > 0 && updatedPosition.x > maxX)
            {
                if (state == 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                gameObject.transform.position = updatedPosition;
            }

            // If win level, reset ship speed 
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

    public void ResetShip()
    {
        shipSpeed = 3.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            shieldPiece.Die();
        }
    }

    public void Die()
    {
        state = 0;

        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValues[playerShots % 15];

        // Make ship fall to the ground
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
