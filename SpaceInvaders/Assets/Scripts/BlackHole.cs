using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public int numMissilesReceived;
    public float blackHoleSpeed;
    public float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        numMissilesReceived = 0;
        blackHoleSpeed = 3.0f;
        minX = -12f;
        maxX = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused && !Global.levelWon)
        {
            Vector3 updatedPosition = gameObject.transform.position;
            //updatedPosition.x += shipSpeed;
            updatedPosition += Vector3.right * blackHoleSpeed * Time.deltaTime;

            if (blackHoleSpeed < 0 && updatedPosition.x < minX || blackHoleSpeed > 0 && updatedPosition.x > maxX)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.transform.position = updatedPosition;
                gameObject.transform.Rotate(Vector3.up * 100.0f * Time.deltaTime);
            }

            // If win level, reset ship speed 
            if (Global.invadersRemaining == 0)
            {
                ResetBlackHole();
            }
        }
        else
        {
            //flyingSound.Stop();
        }
    }

    public void ResetBlackHole()
    {
        blackHoleSpeed = 3.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("ShieldPiece"))
        {
            ShieldPiece shieldPiece = collider.gameObject.GetComponent<ShieldPiece>();
            shieldPiece.Die();

            // Uncomment if you want to spawn something from black hole
            //Vector3 spawnPos = invader.position;
            //spawnPos.z -= 0.75f; // add slight offset so that missile spawns at bottom of invader

            //// instantiate the Missile
            //GameObject obj = Instantiate(enemyMissilePrefab, spawnPos, Quaternion.identity) as GameObject;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetTimeWarp()
    {
        numMissilesReceived++;

        if (numMissilesReceived == 3)
        {
            Global.timeWarpsGranted++;
        }
    }
}
