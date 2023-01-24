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
        
    }

    public void InstantiateShield()
    {
        //// Instantiate top piece
        //Instantiate(shieldPiecePrefab, new Vector3(shieldPosition.x, shieldPosition.y, shieldPosition.z + 1.0f), Quaternion.identity, this.transform);

        // Instantiate "feet"
        Instantiate(shieldPiecePrefab, new Vector3(shieldPosition.x - 1.0f, shieldPosition.y, shieldPosition.z - 1.0f), Quaternion.identity);
        Instantiate(shieldPiecePrefab, new Vector3(shieldPosition.x + 1.0f, shieldPosition.y, shieldPosition.z - 1.0f), Quaternion.identity);

        // Instantiate middle bar
        for (float i = shieldPosition.x - 1.0f; i <= shieldPosition.x + 1.0f; i++)
        {
            Instantiate(shieldPiecePrefab, new Vector3(i, shieldPosition.y, shieldPosition.z), Quaternion.identity);
        }
    }
}
