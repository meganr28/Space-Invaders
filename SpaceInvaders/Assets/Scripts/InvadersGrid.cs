using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersGrid : MonoBehaviour
{
    public GameObject smallInvaderPrefab;
    public GameObject mediumInvaderPrefab;
    public GameObject largeInvaderPrefab;
    public Vector3 center;
    public float invaderSpeed;
    public float minX, maxX;

    private Vector3 direction = Vector3.right;
    private float decrementStep = 0.1f;
    private float spacing = 1.5f;
    private int rows = 5;
    private int columns = 11;

    void Awake()
    {
        // Change the center position depending on the level
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        center = this.transform.position - (g.level - 1) * Vector3.forward;

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

    // Start is called before the first frame update
    void Start()
    {
        invaderSpeed = 0.5f;
        minX = -9.5f;
        maxX = 9.5f;
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
            if (invader.position.x < minX || invader.position.x > maxX)
            {
                direction *= -1.0f;
                this.transform.position -= Vector3.forward * decrementStep;
            }
        }
    }
}
