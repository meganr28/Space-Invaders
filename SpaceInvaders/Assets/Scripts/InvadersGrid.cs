using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersGrid : MonoBehaviour
{
    public GameObject smallInvaderPrefab;
    public GameObject mediumInvaderPrefab;
    public GameObject largeInvaderPrefab;
    public GameObject enemyMissilePrefab;

    public static int numMissilesFired = 0;

    public Vector3 center;
    public float invaderSpeed;
    public float minX, maxX;

    private Vector3 direction = Vector3.right;
    private float decrementStep = 0.5f;
    private float spacing = 1.5f;
    private int rows = 5;
    private int columns = 11;

    void Awake()
    {
        InstantiateGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        invaderSpeed = 1.0f;
        minX = -11.5f;
        maxX = 11.5f;

        // Fire missiles at certain intervals
        if (!Global.isGamePaused)
        {
            InvokeRepeating("FireMissiles", 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Invaders should always move in one of the horizontal directions
        this.transform.position += direction * invaderSpeed * Time.deltaTime;

        // Loop through each child (invader) transform
        foreach (Transform invader in this.transform)
        {
            // If invaders have reached either edge, flip direction and decrement row
            if (direction == Vector3.left && invader.position.x < minX || direction == Vector3.right && invader.position.x > maxX)
            {
                //Debug.Log("Decrementing step");
                direction *= -1.0f;
                this.transform.position -= Vector3.forward * decrementStep;
            }
        }

        // If win level, reset grid 
        if (Global.invadersRemaining == 0 && !Global.isGameOver)
        {
            Debug.Log("Reinstantiating Grid");
            GameObject obj = GameObject.Find("GlobalObject");
            Global g = obj.GetComponent<Global>();
            g.NextLevel();
            ResetGrid();
        }

        // Increase speed after certain number of invaders remaining
        if (Global.invadersRemaining <= 10)
        {
            invaderSpeed = 0.85f / (Global.invadersRemaining / 10.0f);
        }
    }

    public void InstantiateGrid()
    {
        // Change the center position depending on the level
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        center = new Vector3(0, 0, 3) - (g.level - 1) * Vector3.forward;

        // How far the grid extends in either direction
        int extentX = (this.columns - 1) / 2;
        int extentZ = (this.rows - 1) / 2;

        // Instantiate the appropriate invader type at each position in the grid
        for (int i = -extentZ; i <= extentZ; i++)
        {
            for (int j = -extentX; j <= extentX; j++)
            {
                int row = i + extentZ;
                Vector3 invaderPosition = center + new Vector3(j, 0, i) * this.spacing;

                // Bottom two rows - large invader
                if (row < 2)
                {
                    Instantiate(largeInvaderPrefab, invaderPosition, Quaternion.identity, this.transform);
                }
                // Middle two rows - medium invader
                else if (row < 4)
                {
                    Instantiate(mediumInvaderPrefab, invaderPosition, Quaternion.identity, this.transform);
                }
                // Top row - small invader
                else
                {
                    Instantiate(smallInvaderPrefab, invaderPosition, Quaternion.identity, this.transform);
                }
            }
        }
    }

    public void FireMissiles()
    {
        // Randomly fire missiles
        // If invader does not have active invader in the front (check if one row down isActive), then fire missile 

        // Loop through each child (invader) transform
        foreach (Transform invader in this.transform)
        {
            // Decide if this invader should randomly fire a missile
            float xi = Random.Range(0.0f, 1.0f);
            if (numMissilesFired < 3 && xi < (1.0f / Global.invadersRemaining))
            {
                numMissilesFired++;
                Debug.Log("Enemy Missile fired! Num Missiles Fired: " + numMissilesFired);

                Vector3 spawnPos = invader.position;
                spawnPos.z -= 0.75f; // add slight offset so that missile spawns at bottom of invader

                // instantiate the Missile
                GameObject obj = Instantiate(enemyMissilePrefab, spawnPos, Quaternion.identity) as GameObject;
                break;
            }
        }
    }

    public void ResetGrid()
    {
        direction = Vector3.right;
        invaderSpeed = 1.0f;
        numMissilesFired = 0;

        InstantiateGrid();
    }

}
