using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersGrid : MonoBehaviour
{
    public GameObject smallInvaderPrefab;
    public GameObject mediumInvaderPrefab;
    public GameObject largeInvaderPrefab;
    public Vector3 center;
    private float spacing = 1.5f;
    private int rows = 5;
    private int columns = 11;

    void Awake()
    {
        // Change the center position depending on the level
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        center = this.transform.position - (g.level - 1) * Vector3.forward;

        int extentX = (this.columns - 1) / 2;
        int extentZ = (this.rows - 1) / 2;

        for (int i = -extentZ; i <= extentZ; i++)
        {
            for (int j = -extentX; j <= extentX; j++)
            {
                int row = i + extentZ;
                Vector3 invaderPosition = center + new Vector3(j, 0, i) * this.spacing;

                if (row < 2)
                {
                    Instantiate(largeInvaderPrefab, invaderPosition, Quaternion.identity, this.transform);
                    //invader.transform.localPosition = invaderPosition;
                }
                else if (row < 4)
                {
                    Instantiate(mediumInvaderPrefab, invaderPosition, Quaternion.identity, this.transform);
                }
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

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * 1.0f * Time.deltaTime;
    }

    void DecrementRow()
    {

    }
}
