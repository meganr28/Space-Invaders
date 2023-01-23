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
        // Instantiate one shield piece at the given position
        Instantiate(shieldPiecePrefab, shieldPosition, Quaternion.identity, this.transform);
    }
}
