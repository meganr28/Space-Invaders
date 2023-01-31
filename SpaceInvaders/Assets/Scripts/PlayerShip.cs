using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerShip : MonoBehaviour
{
    public static int numMissilesFired = 0;

    public AudioClip deathKnell;
    public GameObject deathExplosion;
    public GameObject missile;
    public float playerSpeed;
    public float minX, maxX;
    public int numStarsCollected;

    public PostProcessVolume postProcessVolume;
    public Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 7.0f;
        minX = -11.5f;
        maxX = 11.5f;
        numMissilesFired = 0;

        postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Global.isGamePaused)
        {
            GameObject globalObj = GameObject.Find("GlobalObject");
            Global g = globalObj.GetComponent<Global>();
            Renderer renderer = GetComponent<Renderer>();
            Vector3 updatedPosition = gameObject.transform.position; // gameObject refers to object this is currently attached to

            // Only move if within boundaries
            if (updatedPosition.x < maxX && Input.GetAxisRaw("Horizontal") > 0)
            {
                updatedPosition += Vector3.right * playerSpeed * Time.deltaTime;
            }
            else if (updatedPosition.x > minX && Input.GetAxisRaw("Horizontal") < 0)
            {
                updatedPosition -= Vector3.right * playerSpeed * Time.deltaTime;
            }

            gameObject.transform.position = updatedPosition;

            // Handle missile firing
            if (Input.GetKeyDown("space") && Global.firePlayerMissile)    
            {
                Debug.Log("Missile fired!");    

                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.z += 0.5f; // add slight offset so that bullet spawns at front of player ship

                // instantiate the Missile
                GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity) as GameObject;

                // get the Missile Script Component of the new Bullet instance 
                PlayerMissile m = obj.GetComponent<PlayerMissile>();
                // set the direction the Bullet will travel in 
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
                m.direction = rot;

                numMissilesFired++;
                if (Global.missilesRemaining > 0 && !g.infiniteMissiles)
                {
                    Global.missilesRemaining--;
                }

                // Increment total shots taken
                MysteryShip.playerShots++;
            }

            // Handle health "glow" if you collect stardust
            if (numStarsCollected == 3)
            {
                renderer.material.EnableKeyword("_EMISSION");
                renderer.material.SetColor("_EmissionColor", Color.yellow);
                //bloom.enabled.value = true;
                //bloom.intensity.value = 4.0f * numStarsCollected;
            }
            else
            {
                renderer.material.DisableKeyword("_EMISSION");
                renderer.material.SetColor("_EmissionColor", Color.black);
                //bloom.enabled.value = false;
                //bloom.intensity.value = 0.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Stardust"))
        {
            numStarsCollected++;
            Debug.Log("Num Stars: " + numStarsCollected);
        }
    }

    public void Die()
    {
        // Instantiate particle effect
        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        // Play explosion clip
        AudioSource.PlayClipAtPoint(deathKnell, Camera.allCameras[0].transform.position);

        Destroy(gameObject);
    }

    public void Respawn()
    {
        gameObject.transform.position = Global.respawnPosition;
    }
}
