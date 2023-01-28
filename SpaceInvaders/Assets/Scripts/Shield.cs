using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldPiecePrefab;
    public Vector3 shieldPosition;

    void Awake()
    {
        InstantiateShield();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If win level, reset grid 
        if (Global.resetGrid)
        {
            InstantiateShield();
        }
    }

    public void InstantiateShield()
    {
        // Row 3
        for (float i = shieldPosition.x - 1.25f; i <= shieldPosition.x + 1.25f; i += 0.5f)
        {
            Instantiate(shieldPiecePrefab, new Vector3(i, shieldPosition.y, shieldPosition.z), Quaternion.identity);
        }

        // Row 2
        for (float i = shieldPosition.x - 1.25f; i <= shieldPosition.x + 1.25f; i += 0.5f)
        {
            Instantiate(shieldPiecePrefab, new Vector3(i, shieldPosition.y, shieldPosition.z + 0.5f), Quaternion.identity);
        }

        // Row 1
        for (float i = shieldPosition.x - 1.25f; i <= shieldPosition.x + 1.25f; i += 0.5f)
        {
            if (i == shieldPosition.x - 0.25f || i == shieldPosition.x + 0.25f) continue;
            Instantiate(shieldPiecePrefab, new Vector3(i, shieldPosition.y, shieldPosition.z - 0.5f), Quaternion.identity);
        }

        // Row 0
        for (float i = shieldPosition.x - 1.25f; i <= shieldPosition.x + 1.25f; i += 0.5f)
        {
            if (i == shieldPosition.x - 0.25f || i == shieldPosition.x + 0.25f) continue;
            Instantiate(shieldPiecePrefab, new Vector3(i, shieldPosition.y, shieldPosition.z - 1.0f), Quaternion.identity);
        }
    }
}
